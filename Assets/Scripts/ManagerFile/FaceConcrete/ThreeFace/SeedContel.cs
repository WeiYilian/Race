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
    private GameObject SeedendPanel;//种子种植条件面板
   
    public GameObject seedmanger;
    private Image Seedimage;//种子图片
   
    private Image Tipeedimage;//提示板图片
    private Image Tipeedimage1;//提示板图片
    private Image Tipeedimage2;//提示板图片
    private GameObject bg3;
   
    //这里的video_img我是用来放RawImage的，挂载脚本后将RawImage拖入即可
    private GameObject video_img;

    
    private Text seedmangetext;//提示版信息
    private Text seedmangetext2;//提示版信息
    private Text nametext;//提示版名字
    private Text nametext1;//提示版名字
    private Text nametext2;//提示版名字
    
    private string Seedname;//种子名字
//确定，返回按钮
    private Button define;
    private Button back;
    //石否激活按钮
     static  bool isGame;
     static  bool isGame1;
     static  bool isGame2;

     private ThreeFaceMange ThreeFaceMange;
    void Start()
    {
        ThreeFaceMange = UIFaceManager.Instance.GetThreeFaceMange();
        
        video_img=GameObject.Find("Box/3/Panel").transform.Find("vidoiamge1").gameObject;
        bg3 = GameObject.Find("Box/3/Panel").transform.Find("bg").gameObject;
        
       TipPanel = GameObject.Find("Canvas").transform.Find("TIP3").gameObject;
       SeedPanel = TipPanel.transform.Find("种子信息面板").gameObject;
       SeedPlathPanel = TipPanel.transform.Find("种植条件选择面板").gameObject;
     //种植结果信息
       SeedendPanel = TipPanel.transform.Find("种子种植结果信息").gameObject;
       
       Seedname = gameObject.name;
       Seedimage = gameObject.transform.GetComponent<Image>();
       
       Tipeedimage = SeedPanel.transform.Find("seedimage").GetComponent<Image>();
       Tipeedimage1 = SeedPlathPanel.transform.Find("seedimage").GetComponent<Image>();
       Tipeedimage2 = SeedendPanel.transform.Find("seedimage").GetComponent<Image>();
       
       nametext=SeedPanel.transform.Find("nameText/Text").GetComponent<Text>();
       nametext1=SeedPlathPanel.transform.Find("nameText/Text").GetComponent<Text>();
       nametext2=SeedendPanel.transform.Find("nameText/Text").GetComponent<Text>();
       
       seedmangetext=SeedPanel.transform.Find("seedmanage/Text").GetComponent<Text>();
       seedmangetext2=SeedendPanel.transform.Find("seedmanage/Text").GetComponent<Text>();
       
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
            gameObject.GetComponent<Image>().sprite =Updateimage;
            if (Seedname=="甘菊")
            {UIFaceManager.Instance.MessageonCtrol("甘菊已激活");
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
        List<Button> SeedButtonList2 = new List<Button>();
       SeedButtonList2.AddRange(SeedendPanel.GetComponentsInChildren<Button>());//添加整个列表元素到列表里面
        foreach (Button button in SeedButtonList2)
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
                case "甘菊":
                    
                    seedmangetext.text = "【甘菊】\n \n<color=#FF0000>具体种植信息未激活，请找到对应种子拖入激活具体信息</color>\n  \n是否直接进入模拟种植点击确定";
                    UIFaceManager.Instance.MessageonCtrol("种子未激活");
                    break;
                case"凤仙花":
                    seedmangetext.text = "【凤仙花】\n \n<color=#FF0000>具体种植信息未激活，请找到对应种子拖入激活具体信息</color>\n \n是否直接进入模拟种植点击确定";
                    UIFaceManager.Instance.MessageonCtrol("种子未激活");
                    
                    break;
                case "车前子": 
                    seedmangetext.text = "【车前子】\n \n<color=#FF0000>具体种植信息未激活，请找到对应种子拖入激活具体信息</color>\n  \n是否直接进入模拟种植点击确定";
                    UIFaceManager.Instance.MessageonCtrol("种子未激活");
                    break;   
                    
            }
        }

        if (isGame == true)
        {
           
            TipPanel.SetActive(true);
            SeedPanel.SetActive(true);
            nametext.text = Seedname;
            Tipeedimage.sprite = Seedimage.sprite;
            switch (gameObject.name)
            {
                case "甘菊":
                    if (ThreeFaceMange.seedend1 == false)
                    {
                        seedmangetext.text = "【甘菊】\n \n喜欢寒冷的环境,更适合<color=#A52A2A>秋播</color>。甘菊为二年生草花,秋季播种,<color=#A52A2A>发芽温度15-18度,开花到结种时需要充足的阳光,</color>" +
                                             "良好的通风,排水良好的<color=#A52A2A>“沙壤土”</color>或土壤深厚的疏松壤土,抗寒性强\n是否进入模拟系统";
                    }

                    else
                    {
                        seedmangetext.text = "《食疗本草》中写道：其叶，正月采，可作羹；茎，五月五日采；花，九月九日采。〔证〕甘菊古代中国叫作春黄菊，稚子书传白菊开，西成相滞未容回。月明阶下窗纱薄，多少清香透入来。\n菊不仅仅能入药更多依托的人们不畏艰难，顽强斗争，风霜高洁的品质，\n 已种植完成继续模拟";
                    }
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
                    if (ThreeFaceMange.seedend3==false)
                    {
                        seedmangetext.text = "【凤仙花】\n \n种植的季节以<color=#A52A2A>4月</color>播种最为适宜，凤仙花<color=#A52A2A>各期性喜阳光</color>，最适合生长的温度为<color=#A52A2A>20-26℃</color>,凤仙花适应能力强壤，可选用疏松肥沃、<color=#A52A2A>“富含腐殖质的黑土”</color>，" +
                                             "不可长期的置于荫蔽处，以免枝叶徒长，花开稀少，花色暗淡\n是否进入模拟系统";
                    }
                    else
                    {
                        seedmangetext.text ="《本草纲目》草部第十七卷有“凤仙……茎有红白二色，其大如指，中空而脆“，翩翩然“欲羽化而登仙”得名。" +
                                            "“细看金凤小花丛，费尽司花染作工，雪色白边紫色袍，更饶深浅日般红。”说的就是凤仙花。\n 已种植完成继续模拟";
                    }
                   
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
                    if (ThreeFaceMange.seedend2 == false)
                    {
                        seedmangetext.text =
                            "【车前草】 \n  \n车前草原产于热带地区，<color=#A52A2A>20℃及以上的温度</color>更适合它的生长，喜欢生长在比较松软的<color=#A52A2A>“土壤”</color>中，它喜欢阳光充足的生长环境，除了<color=#A52A2A>夏季</color>都可以给它全天的日照。" +
                            "<color=#A52A2A>结子注意在周围喷水进行保湿遮阳</color>\n是否进入模拟系统。";
                    }
                    else
                    {
                        seedmangetext.text = " \n《本草纲目》：王旻《山居录》，有种车前剪苗食法,则昔人常以为蔬矣。今野人犹采食之。故又名当道、牛遗、地衣、过路、胜马、车前突。\n古诗中有写车前草的诗篇有写采采芣苢，薄言采之。采采芣苢薄言有之\n 已种植完成继续模拟";
                    }

                    break;
                    
            }
            
        }
    }
    //判断当前面是否全部完成全部种子种植
    public void isgameover()
    {
        if (ThreeFaceMange.seedend1&&ThreeFaceMange.seedend2&&ThreeFaceMange.seedend3)
        {
           
            ThreeFaceMange.Face3compement = true;
            GameObject.Find("3/Panel/bg").transform.Find("线索3").gameObject.SetActive(true);
            Debug.Log("第三面通关"+ThreeFaceMange.Face3compement);
            UIFaceManager.Instance.MessageonCtrol("第三面通关");
        }
        else
        {
            Debug.Log("一个成功");
        }
    }
