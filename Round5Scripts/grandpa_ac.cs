using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grandpa_ac : MonoBehaviour
{
    Animator animator;
    public bool isLeft = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        if (isLeft)
        {
            animator.SetBool("Left", true);
            animator.SetBool("Right", false);
        }
        else
        {
            animator.SetBool("Left", false);
            animator.SetBool("Right", true);
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}
