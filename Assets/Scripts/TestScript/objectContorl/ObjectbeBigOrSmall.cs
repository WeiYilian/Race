using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectbeBigOrSmall : MonoBehaviour
{
    private bool isZoomedIn = false; // 是否已经放大

    // 原始大小和目标大小
    private Vector3 originalScale;
    private Vector3 targetScale;

    // 放大缩小速度
    public float zoomSpeed = 5f;

    void Start()
    {
        // 获取原始大小
        originalScale = transform.localScale;

        // 计算目标大小为原始大小的两倍
        targetScale = originalScale * 10f;
    }

    void Update()
    {
        // 检测鼠标是否点击了该物体，并且是第一次点击
        if (Input.GetMouseButtonDown(0) && !isZoomedIn)
        {
            // 设置为已经放大
            isZoomedIn = true;

            // 开始放大
            StartCoroutine(ScaleObject(transform.localScale, targetScale));
        }
        // 如果已经放大，并且再次点击
        else if (Input.GetMouseButtonDown(0) && isZoomedIn)
        {
            // 设置为未放大
            isZoomedIn = false;

            // 开始缩小
            StartCoroutine(ScaleObject(transform.localScale, originalScale));
        }
    }

    IEnumerator ScaleObject(Vector3 fromScale, Vector3 toScale)
    {
        // 计算放大缩小的差值
        float progress = 0;
        while (progress <= 1)
        {
            progress += Time.deltaTime * zoomSpeed;
            transform.localScale = Vector3.Lerp(fromScale, toScale, progress);
            yield return null;
        }
    }
}

