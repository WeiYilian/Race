using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowFaceViscera : UIDragByMocha
{
    private Viscera currentViscera;

    private GameObject AnsweiPanel;
    
    // 针对第二面的所需图片特性进行吸附功能重写
    public override void Adsorption()
    {
       
        base.Adsorption();
        // 判断脏腑是否到了指定位置
        if (transform.parent == AdsorptionTarget.transform)
        {
            //Debug.Log("到了指定位置");
            // 遍历找到指定的Viscera
            //Debug.Log("找到相对应的Viscera");
            // 移动到指定位置后可以进行下一步了，将是否可以进行下一步的bool值改成true
            currentViscera.IsGivenPos = true;
            // 将Button打开以进行下一关
            GetComponent<Button>().enabled = true;
            // 将自身给Viscera中的VisceraObj
            currentViscera.VisceraObj = gameObject;
            // 移动到指定位置后不能在进行移动，将是否可以移动的bool值改成false
            IsPrecision = false;

           
        }
    }

    public override void TwoFaceButInit()
    {
        // 获取TwoFaceManager的对象
        TwoFaceManager twoFaceManager = UIFaceManager.Instance.GetTwoFaceManager();
        //  获得Viscera
        currentViscera = twoFaceManager.GetViscera(transform.name);
        // 获取答题面板
        AnsweiPanel = GameObject.Find("Canvas").transform.Find("Answeibg/AnswerPanel").gameObject;
        
        //  判断问题类型
        ProblemType _problemType = currentViscera.ProblemType;
        // 点击后触发答题环节
        GetComponent<Button>().onClick.AddListener(() =>
        {
            //Debug.Log(transform.name+"进入答题环节");
            switch (_problemType)
            {
                // 触发简答题模式
                case ProblemType.ShortAnswer:
                    AnsweiPanel.transform.Find("简答题").gameObject.SetActive(true);
                    break;
                // 触发填空题模式
                case ProblemType.FillVacancy:
                    AnsweiPanel.transform.Find("填空题").gameObject.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        });
    }
    
}
