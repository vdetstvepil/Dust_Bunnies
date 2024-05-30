using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationArm : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ToughtArm()
    {
        if (animator != null)
        {
            animator.enabled = true;
            animator.Play("rotation_train_button");
        }
    }

}
