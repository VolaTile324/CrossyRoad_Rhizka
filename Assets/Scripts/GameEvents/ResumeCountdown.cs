using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ResumeCountdown : MonoBehaviour
{
    [SerializeField] TMP_Text resumeCDText;
    [SerializeField] AudioManager audioManager;
    [SerializeField] AudioClip resumeSound;
    public UnityEvent OnCompleteCountdown;

    public void InitResumeCD()
    {
        resumeCDText.text = "3";
        audioManager.PlaySFX(resumeSound);
        StartCoroutine(CountdownToTwo());
    }

    IEnumerator CountdownToTwo()
    {
        yield return new WaitForSecondsRealtime(1);
        resumeCDText.text = "2";
        audioManager.PlaySFX(resumeSound);
        StartCoroutine(CountdownToOne());
    }

    IEnumerator CountdownToOne()
    {
        yield return new WaitForSecondsRealtime(1);
        resumeCDText.text = "1";
        audioManager.PlaySFX(resumeSound);
        StartCoroutine(CountdownFinish());
    }

    IEnumerator CountdownFinish()
    {
        yield return new WaitForSecondsRealtime(1);
        OnCompleteCountdown.Invoke();
    }
}
