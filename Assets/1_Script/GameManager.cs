using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public int TotalItemCount = 5;  // 전체 아이템 갯수
    public int RemainItemCount;     // 아이템 갯수
    public float RemainJumpTime = 0;    // 점프 부스트 남은 시간 변수들
    public int jumpMin, jumpSec;    // ...
    public int stage;

    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.LogError("GameManager 싱글톤 객체가 없습니다.");
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // 남은 아이템 갯수를 전체 아이템 갯수로 초기화
        RemainItemCount = TotalItemCount;
    }

    // 아이템과 플레이어가 부딪히면 아이템 수를 감소시키는 메서드
    public void DecreaseItemCount()
    {
        if (RemainItemCount > 0)
        {
            RemainItemCount--;

            Debug.Log("남은 아이템 갯수: " + RemainItemCount);

            if (RemainItemCount <= 0)
            {
                Debug.Log("모든 아이템을 획득했습니다!");
                // 다음 스테이지로 넘어가는 처리 추가 가능
            }
        }
    }

    // 남은 점프 시간 타이머 코루틴 호출
    public void StartTimerCoroutine(float duration)
    {
        StartCoroutine(TimerCoroutine(duration));
    }

    // 남은 점프 시간 타이머 코루틴
    private IEnumerator TimerCoroutine(float duration)
    {
        RemainJumpTime += duration;

        while (RemainJumpTime > 0)
        {
            RemainJumpTime -= Time.deltaTime;
            jumpMin = (int)RemainJumpTime / 60;
            jumpSec = (int)RemainJumpTime % 60;
            yield return null;

            if (RemainJumpTime <= 0)
            {
                RemainJumpTime = 0;
                yield break;
            }
        }
    }
}
