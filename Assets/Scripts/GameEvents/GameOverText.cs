using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverText : MonoBehaviour
{
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] TMP_Text bestScoreInfoText;
    [SerializeField] TMP_Text reasonText;
    private int previousRecord;

    public void Start()
    {
        if (PlayerPrefs.HasKey("TimeValue"))
        {
            if (PlayerPrefs.GetInt("TimeValue") == 1)
            {
                if (PlayerPrefs.HasKey("BestScoreNight"))
                {
                    bestScoreInfoText.text = "Best: " + PlayerPrefs.GetInt("BestScoreNight").ToString();
                    previousRecord = PlayerPrefs.GetInt("BestScoreNight");
                }
                else
                {
                    bestScoreInfoText.text = "No Highscore Yet!";
                }

                if (PlayerPrefs.GetInt("BestScoreNight") == 0)
                {
                    bestScoreInfoText.text = "No Highscore Yet!";
                }
            }
            else
            {
                if (PlayerPrefs.HasKey("BestScoreDay"))
                {
                    bestScoreInfoText.text = "Best: " + PlayerPrefs.GetInt("BestScoreDay").ToString();
                    previousRecord = PlayerPrefs.GetInt("BestScoreDay");
                }
                else
                {
                    bestScoreInfoText.text = "No Highscore Yet!";
                }

                if (PlayerPrefs.GetInt("BestScoreDay") == 0)
                {
                    bestScoreInfoText.text = "No Highscore Yet!";
                }
            }
        }
        else
        {
            bestScoreInfoText.text = "No Highscore Yet!";
        }
    }

    public void UpdateGameOverText(int score)
    {
        gameOverText.text = "Game Over! \n Score: " + score;
    }

    public void UpdateBestScoreInfoText(int newScore)
    {
        if (PlayerPrefs.HasKey("TimeValue"))
        {
            if (PlayerPrefs.GetInt("TimeValue") == 1)
            {
                if (newScore > previousRecord)
                {
                    bestScoreInfoText.text = "New Record!";
                }
                else if (PlayerPrefs.GetInt("BestScoreNight") == 0)
                {
                    bestScoreInfoText.text = "No Highscore Yet!";
                }
                else
                {
                    bestScoreInfoText.text = "Best: " + PlayerPrefs.GetInt("BestScoreNight").ToString();
                }
            }
            else
            {
                if (newScore > previousRecord)
                {
                    bestScoreInfoText.text = "New Record!";
                }
                else if (PlayerPrefs.GetInt("BestScoreDay") == 0)
                {
                    bestScoreInfoText.text = "No Highscore Yet!";
                }
                else
                {
                    bestScoreInfoText.text = "Best: " + PlayerPrefs.GetInt("BestScoreDay").ToString();
                }
            }
        }
        else
        {
            bestScoreInfoText.text = "No Highscore Yet!";
        }
    }

    public void UpdateReasonText(string reason)
    {
        reasonText.text = reason;
    }
}
