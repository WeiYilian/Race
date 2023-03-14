using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxZoom : MonoBehaviour
{
    
    //缩放比例限制
    public float MinScale = 0.02f;
    public float MaxScale = 30f;
    //缩放比例
    private float scale = 1f;

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float mouseWheelValue = Input.GetAxis("Mouse ScrollWheel");
            scale += mouseWheelValue;
            scale = Mathf.Clamp(scale, MinScale, MaxScale);

            gameObject.transform.localScale = new Vector3(scale, scale, scale);
        }

    }
}
