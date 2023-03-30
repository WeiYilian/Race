using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButenConter : MonoBehaviour
{
    private bool isgame;
    // Start is called before the first frame update
    void Start()
    {
        SliderConter sliderConter = new SliderConter();
        isgame = sliderConter.CanPlayGame;
    }

    // Update is called once per frame
    void Update()
    {
        if (isgame)
        {
            Debug.Log("开始游戏");
        }
        else
        {
            Debug.Log("不能开始游戏");
        }
    }
}
