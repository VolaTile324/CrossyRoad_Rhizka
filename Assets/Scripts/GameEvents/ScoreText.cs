using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text bestScoreText;

    public void Start()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            bestScoreText.text = "Best: " + PlayerPrefs.GetInt("BestScore").ToString();
        }
        else
        {
            bestScoreText.text = "Best: N/A";
        }
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void SaveScore()
    {
        if (PlayerPrefs.GetInt("BestScore") < int.Parse(scoreText.text))
        {
            PlayerPrefs.SetInt("BestScore", int.Parse(scoreText.text));
        }
    }
}
