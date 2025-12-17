using UnityEngine;
using UnityEngine.SceneManagement;

public class Lifes : MonoBehaviour
{
    public int lifes = 3;
    private Points points;
    private int maxLifes = 3;

    private void Start()
    {
        points = FindAnyObjectByType<Points>();
        HUDController.instance.UpdateLifes(lifes);
    }

    public void LoseLife(int amount)
    {
        lifes -= amount;
        if (lifes < 0) lifes = 0;

        HUDController.instance.UpdateLifes(lifes);
        if (lifes <= 0)
        {
            PlayerPrefs.SetInt("LastScore", points.points);
            PlayerPrefs.SetInt("FromGameScene", 1);
            PlayerPrefs.Save();

            Time.timeScale = 0f;
            SceneManager.LoadScene("ScoreBoardScene");
        }
    }

    public void AddLife(int amount)
    {
        lifes += amount;

        if (lifes > maxLifes) lifes = maxLifes;

        HUDController.instance.UpdateLifes(lifes);
    }
}
