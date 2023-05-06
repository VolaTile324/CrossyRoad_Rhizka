using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameplaySetting : MonoBehaviour
{
    [SerializeField] Toggle timeSetting;
    [SerializeField] TMP_Text dayLabel;
    [SerializeField] TMP_Text nightLabel;
    [SerializeField] Image dayExample;
    [SerializeField] Image nightExample;

    // called when the panel opened up for the first time
    public void InitialSetting()
    {
        if (PlayerPrefs.HasKey("TimeValue"))
        {
            if (PlayerPrefs.GetInt("TimeValue") == 1)
            {
                timeSetting.isOn = true;
                dayLabel.color = Color.gray;
                nightLabel.color = Color.green;
                nightExample.gameObject.SetActive(true);
                dayExample.gameObject.SetActive(false);
            }
            else
            {
                timeSetting.isOn = false;
                dayLabel.color = Color.green;
                nightLabel.color = Color.gray;
                nightExample.gameObject.SetActive(false);
                dayExample.gameObject.SetActive(true);
            }
        }
        else
        {
            timeSetting.isOn = false;
            dayLabel.color = Color.green;
            nightLabel.color = Color.gray;
            nightExample.gameObject.SetActive(false);
            dayExample.gameObject.SetActive(true);
        }
    }
    
    // on value change, a function to read isOn
    public void SetTime(bool value)
    {
        if (value == true)
        {
            PlayerPrefs.SetInt("TimeValue", 1);
            dayLabel.color = Color.gray;
            nightLabel.color = Color.green;
            nightExample.gameObject.SetActive(true);
            dayExample.gameObject.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("TimeValue", 0);
            dayLabel.color = Color.green;
            nightLabel.color = Color.gray;
            nightExample.gameObject.SetActive(false);
            dayExample.gameObject.SetActive(true);
        }
    }
}
