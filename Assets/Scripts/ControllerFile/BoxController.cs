using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BoxController : MonoBehaviour
{
    private GameObject m_Box;

    //水平旋转值
    private float m_HRot;
    //竖直旋转值
    private float m_VRot;

    public float MouseSensitivity;
    
    void Start()
    {
        
    }

    private void Update()
    {
        RotationBox();
    }

    private void RotationBox()
    {
        m_HRot -= Input.GetAxis("Mouse X") * MouseSensitivity;
        m_VRot += Input.GetAxis("Mouse Y") * MouseSensitivity;
        if (Input.GetMouseButton(0))
        {
            transform.rotation = Quaternion.Euler(m_VRot, m_HRot, 0);
        }
    }
}
