using System;
//using TFramework;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    const string INPUT_MOUSE_SCROLLWHEEL = "Mouse ScrollWheel";
    const string INPUT_MOUSE_X = "Mouse X";
    const string INPUT_MOUSE_Y = "Mouse Y";

    public GameObject Box;
    private GameObject verRotObj;
    private GameObject horRotObj;
    //鼠标增量
    private float mHRot;
    private float mVRot;
    //鼠标灵敏度
    public float mouseSensitivity;
    //限制
    float LimitY = 0, LimitX = 0;
    
    [Header("仰视角限制")]
    [Range(0.01f, 89.9f)]
    public float downEulerLimit = 85f;
    [Header("俯视角限制")]
    [Range(-0.01f, -89.9f)]
    public float upEulerLimit = -85f;

    [Header("右限制")]
    [Range(0.01f, 179.9f)]
    public float mRightEulerLimit = 85f;
    [Header("左限制")]
    [Range(-0.01f, -179.9f)]
    public float mLeftEulerLimit = -85f;
    
    [Header("最远查看距离限制")]
    [Range(0, 1)]
    public float farDistance = 0f;
    [Header("最近查看距离限制")]
    [Range(0, 1)]
    public float nearDistance = 0.8f;
    [Header("当前缩放距离")]
    public float distance = 0.5f;
    [Header("缩放速度")]
    public float zoomSpeed = 0.8f;

    private void Start()
    {
        //创建Box旋转需要的两个空物体
        if (verRotObj == null)
            verRotObj = new GameObject("VerRotObj");
        if (horRotObj == null)
            horRotObj = new GameObject("HorRotObj");
        
        InitBox();
    }

    /// <summary>
    /// 初始化Box位置与层级关系
    /// </summary>
    private void InitBox()
    {
        verRotObj.transform.position = Box.transform.position;
        horRotObj.transform.position = Box.transform.position;
        Box.transform.SetParent(horRotObj.transform);
        horRotObj.transform.SetParent(verRotObj.transform);
    }

    private void Update()
    {
        UpdateObjDistanceState();
        
        if(Input.GetMouseButton(0))
            MouseButtonL();
        if (Input.GetMouseButtonDown(0))
            //TODO:MouseButtonDownL();鼠标滚轮缩放未完成
    }

    /// <summary>
    /// 鼠标左键按下控制旋转
    /// </summary>
    private void MouseButtonL()
    {
        //获得鼠标增量
        mHRot -= Input.GetAxis(INPUT_MOUSE_X) * mouseSensitivity * Time.deltaTime;
        mVRot += Input.GetAxis(INPUT_MOUSE_Y)* mouseSensitivity * Time.deltaTime;
        
        LimitY += mVRot;
        LimitX += mHRot;
        if (LimitY < downEulerLimit && LimitY > upEulerLimit)
            verRotObj.transform.Rotate(new Vector3(-mVRot, 0, 0), Space.Self);
        else
            LimitY -= mVRot;

        if (LimitX < mRightEulerLimit && LimitX > mLeftEulerLimit)
            horRotObj.transform.Rotate(new Vector3(0, mHRot, 0), Space.Self);
        else
            LimitX -= mHRot;
    }

    /// <summary>
    /// 松开鼠标左键，进行鼠标增量初始化
    /// </summary>
    private void MouseButtonDownL()
    {
        mHRot = 0;
        mVRot = 0;
    }

    /// <summary>
    /// 鼠标滚轮缩放
    /// </summary>
    private void UpdateObjDistanceState()
    {
        if (Input.GetAxis(INPUT_MOUSE_SCROLLWHEEL) != 0f)
        {
            distance -= Input.GetAxis(INPUT_MOUSE_SCROLLWHEEL) * 100 * Time.deltaTime * -zoomSpeed;
            distance = Mathf.Clamp(distance, farDistance, nearDistance);
        }
    }
    
}