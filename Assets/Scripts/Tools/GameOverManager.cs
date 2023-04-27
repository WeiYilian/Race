using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    private Button ReturnManu;
    private Button ExitGame;
    private Text TimeText;
    private void Start()
    {
        ReturnManu = transform.Find("ReturnMainMenu").GetComponent<Button>();
        ExitGame = transform.Find("ExitGame").GetComponent<Button>();
        TimeText = transform.Find("Time").GetComponent<Text>();
        int m = Mathf.FloorToInt(UIFaceManager.Instance.Timer / 60);
        int s = Mathf.FloorToInt(UIFaceManager.Instance.Timer % 60);
        TimeText.text = m + "分" + s + "秒";
        
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
    }
}
