using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName;

    public void ChangeScene()
    {
        // sceneName이라는 이름의 장면을 로드
        SceneManager.LoadScene(sceneName);
        GameManager.Instance.RemainItemCount = GameManager.Instance.TotalItemCount;
    }
}
