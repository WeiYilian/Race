using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : SceneState
{
    public LoadScene() : base("Loading") { }

    public AsyncOperation mAo;
    
    //加载的进度条
    public Image LoadBar;
    public Text LoadNumber;//加载的进度百分比
    private float WaitTime=0;
    private float AllTime = 100;
    
    public override void StateStart()
    {
        GameObject loadingPanel = GameObject.Find("LoadingPanel");
        LoadBar = loadingPanel.transform.Find("LoadBar/Bar").GetComponent<Image>();
        LoadNumber = loadingPanel.transform.Find("Num").GetComponent<Text>();
        mAo = SceneManager.LoadSceneAsync(mController.MNextSceneState.MSceneName);
        mAo.allowSceneActivation = false;
    }

    public override void StateUpdate()
    {
        WaitTime += Time.deltaTime * 50f;
        LoadBar.fillAmount = WaitTime / AllTime;
        LoadNumber.text = ((int) WaitTime).ToString() + "%";
        if (WaitTime >= AllTime)
        {
            mAo.allowSceneActivation = true;
            mController.SceneState = mController.MNextSceneState;
            mController.LoadOver = true;
           
        }
    }
}
