using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDragByMocha : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
{

    [Header("是否精准拖拽")]
    public bool mIsPrecision;

    //存储图片中心点与鼠标点击点的偏移量
    private Vector3 mOffset;
    //存储当前拖拽图片的RectTransform组件
    private RectTransform mRt;
    //存储图片的尺寸
    private Vector2 mImageSize;
    //存储当前图片的Image组件
    private Image mImage;
    //当前图片所在的面
    private UIFace mCurrentFace;

    private void Awake()
    {
        FindFaceIndex();
    }

    void Start()
    {
        //初始化
        mRt = gameObject.GetComponent<RectTransform>();
        mImage = gameObject.GetComponent<Image>();
        mImageSize = mRt.sizeDelta;
    }

    /// <summary>
    /// 初始化查找所属面的编号
    /// </summary>
    /// <returns></returns>
    private void FindFaceIndex()
    {
        Transform FaceParent;
        FaceParent = transform;
        while (true)
        {
            if(FaceParent.parent != null)
                FaceParent = FaceParent.parent;
            else
            {
                Debug.Log("查找不到所属面的编号");
                break;
            }

            if (FaceParent.CompareTag("MainFace"))
            {
                mCurrentFace = new UIFace(Int32.Parse(FaceParent.name));
                break;
            }
        }
    }
    
    
    //开始拖拽触发
    public void OnBeginDrag(PointerEventData eventData)
    {
        //如果精准拖拽则进行计算偏移量操作
        if(mIsPrecision)
        {
            //存储点击时的鼠标坐标
            Vector3 tWorldPos;
            //UI屏幕坐标转换为世界坐标
            RectTransformUtility.ScreenPointToWorldPointInRectangle(mRt, eventData.position, eventData.pressEventCamera, out tWorldPos);
            //计算偏移量
            mOffset = transform.position - tWorldPos;
        }
        //否则 默认偏移量为0
        else
        {
            mOffset = Vector3.zero;
        }
        SetDraggedPosition(eventData);
    }

    //拖拽过程中触发
    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }

    //结束拖拽触发
    public void OnEndDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }

    /// <summary>
    /// 设置图片位置方法
    /// </summary>
    /// <param name="eventData"></param>
    private void SetDraggedPosition(PointerEventData eventData)
    {
        //存储当前鼠标所在位置
        Vector3 globalMousePos;
        //UI屏幕坐标转换为世界坐标
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(mRt, eventData.position, eventData.pressEventCamera, out globalMousePos)) 
        {
            //设置位置及偏移量
            mRt.position = globalMousePos + mOffset;
        }
        
    }
}