using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    Rigidbody rigid;               // Rigidbody ������Ʈ�� ���� ���� ����
    public float JumpPower = 10;   // ������ �� ������ ���� ũ��
    public int itemCount;          // ������ ����
    public GameManagerLogic manager;

    public float normalSpeed = 5f; // �⺻ �̵� �ӵ�
    private float moveSpeed;       // ���� �̵� �ӵ�
    private bool isBoosted = false; // �ӵ� ���� ���� Ȯ��

    bool isJump;                   // ���� ���� Ȯ�� ����

    void Awake()
    {
        isJump = false;             // ������ �� ���� ���¸� false�� �ʱ�ȭ
        rigid = GetComponent<Rigidbody>(); // Rigidbody ������Ʈ �ʱ�ȭ
        moveSpeed = normalSpeed;    // �ʱ� �̵� �ӵ��� �⺻ �ӵ��� ����
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
        rigid.AddForce(new Vector3(h, 0, v) * moveSpeed, ForceMode.Impulse);
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

            // �÷��̾� ũ�� ����
            Vector3 currentScale = transform.localScale;
            transform.localScale = new Vector3(currentScale.x + 0.2f, currentScale.y + 0.2f, currentScale.z + 0.2f);
        }
        else if (other.tag == "Speed")
        {
            other.gameObject.SetActive(false);

            // �ӵ� ���� �ڷ�ƾ ȣ��
            StartSpeedBoost(10f, 5f); // �ӵ� 10���� ����, 5�� ����
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
        // �ӵ� ���� ����
        isBoosted = true;
        moveSpeed = boostSpeed;

        // boostDuration ���� ���
        yield return new WaitForSeconds(duration);

        // �ӵ� ����
        moveSpeed = normalSpeed;
        isBoosted = false;
    }
}

