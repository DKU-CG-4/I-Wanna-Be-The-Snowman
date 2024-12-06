using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    Rigidbody rigid;               // Rigidbody ������Ʈ�� ���� ���� ����
    public float JumpPower = 10;   // ������ �� ������ ���� ũ��
    public float scaleIncrease = 0.2f; // �÷��̾� ũ�� ������ (Unity �����Ϳ��� ���� ����)

    public float normalSpeed = 5f; // �⺻ �̵� �ӵ�
    private float moveSpeed;       // ���� �̵� �ӵ�
    private bool isBoosted = false; // �ӵ� ���� ���� Ȯ��
    public float rotationSpeed = 100f; // ȸ�� �ӵ�

    bool isJump;                   // ���� ���� Ȯ�� ����
    private Vector3 moveDirection; // �̵� ����

    void Awake()
    {
        isJump = false;             // ������ �� ���� ���¸� false�� �ʱ�ȭ
        rigid = GetComponent<Rigidbody>(); // Rigidbody ������Ʈ �ʱ�ȭ
        moveSpeed = normalSpeed;    // �ʱ� �̵� �ӵ��� �⺻ �ӵ��� ����
        moveDirection = Vector3.forward; // �ʱ� �̵� ���� (Z��)
    }

    void Update()
    {
        // Jump Ű�� ���Ȱ� ���� ���� ���°� �ƴ� �� ����
        if (Input.GetButtonDown("Jump") && isJump == false)
        {
            isJump = true;         // ���� ���¸� true�� ����
            rigid.AddForce(new Vector3(0, JumpPower, 0), ForceMode.Impulse); // ���� �������� JumpPower ũ�⸸ŭ ���� ����
        }

        // �¿� ����Ű �Է¿� ���� �� ȸ��
        float horizontalInput = Input.GetAxis("Horizontal"); // -1 (����) ~ 1 (������)
        if (horizontalInput != 0)
        {
            // Y�� �������� �̵� ���� ȸ��
            Quaternion rotation = Quaternion.Euler(0, horizontalInput * rotationSpeed * Time.deltaTime, 0);
            moveDirection = rotation * moveDirection; // �̵� ���� ȸ��
        }
    }

    void FixedUpdate()
    {
        // ���� ���� �Է� �� �޾ƿ� (Z�� �̵�)
        float verticalInput = Input.GetAxisRaw("Vertical"); // ����(1)�� ����(-1) ������ �Է� ��

        // �̵�: ���� ȸ���� �̵� ���� �������� ���� ����
        Vector3 move = moveDirection * verticalInput * moveSpeed;
        rigid.AddForce(move, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        // �浹�� ��ü�� �±װ� "Float"�� ��쿡�� isJump�� false�� �����Ͽ� �ٽ� ���� ���� ���·� ��ȯ
        if (collision.gameObject.CompareTag("Float"))
            isJump = false;

        // �浹�� ��ü�� �±װ� "Bear"�� ��� Restart
        if (collision.gameObject.CompareTag("Bear"))
        {
            Debug.Log("Bear�� �浹! ������ ������մϴ�.");
            RestartGame();
        }

        // �浹�� ��ü�� �±װ� "Water"�� ��� 2�� �� ���� �ٽ� �ε�
        if (collision.gameObject.CompareTag("Water"))
        {
            Debug.Log("Water�� �浹! 2�� �� ���� �ٽ� �ε��մϴ�.");
            StartCoroutine(ReloadSceneAfterDelay(2f));
        }
    }

    private void RestartGame()
    {
        // ���� �� �ٽ� �ε�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // ���� ������ ������ ��ü ������ ������ �ٽ� ����
        GameManager.Instance.RemainItemCount = GameManager.Instance.TotalItemCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            other.gameObject.SetActive(false); // ������ ��Ȱ��ȭ

            // ���� �� ���� ����
            GameManager.Instance.RemainItemCount--;

            // �÷��̾� ũ�� ����
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

            // �ӵ� ���� �ڷ�ƾ ȣ��
            StartSpeedBoost(10f, 5f); // �ӵ� 10���� ����, 5�� ����
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
        // �ӵ� ���� ����
        isBoosted = true;
        moveSpeed = boostSpeed;

        // boostDuration ���� ���
        yield return new WaitForSeconds(duration);

        // �ӵ� ����
        moveSpeed = normalSpeed;
        isBoosted = false;
    }

    // 2�� �Ŀ� ���� �ٽ� �ε��ϴ� �ڷ�ƾ
    private IEnumerator ReloadSceneAfterDelay(float delay)
    {
        // ������ �ð���ŭ ���
        yield return new WaitForSeconds(delay);

        // ���� �� �ٽ� �ε�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
