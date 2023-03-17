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
    private float MouseX;
    private float mouseY;
    private float mHRot;
    private float mVRot;
    //鼠标灵敏度
    public float mouseSensitivity;
    //是否需要旋转
    private bool isRevolve;

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

    //插值方法实现相机平滑放大缩小
    //相机缩放速度
    public float zoomSpeed1 = 5f;
    //变量缩放范围1-10
    public float minZoom = 1f;
    public float maxZoom = 10f;
    //平滑程度,理解为过度时间
    public float smoothTime = 0.2f;
//储存当前缩放值
    private float targetZoom;
    //平滑速度值
    private Vector3 smoothVelocity = Vector3.zero;
    private void Start()
    {  //先获取当前平滑值存到TArgetzoom 
        targetZoom = transform.position.magnitude;
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
        //鼠标滚轮缩放未完成
        UpdateObjDistanceState();
        if(Input.GetMouseButton(0))
            MouseButtonL();
        if(Input.GetMouseButtonUp(0))
            MouseButtonUpL();
        
        BoxRotation();
    }

    /// <summary>
    /// 鼠标左键按下控制旋转
    /// </summary>
    private void MouseButtonL()
    {
        isRevolve = true;
        //获得鼠标增量
        MouseX = Input.GetAxis(INPUT_MOUSE_X);
        mouseY = Input.GetAxis(INPUT_MOUSE_Y);
        mHRot -= MouseX * mouseSensitivity * Time.deltaTime;
        mVRot -= mouseY * mouseSensitivity * Time.deltaTime;
        if(Mathf.Abs(MouseX) < 0.1f)
            mHRot = Mathf.Lerp(mHRot, 0, 0.01f);
        if(Mathf.Abs(mouseY) < 0.1f)
            mVRot = Mathf.Lerp(mVRot, 0, 0.01f);
    }

    /// <summary>
    /// 抬起鼠标左键时的操作
    /// </summary>
    private void MouseButtonUpL()
    {
        isRevolve = false;
    }

    /// <summary>
    /// 盒子旋转
    /// </summary>
    public void BoxRotation()
    {
        verRotObj.transform.Rotate(new Vector3(mVRot, 0, 0), Space.Self); 
        horRotObj.transform.Rotate(new Vector3(0, mHRot, 0), Space.Self);

        if (!isRevolve)
        {
            mHRot = Mathf.Lerp(mHRot, 0, 0.1f);
            mVRot = Mathf.Lerp(mVRot, 0, 0.1f);
        }
    }

    /// <summary>
    /// 鼠标滚轮缩放
    /// </summary>
    private void UpdateObjDistanceState()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        targetZoom -= scroll * zoomSpeed;

        // 限制缩放范围
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);

        // 使用差值函数平滑缩放
        transform.position = Vector3.SmoothDamp(transform.position, transform.position.normalized * targetZoom, ref smoothVelocity, smoothTime);
    }
    
}