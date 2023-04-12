using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AnswerManager : MonoBehaviour
{
    //问题类型
    public ProblemType ProblemType;
    //选择问题字典  问题答案以及干扰选项列表,首位为问题,第二位为正确答案
    private List<List<string>> SelectAnswerDic = new List<List<string>>();
    //填空问题字典  问题答案以及干扰选项列表,首位为问题,第二位为正确答案
    private List<List<string>> FillVacancyAnswerDic = new List<List<string>>();
    //选择的问题序号
    private int rod;

    // 问题
    private Text Question;
    // 填空题
    private List<Text> AnswerArea_Text;
    private List<Button> SelAnswerArea_Btn;
    private List<Text> SelAnswerArea_Text;
    private int selectSum = 0;//已选择字的数量
    // 选择题
    private List<Button> Options_Btn;
    private List<Text> Options_Text;

    private void Awake()
    {
        Init();
    }

    //初始化
    private void Init()
    {
        #region 组件初始化
                
        //两种问题通用的查找
        Question = transform.Find("Question").GetComponent<Text>();
        switch (ProblemType)
        {
            case ProblemType.ShortAnswer:
                //选择题
                GetAnswer(transform.Find("Options"),ref Options_Text,true);
                GetAnswer(transform.Find("Options"),ref Options_Btn);
                break;
            case ProblemType.FillVacancy:
                //填空题
                GetAnswer(transform.Find("Answers"),ref AnswerArea_Text,true);
                GetAnswer(transform.Find("AnswerSelect"),ref SelAnswerArea_Btn);
                GetAnswer(transform.Find("AnswerSelect"),ref SelAnswerArea_Text,true);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        #endregion

        #region 问题初始化

        #region 选择题初始化

        SelectAnswerDic.Add(new List<string>()
        {
            "我国第一部药典是（ ）","《神农本草经》","《新修本草》","《神农本草经》","《证类本草》","《中药大辞典》"
        });SelectAnswerDic.Add(new List<string>()
        {
            "我国第一部药典是（ ）","《神农本草经》","《新修本草》","《神农本草经》","《证类本草》","《中药大辞典》"
        });SelectAnswerDic.Add(new List<string>()
        {
            "我国第一部药典是（ ）","《神农本草经》","《新修本草》","《神农本草经》","《证类本草》","《中药大辞典》"
        });SelectAnswerDic.Add(new List<string>()
        {
            "我国第一部药典是（ ）","《神农本草经》","《新修本草》","《神农本草经》","《证类本草》","《中药大辞典》"
        });SelectAnswerDic.Add(new List<string>()
        {
            "我国第一部药典是（ ）","《神农本草经》","《新修本草》","《神农本草经》","《证类本草》","《中药大辞典》"
        });SelectAnswerDic.Add(new List<string>()
        {
            "我国第一部药典是（ ）","《神农本草经》","《新修本草》","《神农本草经》","《证类本草》","《中药大辞典》"
        });SelectAnswerDic.Add(new List<string>()
        {
            "我国第一部药典是（ ）","《神农本草经》","《新修本草》","《神农本草经》","《证类本草》","《中药大辞典》"
        });SelectAnswerDic.Add(new List<string>()
        {
            "我国第一部药典是（ ）","《神农本草经》","《新修本草》","《神农本草经》","《证类本草》","《中药大辞典》"
        });SelectAnswerDic.Add(new List<string>()
        {
            "我国第一部药典是（ ）","《神农本草经》","《新修本草》","《神农本草经》","《证类本草》","《中药大辞典》"
        });SelectAnswerDic.Add(new List<string>()
        {
            "我国第一部药典是（ ）","《神农本草经》","《新修本草》","《神农本草经》","《证类本草》","《中药大辞典》"
        });SelectAnswerDic.Add(new List<string>()
        {
            "我国第一部药典是（ ）","《神农本草经》","《新修本草》","《神农本草经》","《证类本草》","《中药大辞典》"
        });

        #endregion
        
        #region 填空题初始化

        FillVacancyAnswerDic.Add(new List<string>()
        {
            "无阴则阳无以生，无阳则阴无以化，体现了_____","阴阳互根","交","阴","长","互","阳","感","用","根","消","力"
        });FillVacancyAnswerDic.Add(new List<string>()
        {
            "无阴则阳无以生，无阳则阴无以化，体现了_____","阴阳互根","交","阴","长","互","阳","感","用","根","消","力"
        });FillVacancyAnswerDic.Add(new List<string>()
        {
            "无阴则阳无以生，无阳则阴无以化，体现了_____","阴阳互根","交","阴","长","互","阳","感","用","根","消","力"
        });FillVacancyAnswerDic.Add(new List<string>()
        {
            "无阴则阳无以生，无阳则阴无以化，体现了_____","阴阳互根","交","阴","长","互","阳","感","用","根","消","力"
        });FillVacancyAnswerDic.Add(new List<string>()
        {
            "无阴则阳无以生，无阳则阴无以化，体现了_____","阴阳互根","交","阴","长","互","阳","感","用","根","消","力"
        });FillVacancyAnswerDic.Add(new List<string>()
        {
            "无阴则阳无以生，无阳则阴无以化，体现了_____","阴阳互根","交","阴","长","互","阳","感","用","根","消","力"
        });FillVacancyAnswerDic.Add(new List<string>()
        {
            "无阴则阳无以生，无阳则阴无以化，体现了_____","阴阳互根","交","阴","长","互","阳","感","用","根","消","力"
        });FillVacancyAnswerDic.Add(new List<string>()
        {
            "无阴则阳无以生，无阳则阴无以化，体现了_____","阴阳互根","交","阴","长","互","阳","感","用","根","消","力"
        });FillVacancyAnswerDic.Add(new List<string>()
        {
            "无阴则阳无以生，无阳则阴无以化，体现了_____","阴阳互根","交","阴","长","互","阳","感","用","根","消","力"
        });FillVacancyAnswerDic.Add(new List<string>()
        {
            "无阴则阳无以生，无阳则阴无以化，体现了_____","阴阳互根","交","阴","长","互","阳","感","用","根","消","力"
        });FillVacancyAnswerDic.Add(new List<string>()
        {
            "无阴则阳无以生，无阳则阴无以化，体现了_____","阴阳互根","交","阴","长","互","阳","感","用","根","消","力"
        });
        
        #endregion

        #endregion
    }
    

    private void Start()
    {
        InitBtn();
    }

    private void InitBtn()
    {
        switch (ProblemType)
        {
            case ProblemType.ShortAnswer:
                BtnDelegate(Options_Btn[0]);
                BtnDelegate(Options_Btn[1]);
                BtnDelegate(Options_Btn[2]);
                BtnDelegate(Options_Btn[3]);
                break;
            case ProblemType.FillVacancy:
                BtnDelegate(SelAnswerArea_Btn[0],false);
                BtnDelegate(SelAnswerArea_Btn[1],false);
                BtnDelegate(SelAnswerArea_Btn[2],false);
                BtnDelegate(SelAnswerArea_Btn[3],false);
                BtnDelegate(SelAnswerArea_Btn[4],false);
                BtnDelegate(SelAnswerArea_Btn[5],false);
                BtnDelegate(SelAnswerArea_Btn[6],false);
                BtnDelegate(SelAnswerArea_Btn[7],false);
                BtnDelegate(SelAnswerArea_Btn[8],false);
                BtnDelegate(SelAnswerArea_Btn[9],false);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void BtnDelegate(Button button,bool isShortAnswer = true)
    {
        if (isShortAnswer)
        {
            button.onClick.AddListener(() =>
            {
                if (SelectAnswerDic[rod][1] == button.transform.GetChild(0).GetComponent<Text>().text)
                {
                    //回答正确
                    UIFaceManager.Instance.MessageonCtrol("回答正确");
                    SelectAnswerDic.RemoveAt(rod);
                }
                else
                {
                    //回答错误
                    UIFaceManager.Instance.MessageonCtrol("回答错误");
                }
                transform.parent.gameObject.SetActive(false);
            });
        }
        else
        {
            button.onClick.AddListener(() =>
            {
                if (selectSum < AnswerArea_Text.Count - 1)
                {
                    AnswerArea_Text[selectSum].text = button.transform.GetChild(0).GetComponent<Text>().text;
                    selectSum++;
                }
                else
                {
                    AnswerArea_Text[selectSum].text = button.transform.GetChild(0).GetComponent<Text>().text;//最后一次点击也要存储
                    StringBuilder selectText = new StringBuilder();
                    foreach (Text text in AnswerArea_Text)
                    {
                        selectText.Append(text.text);
                        text.text = null;
                    }
                    Debug.Log(selectText);
                    if (FillVacancyAnswerDic[rod][1] == selectText.ToString())
                    {
                        UIFaceManager.Instance.MessageonCtrol("回答正确");
                        FillVacancyAnswerDic.RemoveAt(rod);
                        transform.parent.gameObject.SetActive(false);
                    }
                    else
                    {
                        Debug.Log(AnswerArea_Text.Count);
                        UIFaceManager.Instance.MessageonCtrol("回答错误");
                    }
                    selectSum = 0;
                }
            });
        }
        
    }

    /// <summary>
    /// 遍历子物体并获取Text组件
    /// </summary>
    private void GetAnswer<T>(Transform parentObj,ref List<T> list,bool isChildComponent = false)
    {
        list = new List<T>();
        if (isChildComponent)
        {
            foreach (Transform child in parentObj)
            {
                list.Add(child.GetChild(0).GetComponent<T>());
            }
        }
        else
        {
            foreach (Transform child in parentObj)
            {
                list.Add(child.GetComponent<T>());
            }
        }
    }
    
    private void OnEnable()
    {
        SpawnAnswer();
    }


    /// <summary>
    /// 初始化问题
    /// 问题答案以及干扰选项列表,首位为问题，第二位为正确答案，其余的均为干扰选项
    /// </summary>
    private void SpawnAnswer()
    {
        switch (ProblemType)
        {
            case ProblemType.ShortAnswer:
                //选择题
                rod = Random.Range(0, SelectAnswerDic.Count);
                if (SelectAnswerDic[rod] != null)
                {
                    List<string> questions = SelectAnswerDic[rod];
                    Question.text = questions[0];//问题初始化
                    for (int i = 0; i < Options_Text.Count; i++)
                    {
                        Options_Text[i].text = questions[i + 2];
                    }
                }
                break;
            case ProblemType.FillVacancy:
                rod = Random.Range(0, FillVacancyAnswerDic.Count);
                if (FillVacancyAnswerDic[rod] != null)
                {
                    List<string> questions = FillVacancyAnswerDic[rod];
                    Question.text = questions[0];//问题初始化
                    for (int i = 0; i < SelAnswerArea_Text.Count; i++)
                    {
                        SelAnswerArea_Text[i].text = questions[i + 2];
                    }
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }
}
