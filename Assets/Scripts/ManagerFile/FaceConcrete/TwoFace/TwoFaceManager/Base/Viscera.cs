using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProblemType
{
    ShortAnswer,
    FillVacancy
}

public class Viscera
{
    // 脏腑名字
    public string Name;
    //是否答完题目
    public bool AnswerOver = false;
    // 判断点击按钮之后的问题类型
    public ProblemType ProblemType;
}