//选择种植种子条件
    private void seedButtonclik(Button sender)
    {
        switch (sender.name)
        {
            case "确定":

            
            {   SeedPlathPanel.SetActive(true);
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
            case "确定返回":

            {
                //关闭信息面板
                SeedPanel.SetActive(true);
                SeedendPanel.SetActive(false);
            }
                break;
            case "开始":
            {//开始播放植物动画并检测最终结果

                Debug.Log("开始");
               
                  
                   
                    if (UIConter.Landidex == 0&& UIConter.Seasonidex==0&& UIConter.temidex==0&& UIConter.sunidex==0)
                    {
                        
                        UIFaceManager.Instance.MessageonCtrol("未设置模拟失败");
                        SeedPlathPanel.SetActive(false);
                        SeedendPanel.SetActive(true);
                        nametext2.text = Seedname;
                        Tipeedimage2.sprite = Seedimage.sprite;
                        seedmangetext2.text = "<color=#FF0000>模拟失败!条件未设置\n 是否继续模拟</color> \n 返回信息面板阅读信息继续模拟";
                        
                    }
                    else
                    {
                        if (UIConter.Landidex == 4&& UIConter.Seasonidex==4&& UIConter.temidex==4&& UIConter.sunidex==4)
                        {
                            Debug.Log("甘菊失败");
                            SeedPlathPanel.SetActive(false);
                            SeedendPanel.SetActive(true);
                            nametext2.text = Seedname;
                            Tipeedimage2.sprite = Seedimage.sprite;
                            seedmangetext2.text = "<color=#FF0000>模拟失败!条件选择不正确\n 是否继续模拟</color> \n 返回信息面板阅读信息继续模拟";
                        }
                        else
                        {
                            if (UIConter.Landidex == 5&& UIConter.Seasonidex==5&& UIConter.temidex==5&& UIConter.sunidex==5)
                            {
                                Debug.Log("凤仙花失败");
                                SeedPlathPanel.SetActive(false);
                                SeedendPanel.SetActive(true);
                                nametext2.text = Seedname;
                                Tipeedimage2.sprite = Seedimage.sprite;
                                seedmangetext2.text = "<color=#FF0000>模拟失败!条件选择不正确\n 是否继续模拟</color> \n 返回信息面板阅读信息继续模拟";
                            }
                            else
                            {
                                if (UIConter.Landidex == 6&& UIConter.Seasonidex==6&& UIConter.temidex==6&& UIConter.sunidex==6||UIConter.Landidex == 0&& UIConter.Seasonidex==6&& UIConter.temidex==6&& UIConter.sunidex==0)
                                {
                                    Debug.Log("车前子失败");
                                    SeedPlathPanel.SetActive(false);
                                    SeedendPanel.SetActive(true);
                                    nametext2.text = Seedname;
                                    Tipeedimage2.sprite = Seedimage.sprite;
                                    seedmangetext2.text = "<color=#FF0000>模拟失败!条件选择不正确\n 是否继续模拟</color> \n 返回信息面板阅读信息继续模拟";
                                }
                                else
                                {
                                    if (UIConter.Landidex == 1&& UIConter.Seasonidex==1&& UIConter.temidex==1&& UIConter.sunidex==1)
                                    {
                                        Debug.Log("甘菊成功");
                                        UIFaceManager.Instance.MessageonCtrol("甘菊模拟种植开始");
                                        SeedPlathPanel.SetActive(false);
                                        TipPanel.SetActive(false);
                                        seedmanger.gameObject.SetActive(false);
                                        SeedendPanel.gameObject.SetActive(false);
                                        video_img.SetActive(true);

                                       
                                        ThreeFaceMange.seedend1 = true;
                                        isgameover();
                                    }
                                    else
                                    {
                                        if (UIConter.Landidex == 2&& UIConter.Seasonidex==2&& UIConter.temidex==2&& UIConter.sunidex==2)
                                        {
                                            Debug.Log("凤仙花已成功");
                                            UIFaceManager.Instance.MessageonCtrol("凤仙花模拟种植开始");
                                            SeedPlathPanel.SetActive(false);
                                            TipPanel.SetActive(false);
                        
                                            video_img.SetActive(true);
                                            seedmanger.gameObject.SetActive(false);
                                            ThreeFaceMange.seedend3 = true;
                                            isgameover();
                                        }
                                        else
                                        {
                                            if (UIConter.Landidex == 3&& UIConter.Seasonidex==3&& UIConter.temidex==3&& UIConter.sunidex==3)
                                            {
                                                Debug.Log("车前草已成功");
                                                UIFaceManager.Instance.MessageonCtrol("车前子模拟种植开始");
                                                SeedPlathPanel.SetActive(false);
                                                TipPanel.SetActive(false);
                                                video_img.SetActive(true);
                                                seedmanger.gameObject.SetActive(false); 
                                                ThreeFaceMange.seedend2 = true;
                                                isgameover();
                                            }
                                            else
                                            {
                                                UIFaceManager.Instance.MessageonCtrol(""+gameObject.name+"模拟失败");
                        
                                                SeedendPanel.SetActive(true);
                                                nametext2.text = nametext1.text;
                                                Tipeedimage2.sprite = Tipeedimage1.sprite;
                                                SeedPlathPanel.SetActive(false);
                                                seedmangetext2.text = "<color=#FF0000>模拟失败!条件选择不正确\n 是否继续模拟</color> \n 返回信息面板阅读信息继续模拟";
                                            }
                                        }
                                    }
                                }
                            }
                        }
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
