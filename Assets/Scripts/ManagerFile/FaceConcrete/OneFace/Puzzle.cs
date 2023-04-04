using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum PuzzleType
{
    OneJigsawPuzzle,
    TwoJigsawPuzzle
}

public class Puzzle : MonoBehaviour, IPointerClickHandler
{
    // 判断是否可以进行大拼图切换
    public bool exchangePuzzle;
    // 判断是否可以放大拼图
    public bool magnifyPuzzle;
    // 拼图需要达到的目标位置索引
    public int jpTargetIndex;
    // 拼图的类型
    public PuzzleType jpType;
    // 当前位置编号
    public int jpCurIndex;
    // 判断是否拼图成功
    private bool isSucceed;
    //上一次点击时间
    private float lastClickTime;
    // 两次点击之间的最大时间间隔
    private float clickInterval;
    //记录原来信息，方便回退
    [HideInInspector] public Vector3 originalPos;
    [HideInInspector] public Vector2 originalSize;
    
    // OneFaceManager
    private OneFaceManager oneFaceManager;


    private void Start()
    {
        Init();
    }

    // 初始化
    private void Init()
    {
        // 获取OneFaceManager
        oneFaceManager = UIFaceManager.Instance.GetOneFaceManager();
        // 将自身加进字典中
        oneFaceManager.PuzzleDic[jpCurIndex] = this;
        // 初始化时间间隔
        clickInterval = 0.3f;
    }
    

    /// <summary>
    /// 大拼图切换
    /// </summary>
    /// <param name="eventData"></param>
    private void ExchangeBigPuzzle(PointerEventData eventData)
    {
        if (!exchangePuzzle) return;
        if (!oneFaceManager.InitialObj)
        {
            //如果未进行第一次点击，就记录第一次点击的物体
            oneFaceManager.InitialObj = eventData.pointerCurrentRaycast.gameObject;
            // 如果是双击则不记录
            if (Time.time - lastClickTime < clickInterval)
                oneFaceManager.InitialObj = null;
            //TODO:同时使第一个点击的物体触发点击特效
        }
        else
        {
            // 存放第一次点击的物体位置
            Vector3 firstPos = oneFaceManager.InitialObj.transform.position;
            // 存放第二次点击的物体位置
            Vector3 secondPos = eventData.pointerCurrentRaycast.gameObject.transform.position;
            // 两个物体之间的位置切换
            oneFaceManager.InitialObj.transform.position = secondPos;
            secondPos = firstPos;
            eventData.pointerCurrentRaycast.gameObject.transform.position = secondPos;
            // 两个物体之间的父物体切换
            GameObject parentObj = transform.parent.gameObject;
            transform.SetParent(oneFaceManager.InitialObj.transform.parent);
            oneFaceManager.InitialObj.transform.SetParent(parentObj.transform);
            // 对Puzzle的属性进行交换
            oneFaceManager.ExchangePuzzleCharacteristic(eventData.pointerCurrentRaycast.gameObject.GetComponent<Puzzle>());
            // 切换完成，释放第一次点击后存储的对象
            oneFaceManager.InitialObj = null;
        }
    }

    
    /// <summary>
    /// 大小拼图切换
    /// </summary>
    /// <param name="eventData"></param>
    private void ExchangeSmallPuzzle(PointerEventData eventData)
    {
        if (!magnifyPuzzle) return;
        // 判断两次点击时间是否超过clickInterval
        if (Time.time - lastClickTime < clickInterval)
        {
            // 放大之后设置为不能再次放大，可以与其他拼图交换
            magnifyPuzzle = false;
            exchangePuzzle = true;
            // 记录父物体(大拼图)
            GameObject parentObj = transform.parent.gameObject;
            // 记录初始信息
            originalPos = transform.localPosition;
            originalSize = transform.GetComponent<RectTransform>().sizeDelta;
            // 将小拼图的相对位置设置在中心
            transform.localPosition = Vector3.zero;
            // 将小拼图变大
            transform.GetComponent<RectTransform>().sizeDelta = parentObj.GetComponent<RectTransform>().rect.size;
        }
        else
        {
            // 更新上一次点击的时间,记录点击时间
            lastClickTime = Time.time;
        }
    }
    
    
    // 点击事件
    public void OnPointerClick(PointerEventData eventData)
    {
        ExchangeBigPuzzle(eventData);
        ExchangeSmallPuzzle(eventData);
    }
}
