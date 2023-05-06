using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    static HashSet<Vector3> positionSet;
    public static HashSet<Vector3> AllPositions { get => new HashSet<Vector3>(positionSet); }

    public void OnEnable()
    {
        if (positionSet == null)
        {
            positionSet = new HashSet<Vector3>();
        }
        positionSet.Add(transform.position);
    }

    public void OnDisable()
    {
        if (positionSet != null)
        {
            positionSet.Remove(transform.position);
        }
    }
}
