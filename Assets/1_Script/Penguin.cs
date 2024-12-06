using UnityEngine;
using UnityEngine.SceneManagement;

public class Penguin : MonoBehaviour
{
    public float followSpeed = 5f;       // Penguin�� �̵� �ӵ�
    public float followDistance = 10f;   // Player�� ������� �����ϴ� �Ÿ�
    public float headRadius = 1.7f;      // �Ӹ��� �浹 ���� �ݰ�
    public LayerMask playerLayer;        // Player�� ������ ���̾�

    private Transform playerTransform;   // Player�� Transform�� ������ ����
    private bool isFallen = false;       // Penguin�� ���������� Ȯ��
    private bool isHeadHit = false;      // �Ӹ��� �������� Ȯ��
    private CapsuleCollider capsuleCollider; // Penguin�� Capsule Collider
    private Transform headTransform;     // �Ӹ� ��ġ�� ������ Transform

    void Start()
    {
        // Tag�� "Player"�� GameObject�� ã�� Transform�� ������
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player�� ã�� �� �����ϴ�!");
        }

        // Capsule Collider�� �Ӹ� Transform ��������
        capsuleCollider = GetComponent<CapsuleCollider>();

        // headTransform�� ���ο� GameObject�� ����
        headTransform = new GameObject("Head").transform;
        headTransform.SetParent(transform); // Penguin ��ü�� �ڽ����� ����

        // �Ӹ� ��ġ�� Capsule Collider�� ���κп� ���߱�
        UpdateHeadPosition();
    }

    void Update()
    {
        UpdateHeadPosition();
        if (isFallen) return; // ������ ���¿����� �������� ����

        if (playerTransform != null)
        {
            // Player�� Penguin ������ �Ÿ� ���
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            // Ư�� �Ÿ� ���� ������ Player�� ���� �̵�
            if (distanceToPlayer <= followDistance)
            {
                FollowPlayer();
            }
        }

        // �Ӹ� ���� Player�� �ִ��� ����
        CheckHeadCollision();
    }

    void FollowPlayer()
    {
        // Player�� ��ġ �������� �̵�
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.position += direction * followSpeed * Time.deltaTime;

        // �Ӹ� ��ġ ������Ʈ (Capsule Collider�� ���缭 �Ӹ� ��ġ�� �̵�)
        UpdateHeadPosition();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Player�� �浹���� ��, �Ӹ��� ���� ��찡 �ƴ϶�� ���� Scene �ٽ� �ε�
        if (collision.gameObject.CompareTag("Player") && !isFallen && !isHeadHit)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ���� Scene �ٽ� �ε�
        }
    }

    void CheckHeadCollision()
    {
        // �Ӹ� ��ġ���� ���� �ݰ� ���� Player�� �ִ��� Ȯ��
        Collider[] hits = Physics.OverlapSphere(headTransform.position, headRadius, playerLayer);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                isHeadHit = true; // �Ӹ��� �����ٰ� ����
                FallDown();       // Penguin�� �������� ó��
                return;
            }
        }

        isHeadHit = false; // Player�� �Ӹ� ���� ������ �ٽ� false�� ����
    }

    void FallDown()
    {
        isFallen = true; // ������ ���·� ����

        // Rigidbody �߰� �� ȸ��
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.AddForce(Vector3.back * 5f, ForceMode.Impulse);  // �ڷ� �������� �� ����
        rb.AddTorque(Vector3.right * 10f, ForceMode.Impulse); // �������� ȸ���� ����

        // ���� �ð� �Ŀ� ������ ����
        Destroy(rb, 3f);
    }
    // �Ӹ� ��ġ�� Capsule Collider�� ���κ����� ������Ʈ
    void UpdateHeadPosition()
    {
        // CapsuleCollider�� �߽��� ���� ��ǥ�迡�� ���
        Vector3 colliderCenter = transform.TransformPoint(capsuleCollider.center);

        // CapsuleCollider�� ���̸� �ݿ��ؼ� �Ӹ� ��ġ�� ���κ����� ����
        float headHeight = capsuleCollider.height / 2f;

        // �Ӹ� ��ġ�� �߽ɿ��� half height��ŭ ���� �̵��ؾ� �ϹǷ�, �̸� ����
        headTransform.position = colliderCenter + new Vector3(0, headHeight, 0);

        // ������� ���� ���
        Debug.Log("Collider Center: " + colliderCenter);
        Debug.Log("Head Position: " + headTransform.position);
    }



    void OnDrawGizmosSelected()
    {
        // �Ӹ� �浹 ���� ���� �ð�ȭ
        if (headTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(headTransform.position, headRadius);
        }
    }
}