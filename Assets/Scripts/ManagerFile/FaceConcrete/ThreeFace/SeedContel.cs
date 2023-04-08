using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedContel : MonoBehaviour
{ public Sprite Updateimage;//种子激活图片
    // Start is called before the first frame update
    private GameObject TipPanel;//第三面提示面板
    private GameObject SeedPanel;//种子提示面板
    private GameObject SeedPlathPanel;//种子种植条件面板
    private Image Seedimage;//种子图片
   
    private Image Tipeedimage;//提示板图片
    private Image Tipeedimage1;//提示板图片
    
   
    
    private Text seedmangetext;//提示版信息
    private Text nametext;//提示版名字
    private Text nametext1;//提示版名字
    private string Seedname;//种子名字
//确定，返回按钮
    private Button define;
    private Button back;
    //石否激活按钮
     static  bool isGame;
     static  bool isGame1;
     static  bool isGame2;
    void Start()
    {
       TipPanel = GameObject.Find("Canvas").transform.Find("TIP3").gameObject;
       SeedPanel = TipPanel.transform.Find("种子信息面板").gameObject;
       SeedPlathPanel = TipPanel.transform.Find("种植条件选择面板").gameObject;
       Seedname = gameObject.name;
       Seedimage = gameObject.transform.GetComponent<Image>();
       Tipeedimage = SeedPanel.transform.Find("seedimage").GetComponent<Image>();
       Tipeedimage1 = SeedPlathPanel.transform.Find("seedimage").GetComponent<Image>();
       nametext=SeedPanel.transform.Find("nameText/Text").GetComponent<Text>();
       nametext1=SeedPlathPanel.transform.Find("nameText/Text").GetComponent<Text>();
       seedmangetext=SeedPanel.transform.Find("seedmanage/Text").GetComponent<Text>();
       define = SeedPanel.transform.Find("确定").GetComponent<Button>();
       back = SeedPanel.transform.Find("返回").GetComponent<Button>();
       MyOnEnable();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == Seedname)
        {
            Debug.Log("ss");
            gameObject.GetComponent<Image>().sprite =Updateimage;
            if (Seedname=="洋甘菊")
            {
                SeedContel.isGame = true;
            }

            if (Seedname == "车前子")
            {
                SeedContel.isGame2 = true;
            }
            if (Seedname == "凤仙花")
            {
                SeedContel.isGame1 = true;
            }

            Destroy(collision.gameObject);
        }
       
    }

    private void MyOnEnable()
    {
        //种子信息面板按钮管理
        List<Button> SeedButtonList = new List<Button>();
       SeedButtonList.AddRange(SeedPanel.GetComponentsInChildren<Button>());//添加整个列表元素到列表里面
        foreach (Button button in SeedButtonList)
        {
            button.onClick.AddListener(() =>
            {
                seedButtonclik(button);

            });
        }
        List<Button> SeedButtonpalthList = new List<Button>();
        SeedButtonpalthList.AddRange(SeedPlathPanel.GetComponentsInChildren<Button>());//添加整个列表元素到列表里面
        foreach (Button button in SeedButtonpalthList)
        {
            button.onClick.AddListener(() =>
            {
                seedButtonclik(button);

            });
        }
    }
//点击种子弹出消息面板，是否激活和详细信息

    #region 种子面板点击Button

    public void OnCilk()
    {
       
       
        
        if (isGame==false||isGame1==false||isGame2==false)
        {
            Debug.Log("2");
            TipPanel.SetActive(true);
            SeedPanel.SetActive(true);
            nametext.text = Seedname;
            Tipeedimage.sprite = Seedimage.sprite;
            switch (gameObject.name)
            {
                case "洋甘菊":
                    seedmangetext.text = "【洋甘菊】 \n具体种植信息未激活，请找到对应种子拖入激活具体信息\n是否继续种植请点击确定";
                    break;
                case"凤仙花":
                    seedmangetext.text = "【凤仙花】 \n具体种植信息未激活，请找到对应种子拖入激活具体信息\n是否继续种植请点击确定";
                    break;
                case "车前子": 
                    seedmangetext.text = "【车前子】 \n具体种植信息未激活，请找到对应种子拖入激活具体信息\n是否继续种植请点击确定";
                    break;   
                    
            }
        }

        if (isGame == true)
        {
            Debug.Log("1");
            TipPanel.SetActive(true);
            SeedPanel.SetActive(true);
            nametext.text = Seedname;
            Tipeedimage.sprite = Seedimage.sprite;
            switch (gameObject.name)
            {
                case "洋甘菊":
                    seedmangetext.text = "【洋甘菊】 \n喜欢寒冷的环境,更适合秋播。洋甘菊为二年生草花,秋季播种,发芽温度15-18,花期为次年3月至5月洋甘菊需要充足的阳光," +
                                         "良好的通风,排水良好的沙壤土或土壤深厚的疏松壤土,抗寒性强\n\n\n如若继续请点击确定";
                    break;
                // case"凤仙花":
                //     seedmangetext.text = "";
                //     break;
                // case "车前子": 
                //     seedmangetext.text = "";
                //     break;   
                    
            }
        }
        if (isGame1 == true)
        {
            Debug.Log("1");
            TipPanel.SetActive(true);
            SeedPanel.SetActive(true);
            nametext.text = Seedname;
            Tipeedimage.sprite = Seedimage.sprite;
            switch (gameObject.name)
            {
                
                case"凤仙花":
                    seedmangetext.text = "222";
                    break;
                // case "车前子": 
                //     seedmangetext.text = "";
                //     break;   
                    
            }
        }
        if (isGame2 == true)
        {
            Debug.Log("1");
            TipPanel.SetActive(true);
            SeedPanel.SetActive(true);
            nametext.text = Seedname;
            Tipeedimage.sprite = Seedimage.sprite;
            switch (gameObject.name)
            {
                
               
                case "车前子": 
                    seedmangetext.text = "12";
                    break;   
                    
            }
        }
    }
//选择种植种子条件
    private void seedButtonclik(Button sender)
    {
        switch (sender.name)
        {
            case "确定":

            {//开启植物种植面板
               
                SeedPlathPanel.SetActive(true);
                nametext1.text = nametext.text;
                Tipeedimage1.sprite = Tipeedimage.sprite;
                SeedPanel.SetActive(false);
               
            }
                break;
            case "返回":

            {//关闭信息面板
                SeedPanel.SetActive(false);
                TipPanel.SetActive(false);
            }
                break;
            case "开始":

            {//开始播放植物动画并检测最终结果
               
               Debug.Log("开始");
               
            }
                break;
            case "取消":

            {//关闭信息面板
                SeedPlathPanel.SetActive(false);
                TipPanel.SetActive(false);
            }
                break;
            
        }
    }

    #endregion
   
}
