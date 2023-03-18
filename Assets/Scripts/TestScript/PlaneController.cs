using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // 检测到有物品进入该面时，播放吸附音效或者其他效果
        Debug.Log(other.gameObject.name + " 进入了 " + gameObject.name);
    }
}

