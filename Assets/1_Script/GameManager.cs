using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public int TotalItemCount = 5;  // 전체 아이템 갯수
    public int RemainItemCount;     // 남은 아이템 갯수
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
        DontDestroyOnLoad(gameObject);

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
}
