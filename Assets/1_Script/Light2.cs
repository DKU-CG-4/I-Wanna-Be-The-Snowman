using UnityEngine;

public class Light2 : MonoBehaviour
{
    public float halfDayDuration = 60f; // �ݳ��� ���� (�� ����)
    private float currentTime = 0f;     // ���� �ð�
    private bool isDayComplete = false; // ���� �Ϸ�Ǿ����� Ȯ��

    void Update()
    {
        if (!isDayComplete)
        {
            // �ð� ����� ���� ���� ���� ��� (0 ~ 1 ����)
            currentTime += Time.deltaTime;
            float dayProgress = currentTime / halfDayDuration;

            // dayProgress�� 1 �̻��� �Ǹ� ���� ����
            if (dayProgress >= 1f)
            {
                dayProgress = 1f;
                isDayComplete = true;
            }

            // �¾� ȸ�� ���� (X�� ����)
            float sunAngle = Mathf.Lerp(90f, 180f, dayProgress); // 90������ 180������ �̵�
            transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);

            // �¾��� ��� ���� (�������� ���������� ��ο���)
            Light sunLight = GetComponent<Light>();
            if (sunLight != null)
            {
                sunLight.intensity = Mathf.Lerp(1f, 0.2f, dayProgress); // ����(1.0) -> ����(0.2)
            }
        }
    }
}
