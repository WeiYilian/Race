using System;
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
    public Sprite BG;//甘菊
    public Sprite BG1;//指甲花
    public Sprite BG2;//车前草
    private GameObject temsillder;
    private Image backguand;
    private Transform sunlight;//阳光照射范围
    private Dropdown seaDropdown;//获取下拉框
    private Transform temselect;//温度选择
    private Dropdown sunDropdown;//阳光照射范围下拉选项
    private Transform ChangePanel;//获取选择面板
    private Transform Seasonalselet; //获取季节方块
    void Start()
    {ChangePanel = GameObject.Find("Canvas").transform.Find("TIP3").transform.Find("种植条件选择面板");
        seedname = ChangePanel.transform.Find("nameText/Text").GetComponent<Text>();//种子名称
        Seasonalselet = ChangePanel.transform.Find("季节选择");
        seaDropdown = Seasonalselet.transform.Find("季节").GetComponent<Dropdown>();
        sunlight = ChangePanel.transform.Find("阳光照射");
        sunDropdown = sunlight.transform.Find("照射选择").GetComponent<Dropdown>();
        temselect = ChangePanel.transform.Find("种植温度");
        temsillder = temselect.transform.Find("温度器").gameObject;
        video_time = gameObject.GetComponent<VideoPlayer>().clip.length;
        seedname = GameObject.Find("Canvas").transform.Find("TIP3").transform.Find("种子信息面板").transform
            .Find("nameText/Text").GetComponent<Text>();
        backguand = GameObject.Find("展示框").GetComponent<Image>();
        
    }

  

    void close()
    {
        temsillder.transform.GetComponent<Slider>().value=0;
              
        sunDropdown.value=0;
        seaDropdown.value = 0;
               
        
    }
    // Update is called once per frame
    void Update()
    {
        if (seedname.text=="凤仙花")
        {
            videoPlayer.clip=videos[0];
        }

        if (seedname.text=="甘菊")
        {
            videoPlayer.clip=videos[1];
        }

        if (seedname.text == "车前子")
        {
            videoPlayer.clip=videos[2];
        }
        currentTime += Time.deltaTime;
        if (currentTime >= video_time)
        {
            Debug.Log(""+seedname);
            //视频播放结束，这里可以写视频播放结束后的事件
            if (seedname.text=="凤仙花")
            {
                
                UIFaceManager.Instance.MessageonCtrol("凤仙花已完成种植");
                backguand.sprite = BG1;
                currentTime = 0;
                close();
            }
            if (seedname.text=="车前子")
            {
                
                UIFaceManager.Instance.MessageonCtrol("车前子已完成种植");
                backguand.sprite = BG2;
                currentTime = 0;
                close();
            }
            if (seedname.text=="甘菊")
            {
                
                UIFaceManager.Instance.MessageonCtrol("甘菊已完成种植");
                backguand.sprite = BG;
                GameObject.Find("3/Panel/bg").transform.Find("肝").gameObject.SetActive(true);
                currentTime = 0;
                close();
            }
           
            //生成完成爆出装备
            
            gameObject.SetActive(false);
            seefman.gameObject.SetActive(true);
          
        }
    }

}