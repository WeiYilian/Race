using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowFaceViscera : UIDrag
{
    // 第二个面管理者
    private TwoFaceManager twoFaceManager;
    // 当前的脏腑对应的Viscera
    private Viscera currentViscera;
    // 问题的UI物体
    private GameObject Answerbg;
    

    // 针对第二面的所需图片特性进行吸附功能重写
    public override void Adsorption(PointerEventData eventData)
    {
        if (!AdsorptionFunction) return;
        GameObject go = eventData.pointerCurrentRaycast.gameObject;
        if (go.name != "内圆盘")
            transform.SetParent(go.transform);
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("VisceralPits"))
        {
            if (Mathf.Sqrt((transform.position - go.transform.position).magnitude) < adsorptionRange)
            {
                twoFaceManager.JoinVisDesDic(go, transform.parent.gameObject,currentViscera);
                transform.SetParent(go.transform);
                transform.position = go.transform.position;
                twoFaceManager.MatchJudgment();
            }
        }
    }

    // 初始化
    public override void TwoFaceButInit()
    {
        // 获取TwoFaceManager的对象
        twoFaceManager = UIFaceManager.Instance.GetTwoFaceManager();
        //  获得Viscera
        currentViscera = twoFaceManager.GetViscera(transform.name);
        // 获取答题面板
        Answerbg = GameObject.Find("Canvas").transform.Find("Answerbg").gameObject;
        //  判断问题类型
        ProblemType problemType = currentViscera.ProblemType;
        


        #region 问题按钮初始化

        // 点击后触发答题环节
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (!twoFaceManager.Matching || currentViscera.AnswerOver) return;

            twoFaceManager.CurAnsViscera = currentViscera;
            
            Answerbg.gameObject.SetActive(true);
            GameObject shortAnswerObj = Answerbg.transform.Find("ShortAnswer").gameObject;
            GameObject fillVacancyObj = Answerbg.transform.Find("FillVacancy").gameObject;
            //Debug.Log(transform.name+"类型是："+problemType+"进入答题环节");
            switch (problemType)
            { 
                // 触发简答题模式
                case ProblemType.ShortAnswer:
                    shortAnswerObj.SetActive(true);
                    fillVacancyObj.SetActive(false);
                    break;
                // 触发填空题模式
                case ProblemType.FillVacancy:
                    shortAnswerObj.SetActive(false);
                    fillVacancyObj.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        });

        #endregion
    }

}