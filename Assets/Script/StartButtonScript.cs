using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonScript : MonoBehaviour
{
    public void StartGame()
    {
        // "GameScene"이라는 이름의 장면을 로드
        SceneManager.LoadScene("Example1");
    }
}

