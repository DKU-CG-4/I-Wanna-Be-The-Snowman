using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    Rigidbody rigid;             // Rigidbody 컴포넌트를 위한 변수 선언
    public float JumpPower = 10;  // 점프할 때 적용할 힘의 크기
    public int itemCount;
    public GameManagerLogic manager;

    bool isJump;                  // 점프 상태 확인 변수

    void Awake()
    {
        isJump = false;           // 시작할 때 점프 상태를 false로 초기화
        rigid = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 초기화
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
        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
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
        }
        // 트리거로 충돌한 객체의 태그가 "Finish"일 때, "SampleScene"으로 장면을 로드하여 다시 시작
        else if (other.name == "Finish")
        {
            if(itemCount == manager.TotalItemCount)
            {
                //Game Clear!
                SceneManager.LoadScene("Example" + (manager.stage+1).ToString());
            }
            else
            {
                //Restart..
                SceneManager.LoadScene("Example" + (manager.stage).ToString());
            }
        }
    }
}
