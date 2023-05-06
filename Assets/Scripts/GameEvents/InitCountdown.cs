using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

public class InitCountdown : MonoBehaviour
{
    [SerializeField] TMP_Text cdText;
    [SerializeField] AudioManager audioManager;
    [SerializeField] AudioClip initSound;
    public UnityEvent OnStart;
    public UnityEvent OnEnd;
    
    // Start is called before the first frame update
    void Start()
    {
        OnStart.Invoke();
        cdText.text = string.Empty;
        Invoke("InitCD", 1);
    }

    void InitCD()
    {
        var cdSequence = DOTween.Sequence();
        cdText.transform.localScale = Vector3.zero;
        cdText.text = "3";
        audioManager.PlaySFX(initSound);
        cdSequence.Append(cdText.transform.DOScale
            (
            Vector3.one,
            1
            ).OnComplete(() =>
            {
                cdText.transform.localScale = Vector3.zero;
                cdText.text = "2";
                audioManager.PlaySFX(initSound);
            }
            ));
        cdSequence.Append(cdText.transform.DOScale
            (
            Vector3.one,
            1
            ).OnComplete(() =>
            {
                cdText.transform.localScale = Vector3.zero;
                cdText.text = "1";
                audioManager.PlaySFX(initSound);
            }
            ));
        cdSequence.Append(cdText.transform.DOScale
            (
            Vector3.one,
            1
            ).OnComplete(() =>
            {
                OnEnd.Invoke();
            }
            ));
    }
}
