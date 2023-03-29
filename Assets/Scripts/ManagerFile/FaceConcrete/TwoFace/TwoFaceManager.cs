using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class TwoFaceManager : MonoBehaviour
{
    public List<GameObject> VisceraList;

    private Dictionary<string, Viscera> VisceraDic;
    
    void Start()
    {
        VisceraDic.Add("heart",new Viscera(){Index = 1,VisceraObj = VisceraList[1],Matter = "关于心的问题"});
        VisceraDic.Add("liver",new Viscera(){Index = 2,VisceraObj = VisceraList[2],Matter = "关于肝的问题"});
        VisceraDic.Add("spleen",new Viscera(){Index = 3,VisceraObj = VisceraList[3],Matter = "关于脾的问题"});
        VisceraDic.Add("lungs",new Viscera(){Index = 4,VisceraObj = VisceraList[4],Matter = "关于肺的问题"});
        VisceraDic.Add("kidney",new Viscera(){Index = 5,VisceraObj = VisceraList[5],Matter = "关于肾的问题"});
    }
}
