using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float halfDayDuration = 60f; // 반나절 길이 (초 단위)
    private float currentTime = 0f;     // 현재 시간
    private bool isDayComplete = false; // 낮이 완료되었는지 확인

    void Update()
    {
        if (!isDayComplete)
        {
            // 시간 경과에 따라 진행 비율 계산 (0 ~ 1 사이)
            currentTime += Time.deltaTime;
            float dayProgress = currentTime / halfDayDuration;

            // dayProgress가 1 이상이 되면 낮을 멈춤
            if (dayProgress >= 1f)
            {
                dayProgress = 1f;
                isDayComplete = true;
            }

            // 태양 회전 설정 (X축 기준)
            float sunAngle = dayProgress * 90f; // 0도에서 90도까지만 이동
            transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);

            // 태양의 밝기 조절 (새벽에서 정오까지 점점 밝아짐)
            Light sunLight = GetComponent<Light>();
            if (sunLight != null)
            {
                sunLight.intensity = Mathf.Lerp(0.2f, 1f, dayProgress); // 새벽(0.2) -> 정오(1)
            }
        }
    }
}
