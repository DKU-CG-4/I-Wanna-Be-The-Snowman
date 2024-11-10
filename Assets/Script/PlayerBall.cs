using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    Rigidbody rigid;             // Rigidbody ������Ʈ�� ���� ���� ����
    public float JumpPower = 10;  // ������ �� ������ ���� ũ��
    public int itemCount;
    public GameManagerLogic manager;

    bool isJump;                  // ���� ���� Ȯ�� ����

    void Awake()
    {
        isJump = false;           // ������ �� ���� ���¸� false�� �ʱ�ȭ
        rigid = GetComponent<Rigidbody>(); // Rigidbody ������Ʈ �ʱ�ȭ
    }

    void Update()
    {
        // Jump Ű�� ���Ȱ� ���� ���� ���°� �ƴ� �� ����
        if (Input.GetButtonDown("Jump") && isJump == false)
        {
            isJump = true;         // ���� ���¸� true�� ����
            rigid.AddForce(new Vector3(0, JumpPower, 0), ForceMode.Impulse); // ���� �������� JumpPower ũ�⸸ŭ ���� ����
        }
    }

    void FixedUpdate()
    {
        // ���� �� ���� �Է� ���� �޾ƿ�
        float h = Input.GetAxisRaw("Horizontal"); // ����(-1)�� ������(1) ������ �Է� ��
        float v = Input.GetAxisRaw("Vertical");   // ����(1)�� ����(-1) ������ �Է� ��

        // �Էµ� �������� ���� ���Ͽ� �̵�
        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        // �浹�� ��ü�� �̸��� "test"�� ��쿡�� isJump�� false�� �����Ͽ� �ٽ� ���� ���� ���·� ��ȯ
        if (collision.gameObject.name == "test")
            isJump = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            other.gameObject.SetActive(false);
        }
        // Ʈ���ŷ� �浹�� ��ü�� �±װ� "Finish"�� ��, "SampleScene"���� ����� �ε��Ͽ� �ٽ� ����
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
