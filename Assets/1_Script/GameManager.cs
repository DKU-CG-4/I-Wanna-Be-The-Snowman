using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public int TotalItemCount = 5;  // ��ü ������ ����
    public int RemainItemCount;     // ������ ����
    public float RemainJumpTime = 0;    // ���� �ν�Ʈ ���� �ð� ������
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

    // ���� ���� �ð� Ÿ�̸� �ڷ�ƾ ȣ��
    public void StartTimerCoroutine(float duration)
    {
        StartCoroutine(TimerCoroutine(duration));
    }

    // ���� ���� �ð� Ÿ�̸� �ڷ�ƾ
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
