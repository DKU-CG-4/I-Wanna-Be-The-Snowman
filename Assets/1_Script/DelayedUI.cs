using System.Collections; // �ڷ�ƾ�� �ʿ��� ���ӽ����̽�
using UnityEngine;
using UnityEngine.UI;

public class DelayedUI : MonoBehaviour
{
    public GameObject uiElement; // ��Ÿ�� UI ������Ʈ
    public float delay = 7f;     // ������ �ð� (��)

    void Start()
    {
        // UI�� ó���� ��Ȱ��ȭ
        if (uiElement != null)
        {
            uiElement.SetActive(false);
        }

        // �ڷ�ƾ ����
        StartCoroutine(ShowUIAfterDelay());
    }

    IEnumerator ShowUIAfterDelay()
    {
        // ������ �ð���ŭ ���
        yield return new WaitForSeconds(delay);

        // UI Ȱ��ȭ
        if (uiElement != null)
        {
            uiElement.SetActive(true);
        }
    }
}
