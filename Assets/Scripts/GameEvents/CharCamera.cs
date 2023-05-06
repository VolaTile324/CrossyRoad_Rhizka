using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField, Range(0, 1)] private float moveDuration;

    // Start is called before the first frame update
    private void Start()
    {
        offset = this.transform.position;
    }

    public void UpdateCamPosition(Vector3 targetPos)
    {
        DOTween.Kill(this.transform);
        transform.DOMove(offset + targetPos, moveDuration);
    }
}
