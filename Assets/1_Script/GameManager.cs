using UnityEngine;

// �̱��� ������ ���� ���� �Ŵ��� ����
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public int TotalItemCount = 5;
    public int RemainItemCount; // ���� ������ ����
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

        // ���� ������ ������ ��ü ������ ������ �ʱ�ȭ
        RemainItemCount = TotalItemCount;
    }
}
