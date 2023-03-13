using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUIManager : MonoBehaviour
{
    public static StartUIManager Instance;

    public Transform m_LoginPanel, m_RegisterPanel, m_MainPanel;
    public GameObject m_HintMessage;

    private InputField username_Login, age_Login, username_Register, age_Register;

    private void Awake()
    {
        Instance = this;
        InitIF();
        InitBtn();
    }

    //初始化输入框
    private void InitIF()
    {
        username_Login = m_LoginPanel.Find("UserName/InputField").GetComponent<InputField>();
        age_Login = m_LoginPanel.Find("Age/InputField").GetComponent<InputField>();
        username_Register = m_RegisterPanel.Find("UserName/InputField").GetComponent<InputField>();
        age_Register = m_RegisterPanel.Find("Age/InputField").GetComponent<InputField>();
    }

    private void ShowHint(string str)
    {
        m_HintMessage.SetActive(true);
        m_HintMessage.GetComponent<Text>().text = str;
    }
    
    //初始化输入框的内容
    private void InitContent()
    {
        username_Login.text = "";
        age_Login.text = "";
        username_Register.text = "";
        age_Register.text = "";
    }
    
    //使用委托初始化按钮点击事件
    private void InitBtn()
    {
        //登录
        m_LoginPanel.Find("LoginBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            //登陆界面的登录按钮
            //发送数据之前，客户端自己先做一个校验，以便要提交的数据不为空
            if (username_Login.text.Equals("") || age_Login.text.Equals(""))
            {    
                InitContent();
                ShowHint("输入的内容不能为空，请重新输入");
                return;
            }
            //登录
            //TODO:登录检验
            m_LoginPanel.gameObject.SetActive(false);
            m_MainPanel.gameObject.SetActive(true);
        });
        m_LoginPanel.Find("RegisterBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            //登陆界面的注册按钮
            m_LoginPanel.gameObject.SetActive(false);
            m_RegisterPanel.gameObject.SetActive(true);
        });
        //注销
        m_MainPanel.Find("EnterGameBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            //进入游戏
            SceneManager.LoadScene("Game1");
        });
        m_MainPanel.Find("LogouBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            //TODO:注销登录
            m_MainPanel.gameObject.SetActive(false);
            m_LoginPanel.gameObject.SetActive(true);
        });
        m_MainPanel.Find("ExitBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            //退出登录
            m_MainPanel.gameObject.SetActive(false);
            m_LoginPanel.gameObject.SetActive(true);
        });
        
        //注册界面
        m_RegisterPanel.Find("RegisterBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            //注册界面的注册按钮
            //发送数据之前，客户端自己先做一个校验，以便要提交的数据不为空
            if (username_Register.text.Equals("") || age_Register.text.Equals(""))
            {
                InitContent();
                ShowHint("输入的内容不能为空，请重新输入");
                return;
            }
            //TODO: 注册
            m_RegisterPanel.gameObject.SetActive(false);
            m_LoginPanel.gameObject.SetActive(true);
        });
        m_RegisterPanel.Find("ReturnBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            //登陆界面的返回按钮
            m_RegisterPanel.gameObject.SetActive(false);
            m_LoginPanel.gameObject.SetActive(true);
        });


    }
}
