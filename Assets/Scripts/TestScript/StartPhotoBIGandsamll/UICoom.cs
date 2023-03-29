using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
//如果没有EventTrigger请添加双击单击代码可以直接调用

//[RequireComponent(typeof(EventTrigger))]
public class UICoom : MonoBehaviour, IPointerClickHandler
{
    public static UICoom instance;
    //上一次点击时间
    private float lastClickTime = 0;
    
    // 两次点击之间的最大时间间隔
    private float doubleClickInterval = 0.3f;
    //放大系数
   
   private GameObject parimage;//父物体
    
    private RectTransform rectTrans; // 要操作的图片的RectTransform组件
    private Vector3 originalPosition; // 原始位置
    private Vector2 originalSize; // 原始大小
    private bool isEnlarged = false; // 是否已经放大
   
    void Start()
    {
        parimage = gameObject.transform.parent.gameObject;
        rectTrans = gameObject.GetComponent<RectTransform>(); // 获取RectTransform组件
        originalPosition = rectTrans.localPosition; // 记录原始位置
        originalSize = rectTrans.sizeDelta; // 记录原始大小
    }

    public void OnPointerClick(PointerEventData eventData)
    {if (Time.time - lastClickTime < doubleClickInterval)
        {
            // 双击事件
            //下面的子物体缩小
            if (isEnlarged==false)
            {
                rectTrans.localPosition = Vector3.zero; // 放大后中心点与UI面板重合
                rectTrans.sizeDelta = parimage.GetComponent<RectTransform>().rect.size; // 放大后与UI面板一样大小

                //缩小image下面的子物体按现在与之前的比值去放大
                foreach (var child in gameObject.GetComponentsInChildren<Transform>())
                {
                    if (child != gameObject.transform)
                    {
                        child.localScale = child.localScale * rectTrans.sizeDelta.x / originalSize.x;
                    }
                }
                isEnlarged = true;
            }


            else
            {
                foreach(var child in gameObject.GetComponentsInChildren<Transform>())
                {
                    if(child != gameObject.transform)
                    {
                        child.localScale =child.localScale*originalSize.x/rectTrans.sizeDelta.x;
                    }
                }
                rectTrans.localPosition = originalPosition; // 缩小回原始位置
                rectTrans.sizeDelta = originalSize; // 缩小回原始大小
                isEnlarged = false;
            }  
            
        }
        else
        {
            

            // 更新上一次点击的时间,记录点击时间
            lastClickTime = Time.time;



        }

    }
   

    
}
