using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtenMana : MonoBehaviour
{    //传入是否达到温度来控制按钮
    //还有是否为指定编号先写一个控制编号的存储当前按钮的编号，
    //存储当前按钮的顺序如果当前按钮顺序正确则传给其他按钮能进行下
    [SerializeField] Sprite sprite;
    [SerializeField] Sprite sprite1;
    [SerializeField] Sprite sprite3;
    
    public static bool Isgame;
    

    public List<Button> sequence;

    private int currentPosition;
    
    private void Awake()
    {
       sequence = new List<Button>();
       currentPosition = 0;
    }


    public void OnButtonClick(Button button)
    {
       
       
            if (Isgame)
            {
                Debug.Log("开始游戏");
                if (button == sequence[currentPosition])
                {
                   
                    sequence[currentPosition].GetComponent<Image>().sprite = sprite;
                    currentPosition++;

                    if (currentPosition == sequence.Count)
                    { //TODO:成功获得奖励面板，face4 isover
                        Debug.Log("你完成了这个任务!");
                        
                        for(int i=0;i<currentPosition;i++)
                        {
                            sequence[i].GetComponent<Image>().sprite = sprite3;
                       
                        }
                        currentPosition = 0;
                    }
                }
                else
                {
                    Debug.Log("你按错了，要从头开始！");
                   for(int i=0;i<currentPosition;i++)
                   {
                       sequence[i].GetComponent<Image>().sprite = sprite1;
                       
                   }
                    
                    currentPosition = 0;
                    
                }
            }
            else
            {
                Debug.Log("不能开始游戏");
                for(int i=0;i<currentPosition;i++)
                {
                    sequence[i].GetComponent<Image>().sprite = sprite1;
                       
                }
            }
      

    }
}
