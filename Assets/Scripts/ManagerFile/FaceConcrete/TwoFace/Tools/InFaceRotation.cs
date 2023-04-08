using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InFaceRotation : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
{
    public List<RectTransform> WuXing = new List<RectTransform>();

    private Vector2 ModelPos;
    //当前鼠标位置
    private Vector2 mousePos;
    //上一帧鼠标位置
    private Vector2 premousePos;
    private float RotateAngle;
    private Quaternion q; 
    //模型欧拉角存储变量
    private Vector3 localEluer;

    public float rotSpeed;
    

    //控件所在画布
    private RectTransform canvasRec;

    private void Start()
    {
        canvasRec = UIFaceManager.Instance.UIFaceList[2].GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 获得当前点击的摄像机
        Camera camera = eventData.pressEventCamera;
        //ModelPos = Camera.main.WorldToScreenPoint(transform.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRec, eventData.position,camera , out ModelPos);
        // 将屏幕空间鼠标位置eventData.position转换为鼠标在画布空间的鼠标位置
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRec, eventData.position,camera , out mousePos);
        //每次重新点击的时候都重置鼠标上一帧点击坐标
        premousePos = mousePos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Camera camera = eventData.pressEventCamera;
        // 将屏幕空间鼠标位置eventData.position转换为鼠标在画布空间的鼠标位置
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRec, eventData.position, camera, out mousePos);
        //获取旋转角度
        RotateAngle = Vector2.Angle(premousePos - ModelPos, mousePos - ModelPos);
        
        if (RotateAngle == 0)
        {
            premousePos = mousePos;
        }
        else
        {
            //创建从上一角度到现在角度的旋转四元数
            q = Quaternion.FromToRotation(premousePos - ModelPos, mousePos - ModelPos);
            //判断顺时针旋转或者逆时针旋转
            float k = q.z > 0 ? 1 : -1;
            //存储欧拉角
            localEluer.z += k * RotateAngle * rotSpeed;
            //进行旋转
            transform.localEulerAngles = localEluer;
            for (int i = 0; i < WuXing.Count; i++)
            {
                WuXing[i].transform.localEulerAngles = -localEluer;
            }
            premousePos = mousePos;
        }

        if (Mathf.Abs(localEluer.z) % 70 < 5)
        {
            var transformRotation = transform.rotation;
            transformRotation.z = GetAngles(transform.localEulerAngles.z);
        }
    }

    /// <summary>
    /// 计算角度值
    /// </summary>
    private float GetAngles(float Angles)
    {
        float a;
        a = (Angles % 70) < 35 ? (Angles % 70) : (70 - Mathf.Abs(Angles % 70));
        if ((Angles % 70) < 35)
        {
            a = (Angles % 70);
            return Angles - a;
        }
        else
        {
            a = 70 - Mathf.Abs(Angles % 70);
            if (Angles > 0)
                return Angles + a;
            else
                return Angles - a;
        }
    }
    

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}
