using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class CarMoving : MonoBehaviour
{
    private Animator animator;
    private SplineFollower splineFollower;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        splineFollower = GetComponent<SplineFollower>();
    }

    public void CarMove()
    {
        if (animator != null || splineFollower != null)
        {
            animator.enabled = true;
            animator.Play("rotation wheel");
            splineFollower.enabled = true;
        }
    }
}
