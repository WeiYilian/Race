using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 开始场景
/// </summary>
public class StartScene : SceneState
{
    public StartScene() : base("Start") { }

    public override void StateStart()
    {
        if (SceneManager.GetActiveScene().name != "Start"/*如果当前的场景名不为sceneName*/)
        {
            SceneManager.LoadScene("Start");//加载名为sceneName的场景
        }
        AudioManager.Instance.PlayBGMAudio("1");
    }

    public override void StateEnd()
    {
        AudioManager.Instance.StopAudio(0);
    }
}
