using UnityEngine;
using UnityEngine.SceneManagement;

public class Lifes : MonoBehaviour
{
    public int lifes = 3;

    private Points points;

    private void Start()
    {
        points = FindAnyObjectByType<Points>();
        HUDController.instance.UpdateLifes(lifes);
        points = FindAnyObjectByType<Points>();
    }

    public void LoseLife(int amount)
    {
        lifes -= amount;

        if (lifes < 0) { lifes = 0; }

        HUDController.instance.UpdateLifes(lifes);

        if (lifes <= 0)
        {
            PlayerPrefs.SetInt("LastScore", points.points);
            Time.timeScale = 0f;
            SceneManager.LoadScene("ScoreBoardScene");
            return;
        }
    }
}
