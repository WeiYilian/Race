using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private SceneStateController contorl ;
    private void Awake()
    {
        //避免重复的同一个物体
        if (GameObject.Find("GameLoop")!=this.gameObject)
            Destroy(this.gameObject);
        //场景切换不销毁
        DontDestroyOnLoad(this.gameObject);
       
        
    }
    private void Start()
    {
        contorl = new SceneStateController();
        //进入场景不需要加载
        contorl.SetState(new StartScene(), false);
        
    }
    private void Update()
    {
        //在不同状态下需要更新的数据是不一样的
        if (SceneStateController.Instance != null)
        {
           
            //状态更新
            SceneStateController.Instance.StateUpdate();
        }
    } 
}
