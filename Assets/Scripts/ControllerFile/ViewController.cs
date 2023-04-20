using System;
//using TFramework;
using UnityEngine;
using UnityEngine.Serialization;

public class ViewController : MonoBehaviour
{
    private const string InputMouseScrollwheel = "Mouse ScrollWheel";
    private const string InputMouseX = "Mouse X";
    private const string InputMouseY = "Mouse Y";
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    public GameObject box;

    [Header("自动旋转速度")] public float autoRotSpeed;
    [Header("移动速度")] public float posSpeed;
    [Header("旋转速度")] public float rotSpeed;
    [Header("限制上下旋转的角度")] 
    public float rotUpLimit;
    public float rotDownLimit;

    private float limitUp;
    private float limitDown;
    [Header("摄像机当前缩放距离")] public float objDistance;

    

    [Header("缩放速度")] private float zoomSpeed;

    //插值方法实现相机平滑放大缩小
    //相机缩放速度
  
    //变量缩放范围1-10
    private float minZoom;
    private float maxZoom;
    //摄像机与物体之间的标准距离
    private float objCamDistance;
    //平滑程度,理解为过渡时间
    public float smoothTime = 0.2f;
    //储存当前缩放值
    private float targetZoom;
    //平滑速度值
    private Vector3 smoothVelocity = Vector3.zero;

    private void Start()
    {
        //box = UIFaceManager.Instance.UIFaceList[0];
        initObjDistance();
    }

    /// <summary>
    /// 初始化缩放需要的各种变量
    /// 缩放速度改变
    /// </summary>
    private void initObjDistance()
    { 
        //最大与最小范围随物体改变
        minZoom = box.transform.localScale.magnitude / 1.55f;
        maxZoom = box.transform.localScale.magnitude * 2;
        zoomSpeed = (maxZoom + minZoom) / 2;
    }

    

    private void Update()
    {
        if (UIFaceManager.Instance.isGameOver)
        return;
        //摄像机围绕物体旋转
        CameraRotate();
        
        //鼠标滚轮缩放
        UpdateObjDistanceState();
        //鼠标控制旋转
    }

    /// <summary>
    /// 摄像机围绕盒子旋转的方法
    /// </summary>
    private void CameraRotate()
    {
        transform.RotateAround(box.transform.position, Vector3.up, autoRotSpeed * Time.deltaTime); //摄像机自动围绕目标旋转
        var mHROt = -Input.GetAxis(Horizontal);//获取键盘X轴移动
        var mVROt = Input.GetAxis(Vertical);//获取键盘Y轴移动
        var mouseX = Input.GetAxis(InputMouseX);//获取鼠标X轴移动
        var mouseY = -Input.GetAxis(InputMouseY);//获取鼠标Y轴移动
        limitUp = Mathf.Sin(rotUpLimit * Mathf.Deg2Rad) * objDistance;
        limitDown = -Mathf.Sin(rotDownLimit * Mathf.Deg2Rad) * objDistance;
        //按鼠标右键移动摄像机位置
        if (Input.GetMouseButton(1))
        {
            transform.Translate(Vector3.left * (mouseX * posSpeed * Time.deltaTime));
            transform.Translate(Vector3.up * (mouseY * posSpeed * Time.deltaTime));
        }
        
        //WSASD控制摄像机围绕物体旋转
        if(mHROt != 0 || mVROt != 0)
        {
            //限制旋转角度
            if ((transform.position.y >= limitUp && mVROt > 0) || (transform.position.y <= limitDown && mVROt < 0))
                mVROt = 0;

            transform.RotateAround(box.transform.transform.position, Vector3.up, mHROt * rotSpeed * Time.deltaTime);
            transform.RotateAround(box.transform.transform.position, transform.right, mVROt * rotSpeed * Time.deltaTime);
            
        }
    }

    

    /// <summary>
    /// 鼠标滚轮缩放
    /// </summary>
    private void UpdateObjDistanceState()
    {
        float scroll = Input.GetAxis(InputMouseScrollwheel);
        targetZoom -= scroll * zoomSpeed;
        // 限制缩放范围
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        // 使用差值函数平滑缩放
        transform.position = Vector3.SmoothDamp(transform.position, transform.position.normalized * targetZoom, ref smoothVelocity, smoothTime);
        // 获取当前缩放距离
        objDistance = Vector3.Distance(transform.position, box.transform.position) - 100f;
    }
    
}