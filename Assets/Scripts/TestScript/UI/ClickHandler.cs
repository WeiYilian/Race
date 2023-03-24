using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    // 上一次点击的时间
    private float lastClickTime = 0;

    // 两次点击之间的最大时间间隔
    private float doubleClickInterval = 0.3f;

    // 单击事件回调函数
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.time - lastClickTime < doubleClickInterval)
        {
            // 双击事件
            Debug.Log("DoubleClick!");
        }
        else
        {
            // 单击事件
            Debug.Log("SingleClick!");
        }

        // 更新上一次点击的时间
        lastClickTime = Time.time;
    }
}
