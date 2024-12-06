using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName;

    public void ChangeScene()
    {
        // sceneName�̶�� �̸��� ����� �ε�
        SceneManager.LoadScene(sceneName);
        GameManager.Instance.RemainItemCount = GameManager.Instance.TotalItemCount;
    }
}
