using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConter : MonoBehaviour
{
    private Transform Plthcama; //获取方块相机

    private Transform Planchange; //土地选择面板
    //获取季节方块

    // Start is called before the first frame update
    void Start()
    {
        Plthcama = GameObject.Find("LandCamera").transform;
        Planchange = GameObject.Find("Canvas").transform.Find("TIP3").transform.Find("种植条件选择面板").transform.Find("土壤选择")
            .transform; //土壤选择面板
    }
    //土壤选择
    private void ShowPlayer (int value)
    {
        for(int i = 0; i < Plthcama.childCount; i++)
        {
            Plthcama.GetChild(i).gameObject.SetActive(false);
        }
       Plthcama.GetChild(value).gameObject.SetActive(true );
    }
}
