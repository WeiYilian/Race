using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    private Text m_Text;
    
    //打字时间间隔
    public float charsPerSecond = 0.2f;
    //保存需要显示的文字
    private string words;
    //判断是否需要显示字
    private bool isActive = true;
    //计时器
    private float timer;
    //当前打字位置
    private int currentPos;
    //存放需要打印的文字列表
    private List<string> TextList = new List<string>();
    //列表索引
    private int index;

    void Start ()
    {
        //初始化
        m_Text = transform.GetChild(0).GetComponent<Text>();
        TextList.Add("中国中医学博大精深，其中诞生了许多伟人与文化瑰宝");
        TextList.Add("了解他们，传承它们是我们应尽的职责");
        TextList.Add("那么，运用你的智慧去破解那些谜题吧");

        //要打印的第一句话为列表的首位
        words = TextList[index];
    }

    void Update () 
    {
        OnStartWriter ();
    }
    
    /// <summary>
    /// 执行打字任务
    /// </summary>
    private void OnStartWriter()
    {
        if(isActive)
        {
            timer += Time.deltaTime;
            if(timer >= charsPerSecond) //判断计时器时间是否到达
            {
                timer = 0;
                currentPos++;
                m_Text.text = words.Substring (0,currentPos);//刷新文本显示内容
 
                if(currentPos >= words.Length)
                {
                    index++;
                    OnFinish();
                    Invoke(nameof(OnRefresh),2f);
                }
            }
        }
    }
    
    /// <summary>
    /// 结束打字，初始化数据
    /// </summary>
    private void OnFinish()
    {
        isActive = false;
        timer = 0;
        currentPos = 0;
        m_Text.text = words;
    }

    /// <summary>
    /// 刷新下一句话
    /// </summary>
    private void OnRefresh()
    {
        //若索引超出范围就返回
        if (index >= TextList.Count)
        {
            GetComponent<Image>().DOFade(0, 1);
            Camera.main.GetComponent<ViewController>().autoRotSpeed = 0;
            Invoke(nameof(Disappear),1);
            return;
        }
        
        //初始化
        words = TextList[index];
        m_Text.text = "";
        isActive = true;
    }

    private void Disappear()
    {
        UIFaceManager.Instance.isGameStart = true;
        gameObject.SetActive(false);
    }
}
