using UnityEngine;
using UnityEngine.SceneManagement;

public class Penguin : MonoBehaviour
{
    public float followSpeed = 5f;       // Penguin의 이동 속도
    public float followDistance = 10f;   // Player를 따라오기 시작하는 거리
    public float headRadius = 1.7f;      // 머리의 충돌 감지 반경
    public LayerMask playerLayer;        // Player를 감지할 레이어

    private Transform playerTransform;   // Player의 Transform을 저장할 변수
    private bool isFallen = false;       // Penguin이 쓰러졌는지 확인
    private bool isHeadHit = false;      // 머리가 밟혔는지 확인
    private CapsuleCollider capsuleCollider; // Penguin의 Capsule Collider
    private Transform headTransform;     // 머리 위치를 저장할 Transform

    void Start()
    {
        // Tag가 "Player"인 GameObject를 찾아 Transform을 가져옴
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player를 찾을 수 없습니다!");
        }

        // Capsule Collider와 머리 Transform 가져오기
        capsuleCollider = GetComponent<CapsuleCollider>();

        // headTransform을 새로운 GameObject로 생성
        headTransform = new GameObject("Head").transform;
        headTransform.SetParent(transform); // Penguin 객체의 자식으로 설정

        // 머리 위치를 Capsule Collider의 윗부분에 맞추기
        UpdateHeadPosition();
    }

    void Update()
    {
        UpdateHeadPosition();
        if (isFallen) return; // 쓰러진 상태에서는 동작하지 않음

        if (playerTransform != null)
        {
            // Player와 Penguin 사이의 거리 계산
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            // 특정 거리 내에 들어오면 Player를 향해 이동
            if (distanceToPlayer <= followDistance)
            {
                FollowPlayer();
            }
        }

        // 머리 위에 Player가 있는지 감지
        CheckHeadCollision();
    }

    void FollowPlayer()
    {
        // Player의 위치 방향으로 이동
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.position += direction * followSpeed * Time.deltaTime;

        // 머리 위치 업데이트 (Capsule Collider에 맞춰서 머리 위치도 이동)
        UpdateHeadPosition();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Player와 충돌했을 때, 머리를 밟힌 경우가 아니라면 현재 Scene 다시 로드
        if (collision.gameObject.CompareTag("Player") && !isFallen && !isHeadHit)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 Scene 다시 로드
        }
    }

    void CheckHeadCollision()
    {
        // 머리 위치에서 일정 반경 내에 Player가 있는지 확인
        Collider[] hits = Physics.OverlapSphere(headTransform.position, headRadius, playerLayer);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                isHeadHit = true; // 머리가 밟혔다고 설정
                FallDown();       // Penguin이 쓰러지게 처리
                return;
            }
        }

        isHeadHit = false; // Player가 머리 위에 없으면 다시 false로 설정
    }

    void FallDown()
    {
        isFallen = true; // 쓰러짐 상태로 설정

        // Rigidbody 추가 후 회전
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.AddForce(Vector3.back * 5f, ForceMode.Impulse);  // 뒤로 쓰러지는 힘 적용
        rb.AddTorque(Vector3.right * 10f, ForceMode.Impulse); // 쓰러지는 회전력 적용

        // 일정 시간 후에 움직임 멈춤
        Destroy(rb, 3f);
    }
    // 머리 위치를 Capsule Collider의 윗부분으로 업데이트
    void UpdateHeadPosition()
    {
        // CapsuleCollider의 중심을 월드 좌표계에서 계산
        Vector3 colliderCenter = transform.TransformPoint(capsuleCollider.center);

        // CapsuleCollider의 높이를 반영해서 머리 위치를 윗부분으로 설정
        float headHeight = capsuleCollider.height / 2f;

        // 머리 위치는 중심에서 half height만큼 위로 이동해야 하므로, 이를 적용
        headTransform.position = colliderCenter + new Vector3(0, headHeight, 0);

        // 디버깅을 위한 출력
        Debug.Log("Collider Center: " + colliderCenter);
        Debug.Log("Head Position: " + headTransform.position);
    }



    void OnDrawGizmosSelected()
    {
        // 머리 충돌 감지 영역 시각화
        if (headTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(headTransform.position, headRadius);
        }
    }
}