using UnityEngine;

public class ItemJump : MonoBehaviour
{
    public float scaleSpeed = 1f; // 아이템의 스케일 변화를 위한 속도
    public float increasedJumpHeight = 10f; // 증가된 점프 높이
    public float jumpDuration = 5f; // 점프 부스트 지속 시간

    private Vector3 initialS; // 초기 스케일 저장
    private bool isJumpIncreased = false; // 점프 부스트 여부 확인

    private void Start()
    {
        // 초기 스케일 값 저장
        initialS = transform.localScale;
    }

    void Update()
    {
        // 스케일 변화를 애니메이션처럼 적용
        float scaleFactor = Mathf.PingPong(Time.time * scaleSpeed, 0.2f) + 1f; // 1 ~ 1.2 사이
        transform.localScale = initialS * scaleFactor; // 초기 스케일에 스케일 팩터 적용
    }

    void OnTriggerEnter(Collider other)
    {
        // 태그가 "Player"인 객체와 충돌했을 경우
        if (other.CompareTag("Player") && !isJumpIncreased)
        {
            PlayerBall player = other.GetComponent<PlayerBall>();

            if (player != null)
            {
                isJumpIncreased = true;

                // Player에게 점프 부스트 시작
                player.StartJumpBoost(increasedJumpHeight, jumpDuration);
            }

            // 아이템 비활성화
            gameObject.SetActive(false);
        }
    }
}
