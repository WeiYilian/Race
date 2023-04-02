using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class TwoFaceManager
{
    public List<Viscera> VisceraList = new List<Viscera>();
    public TwoFaceManager()
    {
        // 初始化
        VisceraList.Add(new Viscera(){Name = "心",ProblemType = ProblemType.ShortAnswer,Matter = "关于心的问题"});
        VisceraList.Add(new Viscera(){Name = "肝",ProblemType = ProblemType.FillVacancy,Matter = "关于肝的问题"});
        VisceraList.Add(new Viscera(){Name = "脾",ProblemType = ProblemType.FillVacancy,Matter = "关于脾的问题"});
        VisceraList.Add(new Viscera(){Name = "肺",ProblemType = ProblemType.FillVacancy,Matter = "关于肺的问题"});
        VisceraList.Add(new Viscera(){Name = "肾",ProblemType = ProblemType.ShortAnswer,Matter = "关于肾的问题"});
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
}
