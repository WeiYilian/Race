using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Main场景
/// </summary>
public class MainScene : SceneState
{
    public MainScene() : base("Game1") { }

    // ReSharper disable Unity.PerformanceAnalysis
    public override void StateStart()
    {
        if (SceneManager.GetActiveScene().name != "Main"/*如果当前的场景名不为sceneName*/)
        {
            SceneManager.LoadScene("Main");//加载名为sceneName的场景
        }
        Init();
    }

    public override void StateEnd()
    {
        
    }

    public void Init()
    {
        
    }
}
