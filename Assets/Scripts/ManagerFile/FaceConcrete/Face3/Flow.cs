using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow : MonoBehaviour
{
    public bool bloom;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Flow") //花洒
        {
            Debug.Log("ss");
            if (!bloom)
            { Debug.Log("s");
                // animator.Play("Bloom");
                animator.SetBool("Bloom", true);
                bloom = true;
            }
        }
    }
}

    
