using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    Rigidbody rigid;               // Rigidbody 컴포넌트를 위한 변수 선언
    public float JumpPower = 10;   // 점프할 때 적용할 힘의 크기
    public int itemCount;          // 아이템 개수
    public GameManagerLogic manager;

    public float normalSpeed = 5f; // 기본 이동 속도
    private float moveSpeed;       // 현재 이동 속도
    private bool isBoosted = false; // 속도 증가 상태 확인

    bool isJump;                   // 점프 상태 확인 변수

    void Awake()
    {
        isJump = false;             // 시작할 때 점프 상태를 false로 초기화
        rigid = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 초기화
        moveSpeed = normalSpeed;    // 초기 이동 속도를 기본 속도로 설정
    }

    void Update()
    {
        // Jump 키가 눌렸고 현재 점프 상태가 아닐 때 실행
        if (Input.GetButtonDown("Jump") && isJump == false)
        {
            isJump = true;         // 점프 상태를 true로 설정
            rigid.AddForce(new Vector3(0, JumpPower, 0), ForceMode.Impulse); // 위쪽 방향으로 JumpPower 크기만큼 힘을 가함
        }
    }

    void FixedUpdate()
    {
        // 수평 및 수직 입력 값을 받아옴
        float h = Input.GetAxisRaw("Horizontal"); // 왼쪽(-1)과 오른쪽(1) 방향의 입력 값
        float v = Input.GetAxisRaw("Vertical");   // 앞쪽(1)과 뒤쪽(-1) 방향의 입력 값

        // 입력된 방향으로 힘을 가하여 이동
        rigid.AddForce(new Vector3(h, 0, v) * moveSpeed, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 객체의 이름이 "test"인 경우에만 isJump를 false로 설정하여 다시 점프 가능 상태로 전환
        if (collision.gameObject.name == "test")
            isJump = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            other.gameObject.SetActive(false);

            // 플레이어 크기 증가
            Vector3 currentScale = transform.localScale;
            transform.localScale = new Vector3(currentScale.x + 0.2f, currentScale.y + 0.2f, currentScale.z + 0.2f);
        }
        else if (other.tag == "Speed")
        {
            other.gameObject.SetActive(false);

            // 속도 증가 코루틴 호출
            StartSpeedBoost(10f, 5f); // 속도 10으로 증가, 5초 지속
        }
        else if (other.name == "Finish")
        {
            if (itemCount == manager.TotalItemCount)
            {
                //Game Clear!
                SceneManager.LoadScene("Example" + (manager.stage + 1).ToString());
            }
            else
            {
                //Restart..
                SceneManager.LoadScene("Example" + (manager.stage).ToString());
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
}

