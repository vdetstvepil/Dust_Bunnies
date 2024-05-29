using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KachelyController : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ToughtButtonKachely()
    {
        if (animator != null)
        {
            animator.enabled = true;
            animator.Play("rotation_kachel");
        }
    }
}
