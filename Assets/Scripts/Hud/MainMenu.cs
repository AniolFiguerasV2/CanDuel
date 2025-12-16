using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
        Debug.Log("FUNCIONAAAA");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
