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
    private string selectAnswer;

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
        });
        SelectAnswerDic.Add(new List<string>()
        {
            "《伤寒杂病论》的作者是（ ）","张仲景","张仲景","扁鹊","华佗","李时珍"
        });
        SelectAnswerDic.Add(new List<string>()
        {
            "五位是指药物的（ ）","最基本滋味","五种作用","部份味道","最基本滋味","全部味道"
        });
        SelectAnswerDic.Add(new List<string>()
        {
            "华佗、张仲景、（ ）被并称为“建安三神医”","董奉","李东垣","董奉","扁鹊","杨继洲"
        });
        SelectAnswerDic.Add(new List<string>()
        {
            "在五行生克关系中，下列错误的是（ ）","水克木","木克土","火生土","金生水","水克木"
        });
        SelectAnswerDic.Add(new List<string>()
        {
            "《铜人腧穴针灸图经》的作者是（ ）","王惟一","万密斋","皇甫谧","王惟一","李东垣"
        });
        SelectAnswerDic.Add(new List<string>()
        {
            "国内首先进行现代药理研究的中药是（ ）","麻黄","黄连","黄芩","麻黄","人参"
        });
        SelectAnswerDic.Add(new List<string>()
        {
            "以下中医著作为钱乙所著的是（ ）","《《小儿药证直诀》》","《温疫论》","《小儿药证直诀》","《温热论》","《脾胃论》"
        });
        SelectAnswerDic.Add(new List<string>()
        {
            "下列著作为孙思邈所作的是（ ）","《千金要方》","《脾胃论》","《本草纲目》","《食疗本草》","《千金要方》"
        });
        SelectAnswerDic.Add(new List<string>()
        {
            "下列被称为“医圣”的是（ ）","张仲景","扁鹊","李时珍","华佗","张仲景"
        });

        #endregion
        
        #region 填空题初始化

        FillVacancyAnswerDic.Add(new List<string>()
        {
            "无阴则阳无以生，无阳则阴无以化，体现了_____","阴阳互根","交","阴","长","互","阳","感","用","根","消","力"
        });
        FillVacancyAnswerDic.Add(new List<string>()
        {
            "肺主一身之气取决于肺的____","呼吸功能","吸","脉","气","宣","功","生","百","呼","能","朝"
        });
        FillVacancyAnswerDic.Add(new List<string>()
        {
            "根据五行学说推断，____可作为心病的诊断依据","面见赤色","耳","赤","急","甜","见","槁","面","易","燥","色"
        });
        FillVacancyAnswerDic.Add(new List<string>()
        {
            "中医学的基本特点是____和辨证论治","整体观念","念","谐","体","和","木","观","益","补","整","论"
        });
        FillVacancyAnswerDic.Add(new List<string>()
        {
            "五行中土的特性是","长养化育","育","净","炎","柔","化","发","清","养","杀","长"
        });
        FillVacancyAnswerDic.Add(new List<string>()
        {
            "五脏的共同生理特点是化生和____","贮藏精气","化","气","内","贮","和","温","藏","脏","热","精"
        });
        FillVacancyAnswerDic.Add(new List<string>()
        {
            "脉象有力是指____","柔和有力","力","沉","取","有","柔","从","不","容","和","缓"
        });
        FillVacancyAnswerDic.Add(new List<string>()
        {
            "“春养脾气；夏养肺气；秋养肝气；冬养心气”出自：《____》","食疗本草","经","草","乙","灸","本","腧","甲","疗","针","食"
        });
        FillVacancyAnswerDic.Add(new List<string>()
        {
            "“安谷则昌，绝谷则亡。”出自：《____》","本草纲目","伤","纲","草","病","寒","目","杂","论","疗","本"
        });
        FillVacancyAnswerDic.Add(new List<string>()
        {
            "“上医医国，中医医人，下医医病。”出自：《____》","千金要方","草","要","本","目","金","千","难","纲","经","方"
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
                transform.Find("ConfirmBtn").GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (SelectAnswerDic[rod][1] == selectAnswer)
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
                transform.Find("ClearBtn").GetComponent<Button>().onClick.AddListener(() =>
                {
                    selectSum = 0;
                    foreach (Text text in AnswerArea_Text)
                    {
                        text.text = null;
                    }
                });
                
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
                selectAnswer = button.transform.GetChild(0).GetComponent<Text>().text;
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
