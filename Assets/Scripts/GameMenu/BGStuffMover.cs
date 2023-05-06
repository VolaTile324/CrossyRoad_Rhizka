using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGStuffMover : MonoBehaviour
{
    // handle everything in background of main menu
    [SerializeField] float objectSpeed = 1f;
    [SerializeField] GameObject obj;
    
    Vector3 initialPos;
    Vector3 initialAngle;

    private void Start()
    {
        // get the initial position of the object
        initialPos = this.transform.position;
        initialAngle = this.transform.eulerAngles;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * objectSpeed * Time.deltaTime);
        if (Vector3.Distance(initialPos, this.transform.position) > 13)
        {
            this.transform.position = initialPos;
        }
    }
}
