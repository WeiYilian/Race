using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float rotateSpeed = 10f;  // 控制旋转速度
    public float scaleSpeed = 0.1f;  // 控制缩放速度

    void Update()
    {
        // 通过鼠标移动控制Cube旋转
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * rotateSpeed);

        // 通过鼠标滚轮控制Cube缩放
        float scaleFactor = 1f + Input.GetAxis("Mouse ScrollWheel") * scaleSpeed;
        transform.localScale *= scaleFactor;
    }
}
