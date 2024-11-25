using UnityEngine;

public class DayNightCycle : MonoBehaviour
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
            float sunAngle = dayProgress * 90f; // 0������ 90�������� �̵�
            transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);

            // �¾��� ��� ���� (�������� �������� ���� �����)
            Light sunLight = GetComponent<Light>();
            if (sunLight != null)
            {
                sunLight.intensity = Mathf.Lerp(0.2f, 1f, dayProgress); // ����(0.2) -> ����(1)
            }
        }
    }
}
