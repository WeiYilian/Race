using System;
//using TFramework;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    const string INPUT_MOUSE_SCROLLWHEEL = "Mouse ScrollWheel";
    const string INPUT_MOUSE_X = "Mouse X";
    const string INPUT_MOUSE_Y = "Mouse Y";
    private float axisX = 1;      //鼠标沿水平方向移动的增量   
    private float axisY = 1;      //鼠标沿竖直方向移动的增量   

    [Header("是否激活")]
    public bool Activate = true;
    public Transform cameraLocation;
    GameObject ViewObj = null;
    Transform InitObj = null;
    Transform InitObjRoot;
    Transform TempObjUp;
    Transform TempObjLeft;
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

    [Header("旋转速度")]
    public float speed = 2f;      //旋转速度  

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

    bool isReset = false;
    bool IsReset = false;
    float LimitY = 0, LimitX = 0;
    Vector3 MaxDistance = Vector3.zero;
    Vector3 NowPoint = Vector3.zero;
    Vector3 TempPosition;
    Quaternion TempRotation;

    private void Start()
    {
        if (TempObjUp == null)
            TempObjUp = new GameObject("TempUp").transform;
        if (TempObjLeft == null)
            TempObjLeft = new GameObject("TempLeft").transform;

        UnLimitLeftRotate();
    }
    
    private void LateUpdate()
    {
        if (!Activate)
            return;
        UpdateObjDistanceState();

        if (Input.GetMouseButton(0))
        {
            MouseButtonL(null);
        }
        if (Input.GetMouseButtonDown(0))
        {
            MouseButtonDownL(null);
        }
        if (Input.GetMouseButtonDown(1))
        {
            MouseButtonDownR(null);
        }
    }

    #region 旋转：按下鼠标左键时进行的操作

    /// <summary>
    /// 判断是否激活且有无选中物体
    /// </summary>
    /// <param name="obj"></param>
    private void MouseButtonL(object obj)
    {
        if (!Activate || InitObj == null)
            return;
        UpdateObjRotateState();
    }
    
    /// <summary>
    /// 旋转的具体操作
    /// </summary>
    void UpdateObjRotateState()
    {
        axisX = -Input.GetAxis(INPUT_MOUSE_X);
        axisX *= Time.deltaTime * 600 * speed;
        //获得鼠标增量 
        axisY = Input.GetAxis(INPUT_MOUSE_Y);
        axisY *= Time.deltaTime * 600 * speed;
        #region 限制

        LimitY += axisY;
        LimitX += axisX;
        if (LimitY < downEulerLimit && LimitY > upEulerLimit)
            TempObjUp.Rotate(new Vector3(-axisY, 0, 0), Space.Self);
        else
            LimitY -= axisY;

        if (LimitX < mRightEulerLimit && LimitX > mLeftEulerLimit)
            TempObjLeft.Rotate(new Vector3(0, axisX, 0), Space.Self);
        else
            LimitX -= axisX;

        #endregion
        //TempObjLeft.Rotate(new Vector3(0, axisX, 0), Space.Self);
    }
    #endregion

    #region 旋转：抬起鼠标左键时进行的操作

    private void MouseButtonDownL(object obj)
    {
        if (!Activate || InitObj == null)
            return;
        //对鼠标增量初始化
        axisX = 0f; axisY = 0f;
        GameObject mRayObj = GetCameraDetectionObj(10);
        if (mRayObj && ViewObj == null)
        {
            if (mRayObj.tag == "物品查看点")
            {
                ViewObj = mRayObj;
                Init(mRayObj.transform);
                LimitLeftRotate();
                //if (distance < farDistance + (nearDistance - farDistance) / 2)
                distance = farDistance + (nearDistance - farDistance) / 1.1f;
                isReset = true;
            }
        }
    }
    
    void LimitLeftRotate()
    {
        mRightEulerLimit = 60f;
        mLeftEulerLimit = -60f;
        nearDistance = 0.95f;
    }
    #endregion

    #region 选中物体：按下鼠标右键时进行的操作

    private void MouseButtonDownR(object obj)
        {
            if (OffViewObj())
            {
                return;
            }
            MaxDistance = GetGameObjectForwardPoint(cameraLocation, 10);
            GameObject mRayObj = GetCameraDetectionObj(10);
    
            if (mRayObj)
            {
                TempPosition = mRayObj.transform.position;
                TempRotation = mRayObj.transform.rotation;
                InitObjRoot = mRayObj.transform.parent;
                InitObj = mRayObj.transform;
                distance = (nearDistance - farDistance) / 2;
                //Activate = true;
                Init(InitObj);
            }
        }
    
    Vector3 GetGameObjectForwardPoint(Transform transform, float MaxDistance)
    {

        Vector3 Ray;                       //声明一个射线
        RaycastHit Hit;               //用于记录射线碰撞到的物体
        Ray = transform.forward * MaxDistance;
        //Debug.DrawRay(transform.position, Ray,Color.black);
        //物理检测射线，out一个RaycastHit类型的 hitInfo 信息，float distance是射线长度，int layerMask需要转换二进制，所以有如下操作
        if (Physics.Raycast(transform.position, Ray, out Hit, MaxDistance))
        {
            return Hit.point;
        }
        //Debug.Log("距离不够？物体没有碰撞体？");
        return Vector3.zero;
    }
    
    public bool OffViewObj()
        {
            if (InitObj)
            {
                //Activate = false;
                InitObj.position = TempPosition;
                InitObj.rotation = TempRotation;
                InitObj.parent = InitObjRoot;
                //InitObj.GetComponent<ObjectTransformation>().changeObjState(0);
                InitObj = null;
                ViewObj = null;
                //Player.Instance.HideSetting();
                return true;
            }
            return false;
        }
    
    #endregion

    #region 画面缩放：鼠标滚轮操作

    void UpdateObjDistanceState()
    {
        if (Input.GetAxis(INPUT_MOUSE_SCROLLWHEEL) != 0f)
        {
            float delta = Input.GetAxis(INPUT_MOUSE_SCROLLWHEEL);
            float temp = delta * 100 * Time.deltaTime * -zoomSpeed;
            distance -= temp;
            distance = Mathf.Clamp(distance, farDistance, nearDistance);
            if (distance < farDistance + (nearDistance - farDistance) / 2 && isReset)
            {
                Init(InitObj);
                ViewObj = null;
                isReset = false;
                UnLimitLeftRotate();
            }
        }
        NowPoint = Vector3.Lerp(MaxDistance, cameraLocation.position, distance);
        if (Vector3.Distance(TempObjUp.position, NowPoint) > 0.01f)
            TempObjUp.position = Vector3.Lerp(TempObjUp.position, NowPoint, Time.deltaTime * 5f);
        else
            TempObjUp.position = NowPoint;
        if (IsReset)
        {
            TempObjUp.LookAt(cameraLocation);
            ResetTo(TempObjLeft, TempObjUp);
            LimitY = 0;
            LimitX = 0;
        }
        //TempObjUp.position = NowPoint;
    }

    void UnLimitLeftRotate()
    {
        mRightEulerLimit = 179.9f;
        mLeftEulerLimit = -179.9f;
        nearDistance = 0.85f;
    }
    
    #endregion

    /// <summary>
    /// 从屏幕发出一条射线，返回射线碰撞到的的物体
    /// </summary>
    /// <param name="MaxDistance">射线长度</param>
    /// <returns></returns>
    GameObject GetCameraDetectionObj(float MaxDistance)
    {
        Ray camerRay;                       //声明一个射线
        Vector3 mousePos = new Vector3();   //记录将鼠标（因为屏幕坐标没有z，所以下面是将z设为0）
        RaycastHit cameraHit;               //用于记录射线碰撞到的物体
                                            //这里将屏幕坐标的鼠标位置存入一个vector3里面
        mousePos.x = Input.mousePosition.x;
        mousePos.y = Input.mousePosition.y;
        mousePos.z = 0;

        //Ray ray=Camera.main.ScreenPointToRay(Vector3 Pos):返回一条射线由摄像机近裁面发射经过Pos的射线。
        camerRay = Camera.main.ScreenPointToRay(mousePos);
        //物理检测射线，out一个RaycastHit类型的 hitInfo 信息，float distance是射线长度，int layerMask需要转换二进制，所以有如下操作
        if (Physics.Raycast(camerRay, out cameraHit, MaxDistance))
        {
            GameObject go = cameraHit.transform.gameObject; //这是检测到的物体
            if (go != null)
            {
                return go;
            }
        }
        //Debug.Log("距离不够？没有点到物体？物体没有碰撞体？");
        return null;
    }

    private void Init(Transform mObj)
    {
        if (!Activate || InitObj == null)
            return;

        if (TempObjUp.childCount == 0)
            TempObjLeft.parent = TempObjUp;
        if (TempObjLeft.childCount != 0)
            TempObjLeft.GetChild(0).parent = InitObjRoot;
        
        ResetTo(TempObjLeft, TempObjUp);
        TempObjLeft.localScale = Vector3.one;
        Debug.Log(mObj.name);
        ResetTo(TempObjUp, mObj);
        TempObjUp.localScale = Vector3.one;
        TempObjUp.parent = InitObj.parent;
        InitObj.parent = TempObjLeft;
        IsReset = true;
        Invoke("OnInvok", 1f);
    }
    
    /// <summary>
    /// 为延时将IsReset变为false
    /// </summary>
    void OnInvok()
    {
        IsReset = false;
    }
    
    void ResetTo(Transform transform, Transform Target)
    {
        transform.position = Target.position;
        transform.rotation = Target.rotation;
        transform.localScale = Target.localScale;
    }
}