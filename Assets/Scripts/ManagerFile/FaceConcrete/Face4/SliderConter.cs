using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderConter : MonoBehaviour
{
    private static SliderConter _instance;
   // private bool canPlayGame;
    //滑动温度在45-50即0.45到0.5之间可以触发按钮，否则按钮触发不了可以写一个状态当滑块到0.45到0.5时可以开始按下按钮
    // Start is called before the first frame update
   
//一个判断是否滑动方法
    private float value;
    private void Start()
    {
      
       
    }

    public static SliderConter Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SliderConter();
                
            }
            return _instance;
        }
    }
   
   public void OnValueChanged()
    {
        value = gameObject.transform.GetComponent<Slider>().value;
        if(value >= 40f && value <= 50f)
        {
            
            
            
            ButtenMana buttenMana = new ButtenMana();
            
            buttenMana.Game(true);
        }
        else
        {
            ButtenMana buttenMana = new ButtenMana();
            buttenMana.Game(false);
           
        }
    }

    private void Update()
    {
        OnValueChanged();

    }

}
