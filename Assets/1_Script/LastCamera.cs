using UnityEngine;

public class LastCamera : MonoBehaviour
{
    public float zoomOutSpeed = 5f;        // ī�޶� �� �ƿ� �ӵ�
    public float maxZoomOutDistance = 14f; // �ִ� �� �ƿ� �Ÿ�
    private float initialZPosition;        // �ʱ� Z ��ġ

    private bool isZoomingOut = true;      // �� �ƿ� �������� Ȯ��

    void Start()
    {
        // ī�޶��� �ʱ� Z�� ��ġ�� ����
        initialZPosition = transform.position.z;
    }

    void Update()
    {
        if (!isZoomingOut) return;

        // ���� ī�޶��� Z�� �̵� �Ÿ� ���
        float currentZoomOutDistance = Mathf.Abs(transform.position.z - initialZPosition);

        if (currentZoomOutDistance < maxZoomOutDistance)
        {
            // ī�޶��� Z���� �������� ������ �̵��Ͽ� �� �ƿ� (+Z ����)
            transform.position += Vector3.forward * zoomOutSpeed * Time.deltaTime;
        }
        else
        {
            // �ִ� �� �ƿ� �Ÿ� ���� �� �� �ƿ� ����
            isZoomingOut = false;
            Debug.Log("�� �ƿ� �Ϸ�");
        }
    }
}
