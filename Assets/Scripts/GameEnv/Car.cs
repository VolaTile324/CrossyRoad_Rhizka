using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField, Range(0, 5)] float carSpeed = 1f;
    [SerializeField] Light pointLight;
    Vector3 initialPos;
    float distanceLimit = float.MaxValue;

    public void SetDistanceLimit(float distance)
    {
        this.distanceLimit = distance;
    }
    
    private void Start()
    {
        initialPos = this.transform.position;
        // set tag (already set in unity editor prefab, just to show how to do it in code)
        // this.gameObject.tag = "Car";

        // enable or disable point light depending on time of day
        if (PlayerPrefs.HasKey("TimeValue"))
        {
            if (PlayerPrefs.GetInt("TimeValue") == 1)
            {
                pointLight.enabled = true;
            }
            else
            {
                pointLight.enabled = false;
            }
        }
        else
        {
            pointLight.enabled = false;
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * carSpeed * Time.deltaTime);

        if (Vector3.Distance(initialPos, this.transform.position) > this.distanceLimit)
        {
            Destroy(this.gameObject);
        }
    }
}
