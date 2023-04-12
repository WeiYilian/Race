using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VoidConter : MonoBehaviour
{
    double video_time, currentTime;
    private Text seedname;
    void Start()
    {
        video_time = gameObject.GetComponent<VideoPlayer>().clip.length;
        seedname = GameObject.Find("Canvas").transform.Find("TIP3").transform.Find("种子信息面板").transform
            .Find("nameText/Text").GetComponent<Text>();
    }
 
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= video_time)
        {
            Debug.Log(""+seedname);
            //视频播放结束，这里可以写视频播放结束后的事件
            if (seedname.text=="凤仙花")
            {
                UIFaceManager.Instance.MessageonCtrol("凤仙花已完成种植");
            }
           
            //生成完成爆出装备
            
            gameObject.SetActive(false);
        }
    }

}