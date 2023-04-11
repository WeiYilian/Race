using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow : MonoBehaviour
{
    public GameObject flowanmition;//获取摄像机下面花
    public bool bloom;//动画开关
    private Animator animator;//动画组件
    private GameObject flow;
    public GameObject SeedMange;//种子模拟系统
    public GameObject Flow2;//花洒
    void Start()
    {
        //开花的动画和获取浇水
        
        animator = flowanmition.GetComponent<Animator>();
        flow = flowanmition.transform.Find("flow").gameObject;
        
    }


    public void OnClic()
    {
        UIFaceManager.Instance.MessageonCtrol("寻找道具激活种子模拟");
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
                UIFaceManager.Instance.MessageonCtrol("种子模拟已激活");
                ThreeFaceMange.SeedisGame = true;//传入已经激活
                SeedMange.gameObject.SetActive(true);
            }
        }
    }

   
}

    
