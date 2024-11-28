using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public int TotalItemCount = 5;  // ��ü ������ ����
    public int RemainItemCount;     // ���� ������ ����
    public int stage;

    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.LogError("GameManager �̱��� ��ü�� �����ϴ�.");
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

    // �����۰� �÷��̾ �ε����� ������ ���� ���ҽ�Ű�� �޼���
    public void DecreaseItemCount()
    {
        if (RemainItemCount > 0)
        {
            RemainItemCount--;

            Debug.Log("���� ������ ����: " + RemainItemCount);

            if (RemainItemCount <= 0)
            {
                Debug.Log("��� �������� ȹ���߽��ϴ�!");
                // ���� ���������� �Ѿ�� ó�� �߰� ����
            }
        }
    }
}
