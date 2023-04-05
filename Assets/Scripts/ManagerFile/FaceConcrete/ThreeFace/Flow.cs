using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow : MonoBehaviour
{
    public bool bloom;
    private Animator animator;
    private GameObject flow;
    public GameObject Flow2;
    void Start()
    {
        //开花的动画和获取浇水
        
        animator = GetComponent<Animator>();
        flow = gameObject.transform.Find("flow").gameObject;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Flow") //花洒
        {
            Debug.Log("ss");
            if (!bloom)
            { Debug.Log("s");
                Destroy(Flow2);
                // animator.Play("Bloom");
                flow.gameObject.SetActive(true);
                Destroy(flow,4f);
               
                
                animator.SetBool("Bloom", true);
                bloom = true;
            }
        }
    }

   
}

    
