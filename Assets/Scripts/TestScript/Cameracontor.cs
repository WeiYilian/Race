using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracontor : MonoBehaviour
{
    //插值方法实现相机平滑放大缩小
    //相机缩放速度
    public float zoomSpeed = 5f;
    //变量缩放范围1-10
    public float minZoom = 1f;
    public float maxZoom = 10f;
    //平滑程度,理解为过度时间
    public float smoothTime = 0.2f;
//储存当前缩放值
    private float targetZoom;
    //平滑速度值
    private Vector3 smoothVelocity = Vector3.zero;

    void Start()
    {
        //先获取当前平滑值存到TArgetzoom 
        targetZoom = transform.position.magnitude;
    }

    void Update () 
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        targetZoom -= scroll * zoomSpeed;

        // 限制缩放范围
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        Debug.Log("max:," +maxZoom);
        Debug.Log("min:," +minZoom);
        Debug.Log("tra:," +targetZoom);
        // 使用差值函数平滑缩放
        transform.position = Vector3.SmoothDamp(transform.position, transform.position.normalized * targetZoom, ref smoothVelocity, smoothTime);
    }
}
