using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUIManager : MonoBehaviour
{
    
    private SceneStateController contorl = null;
    
    public static StartUIManager Instance;

    public Transform m_FrontPanel, m_LoginPanel, m_RegisterPanel, m_MainPanel;
    public Transform m_HintMessage;

    private InputField username_Login, password_Login, username_Register, password_Register;

    private void Start()
    {
        contorl = new SceneStateController();
    }
    private void Awake()
    {
        Instance = this;
        InitIF();
        InitBtn();
    }

    //初始化输入框
    private void InitIF()
    {
        username_Login = m_LoginPanel.Find("Username/InputField").GetComponent<InputField>();
        password_Login = m_LoginPanel.Find("Password/InputField").GetComponent<InputField>();
        username_Register = m_RegisterPanel.Find("Username/InputField").GetComponent<InputField>();
        password_Register = m_RegisterPanel.Find("Password/InputField").GetComponent<InputField>();
    }

    private void ShowHint(string str)
    {
        m_HintMessage.gameObject.SetActive(true);
        m_HintMessage.GetChild(0).GetComponent<Text>().text = str;
    }
    
    //初始化输入框的内容
    private void InitContent()
    {
        username_Login.text = "";
        password_Login.text = "";
        username_Register.text = "";
        password_Register.text = "";
    }
    
    //使用委托初始化按钮点击事件
    private void InitBtn()
    {
        //首页面进入游戏按钮
        m_FrontPanel.Find("EnterBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonAudio();
            SceneStateController.Instance.SetState(new MainScene());
        });
    }
}
