using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Transform itemManager;  // 物品所属的Item Manager对象
    private float distance = 10f;  // 物品距离Cube的最小距离
    private bool isDragging = false;  // 是否正在拖拽
    private Vector3 offset = Vector3.zero;  // 鼠标拖拽偏移量

    void OnMouseDown()
    {
        isDragging = true;

        // 计算鼠标拖拽偏移量
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = distance;
        offset = Camera.main.WorldToScreenPoint(transform.position) - mousePos;

        // 将物品从原来的面中移除
        transform.parent = itemManager;
    }

    void OnMouseUp()
    {
        isDragging = false;

        // 将物品吸附到最近的面上
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, distance))
        {
            transform.parent = hit.collider.transform;
            transform.localPosition = Vector3.zero;
            Debug.Log("物品被吸附在 " + hit.collider.gameObject.name + " 上");
        }
    }

    void Update()
    {
        if (isDragging)
        {
            // 计算鼠标拖拽偏移量
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = distance;
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos + offset);

            // 移动物品到目标位置
            transform.position = targetPos;
        }
    }
}
