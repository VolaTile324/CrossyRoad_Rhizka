using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverText : MonoBehaviour
{
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] TMP_Text bestScoreInfoText;
    [SerializeField] TMP_Text reasonText;

    public void Start()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            bestScoreInfoText.text = "Best: " + PlayerPrefs.GetInt("BestScore").ToString();
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
        if (PlayerPrefs.HasKey("BestScore"))
        {
            bestScoreInfoText.text = "Best: " + PlayerPrefs.GetInt("BestScore").ToString();
        }
        else if (!PlayerPrefs.HasKey("BestScore"))
        {
            bestScoreInfoText.text = "No Highscore Yet!";
        }
        else
        {
            bestScoreInfoText.text = "No Highscore Yet!"; // this is most likely unused, but just in case
        }
    }

    public void UpdateReasonText(string reason)
    {
        reasonText.text = reason;
    }
}
