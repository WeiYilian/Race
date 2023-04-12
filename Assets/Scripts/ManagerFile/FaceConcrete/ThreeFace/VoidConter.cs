using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VoidConter : MonoBehaviour
{
    public VideoClip[] videos;
    public VideoPlayer videoPlayer;
    public GameObject seefman;//模拟系统
    double video_time, currentTime;
    private Text seedname;
    public Sprite BG;//洋甘菊
    public Sprite BG1;//指甲花
    public Sprite BG2;//车前草
    
    private Image backguand;
    void Start()
    {
        video_time = gameObject.GetComponent<VideoPlayer>().clip.length;
        seedname = GameObject.Find("Canvas").transform.Find("TIP3").transform.Find("种子信息面板").transform
            .Find("nameText/Text").GetComponent<Text>();
        backguand = GameObject.Find("展示框").GetComponent<Image>();
        if (seedname.text=="凤仙花")
        {
            videoPlayer.clip=videos[0];
        }

        if (seedname.text=="洋甘菊")
        {
            videoPlayer.clip=videos[1];
        }

        if (seedname.text == "车前子")
        {
            videoPlayer.clip=videos[2];
        }
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
                backguand.sprite = BG1;
            }
            if (seedname.text=="车前子")
            {
                
                UIFaceManager.Instance.MessageonCtrol("车前子已完成种植");
                backguand.sprite = BG2;
            }
            if (seedname.text=="洋甘菊")
            {
                
                UIFaceManager.Instance.MessageonCtrol("洋甘菊已完成种植");
                backguand.sprite = BG;
            }
           
            //生成完成爆出装备
            
            gameObject.SetActive(false);
            seefman.gameObject.SetActive(true);
        }
    }

}