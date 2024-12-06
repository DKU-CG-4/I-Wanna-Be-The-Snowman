using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    Rigidbody rigid;               // Rigidbody 컴포넌트를 위한 변수 선언
    public float JumpPower = 10;   // 점프할 때 적용할 힘의 크기
    public float scaleIncrease = 0.2f; // 플레이어 크기 증가량 (Unity 에디터에서 조절 가능)

    public float normalSpeed = 5f; // 기본 이동 속도
    private float moveSpeed;       // 현재 이동 속도
    private bool isBoosted = false; // 속도 증가 상태 확인
    public float rotationSpeed = 100f; // 회전 속도

    bool isJump;                   // 점프 상태 확인 변수
    private Vector3 moveDirection; // 이동 방향

    void Awake()
    {
        isJump = false;             // 시작할 때 점프 상태를 false로 초기화
        rigid = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 초기화
        moveSpeed = normalSpeed;    // 초기 이동 속도를 기본 속도로 설정
        moveDirection = Vector3.forward; // 초기 이동 방향 (Z축)
    }

    void Update()
    {
        // Jump 키가 눌렸고 현재 점프 상태가 아닐 때 실행
        if (Input.GetButtonDown("Jump") && isJump == false)
        {
            isJump = true;         // 점프 상태를 true로 설정
            rigid.AddForce(new Vector3(0, JumpPower, 0), ForceMode.Impulse); // 위쪽 방향으로 JumpPower 크기만큼 힘을 가함
        }

        // 좌우 방향키 입력에 따라 축 회전
        float horizontalInput = Input.GetAxis("Horizontal"); // -1 (왼쪽) ~ 1 (오른쪽)
        if (horizontalInput != 0)
        {
            // Y축 기준으로 이동 방향 회전
            Quaternion rotation = Quaternion.Euler(0, horizontalInput * rotationSpeed * Time.deltaTime, 0);
            moveDirection = rotation * moveDirection; // 이동 방향 회전
        }
    }

    void FixedUpdate()
    {
        // 전후 방향 입력 값 받아옴 (Z축 이동)
        float verticalInput = Input.GetAxisRaw("Vertical"); // 앞쪽(1)과 뒤쪽(-1) 방향의 입력 값

        // 이동: 현재 회전된 이동 방향 기준으로 힘을 가함
        Vector3 move = moveDirection * verticalInput * moveSpeed;
        rigid.AddForce(move, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 객체의 태그가 "Float"인 경우에만 isJump를 false로 설정하여 다시 점프 가능 상태로 전환
        if (collision.gameObject.CompareTag("Float"))
            isJump = false;

        // 충돌한 객체의 태그가 "Bear"인 경우 Restart
        if (collision.gameObject.CompareTag("Bear"))
        {
            Debug.Log("Bear와 충돌! 게임을 재시작합니다.");
            RestartGame();
        }

        // 충돌한 객체의 태그가 "Water"인 경우 2초 후 씬을 다시 로드
        if (collision.gameObject.CompareTag("Water"))
        {
            Debug.Log("Water와 충돌! 2초 후 씬을 다시 로드합니다.");
            StartCoroutine(ReloadSceneAfterDelay(2f));
        }
    }

    private void RestartGame()
    {
        // 현재 씬 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // 남은 아이템 갯수를 전체 아이템 갯수로 다시 세팅
        GameManager.Instance.RemainItemCount = GameManager.Instance.TotalItemCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            other.gameObject.SetActive(false); // 아이템 비활성화

            // 남은 눈 갯수 감소
            GameManager.Instance.RemainItemCount--;

            // 플레이어 크기 증가
            Vector3 currentScale = transform.localScale;
            transform.localScale = new Vector3(
                currentScale.x + scaleIncrease,
                currentScale.y + scaleIncrease,
                currentScale.z + scaleIncrease
            );
        }
        else if (other.tag == "Speed")
        {
            other.gameObject.SetActive(false);

            // 속도 증가 코루틴 호출
            StartSpeedBoost(10f, 5f); // 속도 10으로 증가, 5초 지속
        }
        else if (other.name == "Finish")
        {
            if (GameManager.Instance.RemainItemCount == 0)
            {
                //Game Clear!
                SceneManager.LoadScene("Example" + (GameManager.Instance.stage + 1).ToString());
            }
            else
            {
                //Restart..
                SceneManager.LoadScene("Example" + (GameManager.Instance.stage).ToString());
            }
        }
    }

    public void StartSpeedBoost(float boostSpeed, float duration)
    {
        if (!isBoosted)
        {
            StartCoroutine(SpeedBoostCoroutine(boostSpeed, duration));
        }
    }

    private IEnumerator SpeedBoostCoroutine(float boostSpeed, float duration)
    {
        // 속도 증가 시작
        isBoosted = true;
        moveSpeed = boostSpeed;

        // boostDuration 동안 대기
        yield return new WaitForSeconds(duration);

        // 속도 복원
        moveSpeed = normalSpeed;
        isBoosted = false;
    }

    // 2초 후에 씬을 다시 로드하는 코루틴
    private IEnumerator ReloadSceneAfterDelay(float delay)
    {
        // 지정된 시간만큼 대기
        yield return new WaitForSeconds(delay);

        // 현재 씬 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
