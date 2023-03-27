using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//物体向哪个相邻旋转的方向
public enum DragDirection { UP, DOWN, RIGHT, LEFT }

public class UIDragByMocha : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
{

    [Header("是否精准拖拽")]
    public bool mIsPrecision;

    //存储图片中心点与鼠标点击点的偏移量
    [HideInInspector]public Vector3 mOffset;
    //存储当前拖拽图片的RectTransform组件
    private RectTransform mRt;
    //存储图片的尺寸
    private Vector2 mImageSize;
    //存储当前图片的Image组件
    private Image mImage;
    //当前图片所在的面
    private UIFace mCurrentFace;
    //图片下一个要旋转的面
    private GameObject mNextFace;
    //用来获得下一个新实例化的图片
    private GameObject mNewImage;
    //盒子每个面移动限制
    private float mFaceLimit;
    //判断物体向哪个面移动
    private DragDirection dragDirection;
    //判断图片是否在其他面
    private bool isOtherFace;

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
        mFaceLimit = 50f;
    }

    private void OnEnable()
    {
        isOtherFace = true;
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

    #region 拖拽过程

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

    #endregion

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
            RangeJudge(mImageSize.x/2,mRt.localPosition);
            //Debug.Log("相对父物体：" + mCurrentFace + "子物体相对位置：" + mRt.localPosition);
            if(isOtherFace && mNewImage)
                mNewImage.GetComponent<UIDragByMocha>().OnDrag(eventData);
        }
    }
    
    /// <summary>
    /// 判断图片是否移动到其他面上
    /// </summary>
    /// <param name="currentFace">当前面的编号</param>
    /// <param name="distance">图片中心点到面边缘的距离</param>
    /// <param name="imagePos">图片所在位置（相对位置）</param>
    /// <returns></returns>
    public void RangeJudge(float distance,Vector3 imagePos)
    {
        #region 判断图片是否旋转到其他面
        
        if (imagePos.y + distance > mFaceLimit)
        {
            //将dragDirection设为向上的
            dragDirection = DragDirection.UP;
            Debug.Log("向上转");
            //获得图片将要旋转到的面
            mNextFace = UIFaceManager.Instance.UIFaceList[mCurrentFace.UpFaceIndex];
            if (!isOtherFace)
            {
                mNewImage = Instantiate(transform.gameObject, 
                    mNextFace.transform.Find("Panel/bg"), true);
                mNewImage.transform.localRotation = Quaternion.identity;
                mNewImage.transform.localPosition = Vector3.zero;
                //mNewImage.GetComponent<UIDragByMocha>().mOffset = mOffset;
            }
            Debug.Log("Image的父物体："+mNewImage.transform.parent.parent.parent.name+"位置:"+mNewImage.transform.position);
        }

        if (imagePos.y - distance < -mFaceLimit)
        {
            //将dragDirection设为向下的
            dragDirection = DragDirection.DOWN;
            Debug.Log("向下转");
            //获得图片将要旋转到的面
            mNextFace = UIFaceManager.Instance.UIFaceList[mCurrentFace.DownFaceIndex];
        }

        if (imagePos.x + distance > mFaceLimit)
        {
            //将dragDirection设为向右的
            dragDirection = DragDirection.RIGHT;
            Debug.Log("向右转");
            //获得图片将要旋转到的面
            mNextFace = UIFaceManager.Instance.UIFaceList[mCurrentFace.RightFaceIndex];
        }

        if (imagePos.x - distance < -mFaceLimit)
        {
            //将dragDirection设为向左的
            dragDirection = DragDirection.LEFT;
            Debug.Log("向左转");
            //获得图片将要旋转到的面
            mNextFace = UIFaceManager.Instance.UIFaceList[mCurrentFace.LeftFaceIndex];
        }
        
        if (Mathf.Abs(imagePos.x) + Mathf.Abs(distance) > mFaceLimit || Mathf.Abs(imagePos.y) + Mathf.Abs(distance) > mFaceLimit)
            isOtherFace = true;
        else
            isOtherFace = false;

        #endregion
    }
}