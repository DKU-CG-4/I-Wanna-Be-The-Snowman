using System.Collections; // 코루틴에 필요한 네임스페이스
using UnityEngine;
using UnityEngine.UI;

public class DelayedUI : MonoBehaviour
{
    public GameObject uiElement; // 나타날 UI 오브젝트
    public float delay = 7f;     // 딜레이 시간 (초)

    void Start()
    {
        // UI를 처음에 비활성화
        if (uiElement != null)
        {
            uiElement.SetActive(false);
        }

        // 코루틴 실행
        StartCoroutine(ShowUIAfterDelay());
    }

    IEnumerator ShowUIAfterDelay()
    {
        // 지정된 시간만큼 대기
        yield return new WaitForSeconds(delay);

        // UI 활성화
        if (uiElement != null)
        {
            uiElement.SetActive(true);
        }
    }
}
