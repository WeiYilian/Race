using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowFaceViscera : UIDragByMocha
{
    private Viscera currentViscera;

    private GameObject Answerbg;
    
    // 针对第二面的所需图片特性进行吸附功能重写
    public override void Adsorption(PointerEventData eventData)
    {
        if (!AdsorptionFunction) return;
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("VisceralPits"))
        {
            GameObject go = eventData.pointerCurrentRaycast.gameObject;
            if (Mathf.Sqrt((transform.position - go.transform.position).magnitude) < adsorptionRange)
            {
                transform.SetParent(go.transform);
                transform.position = go.transform.position;
            }
        }
        
        // 判断脏腑是否到了指定位置
        if (transform.parent == AdsorptionTarget.transform)
        {
            Debug.Log("到了指定位置");
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
        Answerbg = GameObject.Find("Canvas").transform.Find("Answerbg").gameObject;
        
        //  判断问题类型
        ProblemType problemType = currentViscera.ProblemType;
        // 点击后触发答题环节
        GetComponent<Button>().onClick.AddListener(() =>
        {
            Answerbg.gameObject.SetActive(true);
            Debug.Log(transform.name+"类型是："+problemType+"进入答题环节");
            switch (problemType)
            {
                // 触发简答题模式
                case ProblemType.ShortAnswer:
                    GameObject shortAnswerObj = Answerbg.transform.Find("AnswerPanel/ShortAnswer").gameObject;
                    shortAnswerObj.SetActive(true);
                    shortAnswerObj.transform.Find("Question").GetComponent<Text>().text = currentViscera.Matter;
                    break;
                // 触发填空题模式
                case ProblemType.FillVacancy:
                    GameObject fillVacancyObj = Answerbg.transform.Find("AnswerPanel/FillVacancy").gameObject;
                    fillVacancyObj.SetActive(true);
                    fillVacancyObj.transform.Find("Question").GetComponent<Text>().text = currentViscera.Matter;
                    //TODO:未确定填空题答案是由数据库随机字还是固定字的随机顺序，留待考虑
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        });
    }
    
}