using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMoving : MonoBehaviour
{
    private SplineFollower splineFollower;

    void Start()
    {
        splineFollower = GetComponent<SplineFollower>();
    }

    public void TrainMove()
    {
        if (splineFollower != null)
        {
            splineFollower.enabled = true;
        }
    }
}