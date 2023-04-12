using System;
using System.Collections;
using System.Collections.Generic;
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
    // 填空题已选择的答案空
    private List<Text> selectedAnswer;
    // 填空题可以被选择的答案
    private List<Text> selectAnswer;



    // 针对第二面的所需图片特性进行吸附功能重写
    public override void Adsorption(PointerEventData eventData)
    {
        if (!AdsorptionFunction) return;
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("VisceralPits"))
        {
            GameObject go = eventData.pointerCurrentRaycast.gameObject;
            if (Mathf.Sqrt((transform.position - go.transform.position).magnitude) < adsorptionRange)
            {
                twoFaceManager.JoinVisDesDic(go, transform.parent.gameObject,currentViscera);
                transform.SetParent(go.transform);
                transform.position = go.transform.position;

                if (twoFaceManager.MatchJudgment())
                {
                    GetComponent<Button>().enabled = true;
                    AdsorptionFunction = false;// 如果完成了匹配就不允许再拖动
                }
                    
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
        // 初始化selectedAnswer
        Transform Answers = Answerbg.transform.Find("FillVacancy/Answers");
        Transform AnswerSelect = Answerbg.transform.Find("FillVacancy/AnswerSelect");
        GetAnswer(Answers,selectAnswer);//可选择答案区的Text
        GetAnswer(AnswerSelect,selectedAnswer);//已选择答案区的Text


        #region 问题按钮初始化

        // 点击后触发答题环节
        GetComponent<Button>().onClick.AddListener(() =>
        {
            //if (!twoFaceManager.Matching) return;
            
            Answerbg.gameObject.SetActive(true);
            Debug.Log(transform.name+"类型是："+problemType+"进入答题环节");
            switch (problemType)
            {
                // 触发简答题模式
                case ProblemType.ShortAnswer:
                    GameObject shortAnswerObj = Answerbg.transform.Find("AnswerPanel/ShortAnswer").gameObject;
                    shortAnswerObj.SetActive(true);
                    shortAnswerObj.transform.Find("Question").GetComponent<Text>().text = currentViscera.Matter;
                    //TODO：确认正确答案
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

        #endregion
        
    }

    
    /// <summary>
    /// 遍历子物体并获取Text组件
    /// </summary>
    private void GetAnswer(Transform parentObj,List<Text> list)
    {
        list = new List<Text>();
        foreach (Transform child in parentObj)
        {
            list.Add(child.GetComponent<Text>());
        }
    }
    
    
    
}