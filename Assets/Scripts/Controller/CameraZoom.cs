using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    // // 定义相机最小和最大缩放值以及缩放速度
    // public float minZoom = 2f;
    // public float maxZoom = 20f;
    // public float zoomSpeed = 5f;
    //
    // public float pe=20;
    // // 监听鼠标滚轮事件
    // void Update()
    // {
    //     float scrollDelta = Input.GetAxis("Mouse ScrollWheel")*pe;
    //     if (scrollDelta != 0)
    //     {
    //         // 计算缩放值
    //         float newSize = GetComponent<Camera>().orthographicSize - scrollDelta * zoomSpeed;
    //         newSize = Mathf.Clamp(newSize, minZoom, maxZoom);
    //
    //         // 设置相机缩放值
    //         GetComponent<Camera>().orthographicSize = newSize;
    //     }
    // }
    void Update()
    {
        //鼠标滚轮的效果
        //Camera.main.fieldOfView 摄像机的视野
        //Camera.main.orthographicSize 摄像机的正交投影
        //Zoom out
        //Camera.main【注意需要设置Camera的Tag为MainCamera】
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.fieldOfView <= 1000)
            {
                Camera.main.fieldOfView += 2;
            }

            if (Camera.main.orthographicSize <= 20)
            {
                Camera.main.orthographicSize += 0.5F;
            }
        }
        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.fieldOfView > 2)
            {
                Camera.main.fieldOfView -= 2;
            }

            if (Camera.main.orthographicSize >= 1)
            {
                Camera.main.orthographicSize -= 0.5F;
            }
        }
    }

}
