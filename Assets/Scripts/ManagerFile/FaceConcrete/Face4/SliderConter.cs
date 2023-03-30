using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderConter : MonoBehaviour
{
   private bool canPlayGame;
    //滑动温度在45-50即0.45到0.5之间可以触发按钮，否则按钮触发不了可以写一个状态当滑块到0.45到0.5时可以开始按下按钮
    // Start is called before the first frame update
    
//一个判断是否滑动方法

    private void Start()
    {
        canPlayGame = false;
    }


    public void OnValueChanged()
    {
      float  value = gameObject.transform.GetComponent<Slider>().value;
        if(value >= 40f && value <= 50f)
        {
            canPlayGame = true;
            Debug.Log("可以按下按钮");
        }
        else
        {
            canPlayGame = false;
            Debug.Log("不能按下按钮");
        }
    }

    private void Update()
    {
        OnValueChanged();
    }
public bool CanPlayGame
    {
       get{ return canPlayGame;}
    }
}
