using UnityEngine;

public class ItemJump : MonoBehaviour
{
    public float scaleSpeed = 1f; // �������� ������ ��ȭ�� ���� �ӵ�
    public float increasedJumpHeight = 10f; // ������ ���� ����
    public float jumpDuration = 5f; // ���� �ν�Ʈ ���� �ð�

    private Vector3 initialS; // �ʱ� ������ ����
    private bool isJumpIncreased = false; // ���� �ν�Ʈ ���� Ȯ��
    private Camera mainCamera; // ���� ī�޶� ����

    private void Start()
    {
        // �ʱ� ������ �� ����
        initialS = transform.localScale;

        // ���� ī�޶� ���� ��������
        mainCamera = Camera.main;
    }

    void Update()
    {
        // ������ ó��: �׻� ī�޶� �ٶ󺸵��� ����
        if (mainCamera != null)
        {
            Vector3 cameraPosition = mainCamera.transform.position;
            Vector3 direction = cameraPosition - transform.position;
            direction.y = 0; // 2D ȯ�濡���� Y �� ȸ�� ����
            transform.rotation = Quaternion.LookRotation(-direction);
        }

        // ������ ��ȭ�� �ִϸ��̼�ó�� ����
        float scaleFactor = Mathf.PingPong(Time.time * scaleSpeed, 0.2f) + 1f; // 1 ~ 1.2 ����
        transform.localScale = initialS * scaleFactor; // �ʱ� �����Ͽ� ������ ���� ����
    }

    void OnTriggerEnter(Collider other)
    {
        // �±װ� "Player"�� ��ü�� �浹���� ���
        if (other.CompareTag("Player") && !isJumpIncreased)
        {
            PlayerBall player = other.GetComponent<PlayerBall>();

            if (player != null)
            {
                isJumpIncreased = true;

                // Player���� ���� �ν�Ʈ ����
                player.StartJumpBoost(increasedJumpHeight, jumpDuration);
            }

            // ������ ��Ȱ��ȭ
            gameObject.SetActive(false);
        }
    }
}
