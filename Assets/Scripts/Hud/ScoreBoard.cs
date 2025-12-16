using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TextMeshProUGUI scoreBoardText;

    private int currentScore;
    private const int maxScores = 10;

    private List<ScoreEntry> scores = new List<ScoreEntry>();

    void Start()
    {
        currentScore = PlayerPrefs.GetInt("LastScore", 0);
        LoadScores();
        ShowScores();
    }

    public void SaveScore()
    {
        string playerName = nameInput.text;

        if (string.IsNullOrEmpty(playerName))
            return;

        scores.Add(new ScoreEntry(playerName, currentScore));
        scores.Sort((a, b) => b.score.CompareTo(a.score));

        if (scores.Count > maxScores)
            scores.RemoveAt(scores.Count - 1);

        SaveScores();
        ShowScores();
    }

    void ShowScores()
    {
        scoreBoardText.text = "";

        for (int i = 0; i < scores.Count; i++)
        {
            scoreBoardText.text += $"{i + 1}. {scores[i].name} - {scores[i].score}\n";
        }
    }

    void SaveScores()
    {
        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetString($"ScoreName{i}", scores[i].name);
            PlayerPrefs.SetInt($"ScoreValue{i}", scores[i].score);
        }
    }

    void LoadScores()
    {
        scores.Clear();

        for (int i = 0; i < maxScores; i++)
        {
            if (PlayerPrefs.HasKey($"ScoreName{i}"))
            {
                string name = PlayerPrefs.GetString($"ScoreName{i}");
                int score = PlayerPrefs.GetInt($"ScoreValue{i}");
                scores.Add(new ScoreEntry(name, score));
            }
        }
    }
}

public class ScoreEntry
{
    public string name;
    public int score;

    public ScoreEntry(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}
