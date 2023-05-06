using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    [SerializeField] int coinValue = 1;
    [SerializeField, Range(0, 100)] float rotationSpeed = 1f;
    // this one is commented out because it's not used
    // [SerializeField, Range(0, 5)] float floatSpeed = 1f;

    public int CoinValue { get => coinValue; }

    public void Collected()
    {
        // be sure to disable it's collision or it will trigger multiple times!
        GetComponent<Collider>().enabled = false;
        // make the coin jump and disappear when collected
        rotationSpeed *= 10;
        this.transform.DOJump(
            this.transform.position,
            2,
            1,
            0.3f
            ).onComplete = CoinSelfDestruct;
    }

    private void CoinSelfDestruct()
    {
        Destroy(this.gameObject);
    }

    void Update()
    {
        // make the coin rotate and float up and down like the usual arcade game items
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        // mathf sin take advantage of the sin wave, it will return a value between -1 and 1
        // 0.1f is the amplitude,
        // 0.5f is the offset (so it won't go below the ground, this means it will float between 0.4f and 0.6f)
        // for now it's disabled for causing issues with the coin jump
        /* transform.position = new Vector3(
            transform.position.x, 
            Mathf.Sin(Time.time * floatSpeed) * 0.1f, 
            transform.position.z
            ); */
    }
}
