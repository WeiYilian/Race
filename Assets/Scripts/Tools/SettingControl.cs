using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingControl : MonoBehaviour
{
    //得到速度控制器和声音控制器
    private Transform systemSettingPanal;
    private Slider speedSlider;
    private Slider audioSlider;
    private Button closeMusic;
    private Button openMusic;

    public AudioSource Adiol;

   private void Start()
   {
        systemSettingPanal = GameObject.Find("Canvas").transform.Find("SystemSettingPanel");
        speedSlider = systemSettingPanal.transform.Find("AdjustRotateSpeed").Find("Slider").gameObject.GetComponent<Slider>();
        audioSlider = systemSettingPanal.transform.Find("AdjustVolume").Find("Slider").gameObject.GetComponent<Slider>();
        closeMusic = systemSettingPanal.transform.Find("CloseMusic").GetChild(0).GetComponent<Button>();
        openMusic = systemSettingPanal.transform.Find("CloseMusic").GetChild(1).GetComponent<Button>();

        transform.Find("SystemSetting").GetComponent<Button>().onClick.AddListener(() =>
        {
            systemSettingPanal.gameObject.SetActive(true);
        });
        
        systemSettingPanal.Find("ApplyBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            systemSettingPanal.gameObject.SetActive(false);
        });
        
        closeMusic.onClick.AddListener(() =>
        {
            AudioManager.Instance.PauseAudio(0);
            openMusic.gameObject.SetActive(true);
            closeMusic.gameObject.SetActive(false);
        });
        
        openMusic.onClick.AddListener(() =>
        {
            AudioManager.Instance.StopAudio(0);
            openMusic.gameObject.SetActive(false);
            closeMusic.gameObject.SetActive(true);
        });
    }
   
    
    // Update is called once per frame
    void Update()
    {
        AudioManager.Instance.transform.GetChild(0).GetComponent<AudioSource>().volume = audioSlider.value;
        Camera.main.GetComponent<ViewController>().rotSpeed = speedSlider.value*100/2;
    }
}
