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
    private Button ReturnManu;
    private Button ExitGame;

    private void Start()
   {
        systemSettingPanal = GameObject.Find("Canvas").transform.Find("SystemSettingPanel");
        ReturnManu = transform.Find("ReturnMainMenu").GetComponent<Button>();
        ExitGame = transform.Find("ExitGame").GetComponent<Button>();
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
        
        ReturnManu.onClick.AddListener(() =>
        {
            //返回主菜单
            AudioManager.Instance.PlayButtonAudio();
            SceneStateController.Instance.SetState(new StartScene());
        }); 
        
        ExitGame.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonAudio();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//在编辑器中退出
#else
            Application.Quit();//在打包之后的退出游戏
#endif
        });
        
        closeMusic.onClick.AddListener(() =>
        {
            AudioManager.Instance.PauseAudio(0);
            openMusic.gameObject.SetActive(true);
            closeMusic.gameObject.SetActive(false);
        });
        
        openMusic.onClick.AddListener(() =>
        {
            AudioManager.Instance.ResumeAudio(0);
            openMusic.gameObject.SetActive(false);
            closeMusic.gameObject.SetActive(true);
        });
    }
   
    
    // Update is called once per frame
    void Update()
    {
        AudioManager.Instance.VolumeScale = audioSlider.value;
        if (!(Camera.main is null)) Camera.main.GetComponent<ViewController>().rotSpeed = speedSlider.value * 100 / 2;
    }
}
