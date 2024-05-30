using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrain : MonoBehaviour
{
    private SplineFollower splineFollower;
    [SerializeField] private GameObject gameObject;
    private ParticleSystem particleSystem;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        splineFollower = GetComponent<SplineFollower>();
        animator = gameObject.GetComponent<Animator>();
        particleSystem = gameObject.GetComponent<ParticleSystem>();
    }

    public void TrainMove()
    {
        if (splineFollower != null)
        {
            splineFollower.enabled = true;
        }
    }

    public void CondeyOn()
    {
        if (animator != null)
        { 
            animator.enabled = true;
            animator.Play("condey_on");
        }

        if (particleSystem != null)
        {
            particleSystem.Play();
        }
    }
}
