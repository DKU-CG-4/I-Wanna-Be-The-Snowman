using UnityEngine;

// 싱글톤 패턴을 통해 게임 매니저 구현
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public int TotalItemCount = 5;
    public int RemainItemCount; // 남은 아이템 갯수
    public int stage;

    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
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
}
