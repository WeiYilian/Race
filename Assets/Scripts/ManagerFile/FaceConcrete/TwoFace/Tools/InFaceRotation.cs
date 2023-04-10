using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InFaceRotation : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
{
    // 存储五行小元素的列表
    public List<RectTransform> WuXing = new List<RectTransform>();
    
    public Queue<string> wuxingQueue = new Queue<string>();

    private Vector2 ModelPos;
    //当前鼠标位置
    private Vector2 mousePos;
    //上一帧鼠标位置
    private Vector2 premousePos;
    private float RotateAngle;
    private Quaternion q; 
    //模型欧拉角存储变量
    private Vector3 localEluer;
    // 旋转速度
    public float rotSpeed;
    // 内盘上每个端点相距的角度
    private int distanceAngle;
    // 上次更新的点位
    private int point = 0;
    

    //控件所在画布
    private RectTransform canvasRec;

    private void Start()
    {
        canvasRec = UIFaceManager.Instance.UIFaceList[2].GetComponent<RectTransform>();
        distanceAngle = 360 / 5;
        for (int i = 0; i < WuXing.Count; i++)
        {
            wuxingQueue.Enqueue(WuXing[i].name);
        }
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
            Debug.Log(transform.localEulerAngles);
            for (int i = 0; i < WuXing.Count; i++)
            {
                //对五个小元素进行反方向旋转，以保证小元素不旋转
                WuXing[i].transform.localEulerAngles = -localEluer;
            }
            premousePos = mousePos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 获取自身欧拉角的z值
        var transformRotation = transform.GetComponent<RectTransform>().rotation.eulerAngles.z;
        //  若与点位的值小于10则吸附至点位
        if (Mathf.Abs(transformRotation) % distanceAngle < 10)
        {
            transform.localEulerAngles = new Vector3(0,0,GetAngles(transformRotation));
            UpdataQuene((int)transformRotation);
        }
            
        UIFaceManager.Instance.GetTwoFaceManager().MatchJudgment();
    }
    
    /// <summary>
    /// 计算角度值是否位于端点附近
    /// </summary>
    private float GetAngles(float angles)
    {
        int a = 0;
        int boundary = distanceAngle / 2;
        //若余数未超过35度则取余，若余数超过35度则用70度减去余数
        if (angles % distanceAngle > boundary)
            a = (int) (angles / distanceAngle) + 1;
        else if(angles%distanceAngle <= boundary)
            a = (int) (angles / distanceAngle);

        return a * distanceAngle;
    }
    
    /// <summary>
    /// 更新队列
    /// 金对应肺，木对应肝，水对应肾，火对应心，土对应脾
    /// </summary>
    private void UpdataQuene(int angles)
    {
        int i = angles / distanceAngle;
        // 更新五行队列顺序
        for (int j = 0; j < i-point; j++)
        {
            string obj = wuxingQueue.Dequeue();
            wuxingQueue.Enqueue(obj);
        }
        point = i;
    }
}
