using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIConter : MonoBehaviour
{
   
    private Transform ChangePanel;//获取选择面板
    private Transform Plthcama; //获取方块相机
  public  int index1 = 0;
    private Transform Planchange; //土地选择面板
   
    private Transform Seasonalselet; //获取季节方块
    private int index2;
    private Dropdown seaDropdown;//获取下拉框
    private Transform temselect;//温度选择
    private GameObject temsillder;
    private Text seedname;//种子名字
    private Transform sunlight;//阳光照射范围
    private Dropdown sunDropdown;//阳光照射范围下拉选项
    #region 各项选择的判断

    public static int Landidex=0;//土地选择三种情况
    public static int Seasonidex = 0;//季节选择三种情况
    public static int sunidex = 0;//阳光范围选择三种情况
    public static int temidex = 0;//温度选择三情况

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
        Plthcama = GameObject.Find("LandCamera").transform;
        ChangePanel = GameObject.Find("Canvas").transform.Find("TIP3").transform.Find("种植条件选择面板");
        seedname = ChangePanel.transform.Find("nameText/Text").GetComponent<Text>();//种子名称
        Seasonalselet = ChangePanel.transform.Find("季节选择");
        seaDropdown = Seasonalselet.transform.Find("季节").GetComponent<Dropdown>();
       
        temselect = ChangePanel.transform.Find("种植温度");
        temsillder = temselect.transform.Find("温度器").gameObject;
       sunlight = ChangePanel.transform.Find("阳光照射");
       sunDropdown = sunlight.transform.Find("照射选择").GetComponent<Dropdown>();
        Planchange = GameObject.Find("Canvas").transform.Find("TIP3").transform.Find("种植条件选择面板").transform.Find("土壤选择")
            .transform; //土壤选择面板
        change(); 
        OnlistTem();
        if (index1==0)
        {
            Landidex = 3;
        }

    }
    //土壤选择器
    void change()
    {
        //按钮点击事件
        Planchange.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
           
            //向左边选择角色 三元运算符

            index1 = index1.Equals(0) ? 2 : --index1;
            ShowPlayer(index1);
            switch (seedname.text)
            {
                case "洋甘菊":
                {
                    if (index1 == 1)
                    {
                        Debug.Log("土壤选择正确");
                        Landidex = 1;
                    }
                    else
                    {
                        Landidex = 0;
                    }
                } break;
                case "凤仙花":
                {
                    if (index1 == 2)
                    {
                        Debug.Log("土壤选择正确");
                        Landidex = 2;
                    }
                    else
                    {
                        Landidex = 0;
                    }
                } break;
                case "车前子":
                {
                    if (index1 == 0)
                    {
                        Debug.Log("土壤选择正确");
                        Landidex = 3;
                    }
                    else
                    {
                        Landidex = 0;
                    }
                } break;
            }
        });
        //土壤识别器
        Planchange.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            index1 = index1.Equals(2) ? 0 : ++index1;
            //向右边选择角色
            ShowPlayer(index1);
            switch (seedname.text)
            {
                case "洋甘菊":

                {
                    if (index1 == 1)
                    {
                        Debug.Log("土壤选择正确");
                        Landidex = 1;
                    }
                    else
                    {
                        Landidex = 0;
                    }
                } break;
                case "凤仙花":
                {
                    if (index1 == 2)
                    {
                        Debug.Log("土壤选择正确");
                        Landidex = 2;
                    }
                    else
                    {
                        Landidex = 0;
                    }
                } break;
                case "车前子":
                {
                    if (index1 == 0)
                    {
                        Debug.Log("土壤选择正确");
                        Landidex = 3;
                    }
                    else
                    {
                        Landidex = 0;
                    }
                } 
                    break;
            
            }
        });

        
        
    }

    void OnlistTem()
    {
        // 下拉框值改变时添加无参监听
        seaDropdown.onValueChanged.AddListener((int index) => DrowdownItemChanged());
        // // 下拉框值改变时添加含参监听
        // seaDropdown.onValueChanged.AddListener((int index)=> DropdownItemChanged(index));
        sunDropdown.onValueChanged.AddListener((int index) => Sundropmange());
    }

//气候识别器
    private void DrowdownItemChanged()
    {
        // 获取下拉框项目索引值
        int index = seaDropdown.value;
        Debug.Log(index);
        // 获取下拉框项目文本值
        var text = seaDropdown.options[index].text;
        Debug.Log(text);
        switch (seedname.text)
        {
            case "洋甘菊":

            {
                if (text == "秋")
                {
                    Debug.Log("气候选择正确");
                    Seasonidex = 1;
                }
                else
                {
                    Seasonidex = 0;
                }
            }
                break;
            case "凤仙花":

            {
                if (text == "春")
                {
                    Debug.Log("气候选择正确");
                    Seasonidex = 2;
                }
                else
                {
                    Seasonidex = 0;
                }
            }
                break;
            case "车前子":

            {
                if (text == "夏")
                {
                    Debug.Log("气候选择正确");
                    Seasonidex = 3;
                }
                else
                {
                    Seasonidex = 0;
                }
            }
                break;
               
        }
    }
//照射范围选择
    private void Sundropmange()
    {
        int index = sunDropdown.value;
        Debug.Log(index);
        // 获取下拉框项目文本值
        var text = sunDropdown.options[index].text;
        Debug.Log(text);
        switch (seedname.text)
        {
            case "洋甘菊":

            {
                if (text == "中后期")
                {
                    Debug.Log("照射选择正确");
                    sunidex = 1;
                }
                else
                {
                    sunidex = 0;
                }
            }
                break;
            case "凤仙花":

            {
                if (text == "前中后期")
                {
                    Debug.Log("照射选择正确");
                    sunidex = 2;
                }
                else
                {
                    sunidex = 0;
                }
            }
                break;
            case "车前子":

            {
                if (text == "前中期")
                {
                    Debug.Log("照射选择正确");
                    sunidex = 3;
                }
                else
                {
                    sunidex = 0;
                }
            }
                break;
        }
    }
//温度识别器
    public void OnValue()
    {
        float value = temsillder.transform.GetComponent<Slider>().value;
        
        switch (seedname.text)
        {
            case "洋甘菊":

            {
                if(value >= 15f && value <= 20f)
                {
                    Debug.Log("温度选择正确");
                    temidex = 1;
                }
                else
                {
                    temidex = 0;
                }
            }
                break;
            case "凤仙花":

            {
                if(value >= 20f && value <= 30f)
                {
                    Debug.Log("温度选择正确");
                    temidex = 2;
                }
                else
                {
                    temidex = 0;
                }
            }
                break;
            case "车前子":

            {
                if(value >= 20f )
                {
                    Debug.Log("温度选择正确");
                    temidex = 3;
                }
                else
                {
                    temidex = 0;
                }
            }
                break;
        }
        
        
    }


    private void ShowPlayer (int value)
    {
        for(int i = 0; i < Plthcama.childCount; i++)
        {
            Plthcama.GetChild(i).gameObject.SetActive(false);
        }
       Plthcama.GetChild(value).gameObject.SetActive(true );
       
    }

    public void close()
    {
        temsillder.transform.GetComponent<Slider>().value=0;
              
        sunDropdown.value=0;
        seaDropdown.value = 0;
        ShowPlayer(0);
        
    }
    
    
}
