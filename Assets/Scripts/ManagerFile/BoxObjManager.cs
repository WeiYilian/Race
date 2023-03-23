using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoxObjManager : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    //鼠标开始拖动物体时的事件
    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    //拖动过程中的事件
    public void OnDrag(PointerEventData eventData)
    {
        //物体的位置等于鼠标的位置
        transform.position = eventData.position;
    }

    //拖动结束时的事件
    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}
