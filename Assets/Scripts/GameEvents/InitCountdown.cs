using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

public class InitCountdown : MonoBehaviour
{
    [SerializeField] TMP_Text cdText;
    public UnityEvent OnStart;
    public UnityEvent OnEnd;
    
    // Start is called before the first frame update
    void Start()
    {
        OnStart.Invoke();
        var cdSequence = DOTween.Sequence();
        cdText.transform.localScale = Vector3.zero;
        cdText.text = "3";
        cdSequence.Append(cdText.transform.DOScale
            (
            Vector3.one,
            1
            ).OnComplete(() =>
            {
                cdText.transform.localScale = Vector3.zero;
                cdText.text = "2";
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
