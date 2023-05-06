using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTutor : MonoBehaviour
{
    [SerializeField] private GameObject tutor;
    [SerializeField] private GameObject duck;
    private void Update()
    {
        if (Vector3.Distance(tutor.transform.position, duck.transform.position) > 10f)
        {
            Destroy(tutor);
        }
    }
}
