using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonScript : MonoBehaviour
{
    public void OnStartGame()
    {
        {
            Debug.LogError("GameManager 싱글톤 객체가 초기화되지 않았습니다.");
        }
    }

}

