using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowFaceViscera : UIDragByMocha
{
    // 第二个面管理者
    private TwoFaceManager twoFaceManager;
    // 当前的脏腑对应的Viscera
    private Viscera currentViscera;
    // 问题的UI物体
    private GameObject Answerbg;

    private List<Text> selectedAnswer;
    
    private List<Text> selectAnswer;
    
    private List<string> correctAnswer;

    
    
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
    }

    
    /// <summary>
    /// 遍历子物体并赋值
    /// </summary>
    private void GetAnswer(GameObject parentObj,bool isSelect = true)
    {
        foreach (Transform child in parentObj.transform)
        {
            if(isSelect)
                selectedAnswer.Add(child.GetComponent<Text>());
            else
                selectAnswer.Add(child.GetComponent<Text>());
        }
    }
    
    
    /// <summary>
    /// 判断填空题答案是否正确
    /// </summary>
    /// <returns></returns>
    private bool IsCorrect_Short()
    {
        int sum = 0;
        for (int i = 0; i < correctAnswer.Count; i++)
        {
            if (correctAnswer[i] == selectedAnswer[i].text)
            {
                sum++;
            }
        }
        if (sum >= correctAnswer.Count)
            return true;
        
        
        return false;
    }
}