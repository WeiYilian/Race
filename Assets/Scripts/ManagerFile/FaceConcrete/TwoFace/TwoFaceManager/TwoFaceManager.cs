using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class TwoFaceManager
{
    public TwoFaceManager()
    {
        // 初始化
        VisceraList.Add(new Viscera(){Name = "心",Matter = "关于心的问题"});
        VisceraList.Add(new Viscera(){Name = "肝",Matter = "关于肝的问题"});
        VisceraList.Add(new Viscera(){Name = "脾",Matter = "关于脾的问题"});
        VisceraList.Add(new Viscera(){Name = "肺",Matter = "关于肺的问题"});
        VisceraList.Add(new Viscera(){Name = "肾",Matter = "关于肾的问题"});
    }

    public List<Viscera> VisceraList = new List<Viscera>();
}
