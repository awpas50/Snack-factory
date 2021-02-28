using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [HideInInspector] public GameObject startPoint;
    [HideInInspector] public GameObject endPoint;

    LineRenderer lr;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, startPoint.transform.position);
        lr.SetPosition(1, endPoint.transform.position);
    }
}
