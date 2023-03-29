using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;


//物体向哪个相邻旋转的方向
public enum DragDirection { UP, DOWN, RIGHT, LEFT }

public class UIDragByMocha : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
{

    [Header("是否拖拽")]
    public bool mIsPrecision; 
    [Header("当前图片所在的面编号")]
    public int CurrentFaceIndex;
    
    
    //存储当前拖拽图片的RectTransform组件
    private RectTransform mRt;

    //盒子每个面移动限制
    private float mFaceLimit;
    //判断图片是否在其他面
    private bool isOtherFace;
    //在下一个面创建的图片
    private GameObject mNewImage;
    //控件所在画布
    private RectTransform canvasRec;
    //控件初始位置
    [HideInInspector]public Vector3 pos;
    //鼠标初始位置（画布空间）
    private Vector2 mousePos;    
    //鼠标初始位置（世界空间）
    private Vector3 mouseWorldPos;

    private void Awake()
    {
        mRt = gameObject.GetComponent<RectTransform>();
        canvasRec = transform.parent.parent.GetComponent<RectTransform>();
    }

    void Start()
    {
        mFaceLimit = 50f;
    }

    private void OnEnable()
    {
        isOtherFace = true;
    }

    #region 拖拽过程

    //开始拖拽触发
    public void OnBeginDrag(PointerEventData eventData)
    {
        //控件所在画布空间的初始位置
        pos = mRt.anchoredPosition;
        //获得当前点击的摄像机
        Camera camera = eventData.pressEventCamera;
        //将屏幕空间鼠标位置eventData.position转换为鼠标在画布空间的鼠标位置
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRec, eventData.position,camera , out mousePos);
        //关闭BlocksRaycasts功能，这样发射的射线可以返回拖动的物体下面一层的东西
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    //拖拽过程中触发
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 newVec = new Vector2();
        Camera camera = eventData.pressEventCamera;
        //将屏幕空间鼠标位置eventData.position转换为鼠标在画布空间的鼠标位置
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRec, eventData.position, camera, out newVec);
        //鼠标移动在画布空间的位置增量
        Vector3 offset = new Vector3(newVec.x - mousePos.x, newVec.y - mousePos.y, 0);
        
        if(mIsPrecision)
            mRt.anchoredPosition = pos + offset;//原始位置增加位置增量即为现在位置
        RangeJudge(mRt.localPosition);
    }

    //结束拖拽触发
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("结束时在"+eventData.pointerCurrentRaycast.gameObject.name);
        transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
    }

    #endregion
    
    
    /// <summary>
    /// 判断图片是否移动到其他面上
    /// </summary>
    /// <param name="distance">图片中心点到面边缘的距离</param>
    /// <param name="imagePos">图片所在位置（相对位置）</param>
    /// <returns></returns>
    private void RangeJudge(Vector3 imagePos)
    {
        #region 判断图片是否旋转到其他面
        
        if (imagePos.y> mFaceLimit && !isOtherFace)
            //Debug.Log("向上转");
            ImageRotOtherFace(DragDirection.UP);


        if (imagePos.y < -mFaceLimit && !isOtherFace)
            //Debug.Log("向下转");
            ImageRotOtherFace(DragDirection.DOWN);
        

        if (imagePos.x > mFaceLimit && !isOtherFace)
            //Debug.Log("向右转");
            ImageRotOtherFace(DragDirection.RIGHT);
        

        if (imagePos.x < -mFaceLimit && !isOtherFace)
            //Debug.Log("向左转");
            ImageRotOtherFace(DragDirection.LEFT);
        
        
        if (Mathf.Abs(imagePos.x)> mFaceLimit || Mathf.Abs(imagePos.y) > mFaceLimit)
            isOtherFace = true;
        else
            isOtherFace = false;

        #endregion
    }

    /// <summary>
    /// 图片旋转到其他面上时进行的操作
    /// </summary>
    /// <param name="dragDirection">旋转方向</param>
    private void ImageRotOtherFace(DragDirection dragDirection)
    {
        //获得图片将要旋转到的面
        UIFaceManager.Instance.GetCurrentUIFace(CurrentFaceIndex,dragDirection,out GameObject mNextFace,out int newFaceIndex);
        transform.SetParent(mNextFace.transform.Find("Panel/bg"));
        transform.localRotation = Quaternion.identity;
        CurrentFaceIndex = newFaceIndex;
        canvasRec = transform.parent.parent.GetComponent<RectTransform>();
    }
}