using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using System.Linq;

public class TwoFaceManager
{
    // viscera脏腑列表
    public List<Viscera> VisceraList = new List<Viscera>();
    // 存储innerDiscIFR
    private InFaceRotation innerDiscIFR;
    // 存储外圆盘
    private GameObject outerDisc;
    // 存储viscera目标位置
    private Dictionary<string,string> visceraDesDic = new Dictionary<string,string>();
    // 存储Viscera对应的密码表
    private Dictionary<string, string> visceraCode = new Dictionary<string, string>();
    // 判断是否全部匹配的bool值
    public bool AnsWer;

    public TwoFaceManager()
    {
        #region VisceraList初始化

        // 初始化
        VisceraList.Add(new Viscera(){Name = "心",ProblemType = ProblemType.ShortAnswer,Matter = "怎样理解心主血脉，其华在面和开窍于舌？"});
        VisceraList.Add(new Viscera()
        {
            Name = "肝",ProblemType = ProblemType.FillVacancy,Matter = "无阴则阳无以生，无阳则阴无以化，体现了____",AnswerList = 
                {"交","阴","长","互","阳","感","用","根","消","力"}
        });
        VisceraList.Add(new Viscera()
        {
            Name = "脾",ProblemType = ProblemType.FillVacancy,Matter = "五行中土的特性是____",AnswerList =
                {"净","育","炎","柔","长","发","化","清","杀","养"}
        });
        VisceraList.Add(new Viscera()
        {
            Name = "肺",ProblemType = ProblemType.FillVacancy,Matter = "肺主一身之气取决于肺的____",AnswerList = 
                {"吸","脉","气","宣","功","生","百","呼","能","朝"}
            
        });
        VisceraList.Add(new Viscera(){Name = "肾",ProblemType = ProblemType.ShortAnswer,Matter = "简述心肾相交的含义"});

        #endregion

        #region Viscera目的地列表初始化
        // 获得innerDiscIFR
        innerDiscIFR = GameObject.Find("Box").transform.Find("2/Panel/bg/内圆盘").GetComponent<InFaceRotation>();
        // 获取外圆盘
        outerDisc = GameObject.Find("Box").transform.Find("2/Panel/bg/外圆盘").gameObject;
        // 初始化visceraDesList
        foreach (Transform child in outerDisc.transform)
        {
            visceraDesDic.Add(child.name,null);
        }

        #endregion

        #region Viscera对应的密码表初始化
        
        // 金对应肺，木对应肝，水对应肾，火对应心，土对应脾
        visceraCode.Add("金","肺");
        visceraCode.Add("木","肝");
        visceraCode.Add("水","肾");
        visceraCode.Add("火","心");
        visceraCode.Add("土","脾");

        #endregion

    }

    /// <summary>
    /// 通过名字获得Viscera类
    /// </summary>
    /// <param name="name">脏腑名字</param>
    /// <returns></returns>
    public Viscera GetViscera(string name)
    {
        //遍历viscera脏腑列表
        foreach (Viscera viscera in VisceraList)
        {
            // 找到了则返回Viscera
            if (name == viscera.Name)
            {
                return viscera;
            }
                
        }
        // 否则一律返回null
        return null;
    }

    
    /// <summary>
    /// 判断是否可以排入VisceraDesDic字典中
    /// </summary>
    /// <param name="obj">Viscera最后停留的物体</param>
    /// <param name="olderperant">Viscera的父物体</param>
    /// <param name="curViscera">当前的Viscera</param>
    public void JoinVisDesDic(GameObject obj,GameObject olderperant,Viscera curViscera)
    {
        // 当Viscera离开Viscera目标位置的时候，字典清除Viscera的名字
        if (visceraDesDic.ContainsKey(olderperant.name))
            visceraDesDic[olderperant.name] = null;
        // 当Viscera落在Viscera目标位置的时候，字典加入Viscera的名字
        if(visceraDesDic.ContainsKey(obj.name))
            visceraDesDic[obj.name] = curViscera.Name;
    }
    
    
    /// <summary>
    /// 判断全部匹配
    /// </summary>
    public bool MatchJudgment()
    {
        // 记录匹配了多少个脏腑与五行
        int matchesNumber = 0;
        int i = 0;
        // 遍历五行队列
        foreach (var wuXingName in innerDiscIFR.wuxingQueue)
        {
            //根据五行名获取密码表对应的Viscera名字
            if (visceraCode.TryGetValue(wuXingName, out string visceraName))
            {
                if (visceraDesDic[i.ToString()] == visceraName)
                    matchesNumber++;
            }

            i++;
        }

        if (matchesNumber >= visceraDesDic.Count)
            return true;

        return false;
    }
}