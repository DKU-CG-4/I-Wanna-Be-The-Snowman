using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonScript : MonoBehaviour
{
    public void StartGame()
    {
        // "GameScene"�̶�� �̸��� ����� �ε�
        SceneManager.LoadScene("Example1");
    }
}

