using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedContel : MonoBehaviour
{ public Sprite Updateimage;//种子激活图片
    // Start is called before the first frame update
    private GameObject TipPanel;//第三面提示面板
    private GameObject SeedPanel;//种子提示面板
    private Image Seedimage;//种子图片
    private Image Tipeedimage;//提示板图片
   
    
    private Text seedmangetext;//提示版信息
    private Text nametext;//提示版名字
    private string Seedname;//种子名字
//确定，返回按钮
    private Button define;
    private Button back;
    //石否激活按钮
    public static  bool isGame;
    void Start()
    {
       TipPanel = GameObject.Find("Canvas").transform.Find("TIP3").gameObject;
       SeedPanel = TipPanel.transform.Find("种子信息面板").gameObject;
       Seedname = gameObject.name;
       Seedimage = gameObject.transform.GetComponent<Image>();
       Tipeedimage = SeedPanel.transform.Find("seedimage").GetComponent<Image>();
       nametext=SeedPanel.transform.Find("nameText/Text").GetComponent<Text>();
       seedmangetext=SeedPanel.transform.Find("seedmanage/Text").GetComponent<Text>();
       define = SeedPanel.transform.Find("确定").GetComponent<Button>();
       back = SeedPanel.transform.Find("返回").GetComponent<Button>();


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
            SeedContel.isGame = true;
            Destroy(collision.gameObject);
        }
    }

   public void OnCilk()
    {
        Debug.Log("1");
       
        
        if (isGame==false)
        { Debug.Log("2");
            TipPanel.SetActive(true);
            SeedPanel.SetActive(true);
            nametext.text = Seedname;
            Tipeedimage.sprite = Seedimage.sprite;
            switch (gameObject.name)
            {
                case "seed":
                    seedmangetext.text = "【洋甘菊】 \n具体种植信息未激活，请找到对应种子拖入激活具体信息\n如若继续请点击确定";
                    break;
                case"2":
                    seedmangetext.text = "";
                    break;
                    case "3": 
                 seedmangetext.text = "";
                 break;   
                    
            }
            
        }
    }
}
