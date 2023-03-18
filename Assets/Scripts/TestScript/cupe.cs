using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cupe : MonoBehaviour
{
   
    
    

        // 定义控制六面体转动的变量
        public float rotationSpeed = 50.0f;
        private bool isRotating = false;
        private Vector3 rotationAxis;

        // 每个面对应的世界
        public GameObject[] worlds;

        // 当六面体旋转时，禁用每个面上的物体以防止穿透
        void DisableWorlds()
        {
            foreach (GameObject world in worlds)
            {
                world.SetActive(false);
            }
        }

        // 当六面体停止旋转时，启用每个面上的物体
        void EnableWorlds()
        {
            foreach (GameObject world in worlds)
            {
                world.SetActive(true);
            }
        }

        // 按下空格键开始/停止旋转
        void Update () {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isRotating = !isRotating;
                rotationAxis = Vector3.up; // 设置旋转轴为Y轴
                DisableWorlds(); // 禁用每个面上的物体
            }

            if (isRotating)
            {
                transform.Rotate(rotationAxis, Time.deltaTime * rotationSpeed);
            }
            else
            {
                EnableWorlds(); // 启用每个面上的物体
            }
        }
    }
    

