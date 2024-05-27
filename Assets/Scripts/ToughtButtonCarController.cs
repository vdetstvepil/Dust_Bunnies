using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToughtButtonCarController : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ToughtButton()
    {
        if (animator != null)
        {
            animator.enabled = true;
            animator.Play("button");
        }
    }
}