using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class TwoFaceManager
{
    public List<Viscera> VisceraList = new List<Viscera>();

    public bool AnsWer;
    
    public TwoFaceManager()
    {
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
    }

    public Viscera GetViscera(string name)
    {
        foreach (Viscera viscera in VisceraList)
        {
            if (name == viscera.Name)
            {
                return viscera;
            }
                
        }

        return null;
    }

    private void GetAnswer()
    {
        int i = 0;
        foreach (Viscera viscera in VisceraList)
        {
            if (viscera.IsGivenPos)
                i++;
        }

        if (i >= 5)
            AnsWer = true;
    }
}