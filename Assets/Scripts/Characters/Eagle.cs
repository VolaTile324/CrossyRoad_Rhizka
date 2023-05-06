using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    // target the player
    [SerializeField, Range(0, 30)] float speed = 1f;

    private void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
}
