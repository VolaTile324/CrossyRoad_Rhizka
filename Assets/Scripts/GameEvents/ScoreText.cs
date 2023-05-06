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
        if (PlayerPrefs.HasKey("TimeValue"))
        {
            if (PlayerPrefs.GetInt("TimeValue") == 1)
            {
                if (PlayerPrefs.HasKey("BestScoreNight"))
                {
                    bestScoreText.text = "Best: " + PlayerPrefs.GetInt("BestScoreNight").ToString();
                }
                else
                {
                    bestScoreText.text = "Best: N/A";
                }

                if (PlayerPrefs.GetInt("BestScoreNight") == 0)
                {
                    bestScoreText.text = "Best: N/A";
                }
            }
            else
            {
                if (PlayerPrefs.HasKey("BestScoreDay"))
                {
                    bestScoreText.text = "Best: " + PlayerPrefs.GetInt("BestScoreDay").ToString();
                }
                else
                {
                    bestScoreText.text = "Best: N/A";
                }

                if (PlayerPrefs.GetInt("BestScoreDay") == 0)
                {
                    bestScoreText.text = "Best: N/A";
                }
            }
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
        if (PlayerPrefs.HasKey("TimeValue"))
            {
            if (PlayerPrefs.GetInt("TimeValue") == 1)
            {
                if (PlayerPrefs.GetInt("BestScoreNight") < int.Parse(scoreText.text))
                {
                        PlayerPrefs.SetInt("BestScoreNight", int.Parse(scoreText.text));
                }
            }
            else
            {
                if (PlayerPrefs.GetInt("BestScoreDay") < int.Parse(scoreText.text))
                {
                    PlayerPrefs.SetInt("BestScoreDay", int.Parse(scoreText.text));
                }
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("BestScoreDay") < int.Parse(scoreText.text))
            {
                PlayerPrefs.SetInt("BestScoreDay", int.Parse(scoreText.text));
            }
        }
    }
}
