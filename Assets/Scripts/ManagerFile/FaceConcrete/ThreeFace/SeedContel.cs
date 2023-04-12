using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
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
    private GameObject bg3;
   
    //这里的video_img我是用来放RawImage的，挂载脚本后将RawImage拖入即可
    private GameObject video_img;

    
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
        video_img=GameObject.Find("Box/3/Panel").transform.Find("vidoiamge1").gameObject;
        bg3 = GameObject.Find("Box/3/Panel").transform.Find("bg").gameObject;
        
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
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //检测是否将各个种子找到
        if (collision.name == Seedname)
        {
            Debug.Log("ss");
            gameObject.GetComponent<Image>().sprite =Updateimage;
            if (Seedname=="洋甘菊")
            {UIFaceManager.Instance.MessageonCtrol("洋甘菊已激活");
                SeedContel.isGame = true;
                ThreeFaceMange.Seed1 = true;
            }

            if (Seedname == "车前子")
            {  UIFaceManager.Instance.MessageonCtrol("车前子已激活");
                SeedContel.isGame2 = true;
                ThreeFaceMange.Seed2 = true;
            }
            if (Seedname == "凤仙花")
            { 
                UIFaceManager.Instance.MessageonCtrol("凤仙花已激活");
                SeedContel.isGame1 = true;
                ThreeFaceMange.Seed3 = true;
            }

            Destroy(collision.gameObject);//找到后销毁
        }
       
    }
//管理各个面板按钮
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
                    
                    seedmangetext.text = "【洋甘菊】 \n<color=#FF0000>具体种植信息未激活，请找到对应种子拖入激活具体信息</color>\n是否继续种植请点击确定";
                    UIFaceManager.Instance.MessageonCtrol("种子未激活");
                    break;
                case"凤仙花":
                    seedmangetext.text = "【凤仙花】 \n<color=#FF0000>具体种植信息未激活，请找到对应种子拖入激活具体信息</color>\n是否继续种植请点击确定";
                    UIFaceManager.Instance.MessageonCtrol("种子未激活");
                    
                    break;
                case "车前子": 
                    seedmangetext.text = "【车前子】 \n<color=#FF0000>具体种植信息未激活，请找到对应种子拖入激活具体信息</color>\n是否继续种植请点击确定";
                    UIFaceManager.Instance.MessageonCtrol("种子未激活");
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
                    seedmangetext.text = "【洋甘菊】 \n喜欢寒冷的环境,更适合秋播。洋甘菊为二年生草花,秋季播种,发芽温度15-18度,开花到结种时需要充足的阳光," +
                                         "良好的通风,排水良好的“沙壤土”或土壤深厚的疏松壤土,抗寒性强\n是否进入模拟系统";
                    
                    break;
                
                    
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
                    seedmangetext.text = "【凤仙花】 \n种植的季节以4月播种最为适宜，凤仙花各期性喜阳光，最适合生长的温度为20-26℃,凤仙花适应能力强壤，可选用疏松肥沃、“富含腐殖质的黑土”，" +
                                         "不可长期的置于荫蔽处，以免枝叶徒长，花开稀少，花色暗淡\n是否进入模拟系统";
                   
                    break;
                
                    
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
                    seedmangetext.text = "【车前草】 \n车前草原产于热带地区，20℃及以上的温度更适合它的生长，喜欢生长在比较松软的“土壤”中，它喜欢阳光充足的生长环境，除了夏季都可以给它全天的日照。" +
                                         "结子注意在周围喷水进行保湿遮阳\n是否进入模拟系统。";
                  
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
               
                    //判断是否都达到了
                    if (UIConter.Landidex == 1&& UIConter.Seasonidex==1&& UIConter.temidex==1&& UIConter.sunidex==1)
                    {
                        Debug.Log("洋甘菊成功");
                        UIFaceManager.Instance.MessageonCtrol("洋甘菊模拟种植开始");
                        SeedPlathPanel.SetActive(false);
                        TipPanel.SetActive(false);
                        
                    }
                    
                    if (UIConter.Landidex == 2&& UIConter.Seasonidex==2&& UIConter.temidex==2&& UIConter.sunidex==2)
                    {
                        Debug.Log("凤仙花已成功");
                        UIFaceManager.Instance.MessageonCtrol("凤仙花模拟种植开始");
                        SeedPlathPanel.SetActive(false);
                        TipPanel.SetActive(false);
                        
                        video_img.SetActive(true);
                      
                    }
                    if (UIConter.Landidex == 3&& UIConter.Seasonidex==3&& UIConter.temidex==3&& UIConter.sunidex==3)
                    {
                        Debug.Log("车前草已成功");
                        UIFaceManager.Instance.MessageonCtrol("车前子模拟种植开始");
                        SeedPlathPanel.SetActive(false);
                        TipPanel.SetActive(false);
                    }

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
