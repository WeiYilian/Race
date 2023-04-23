using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ButtenMana : MonoBehaviour
{
    //传入是否达到温度来控制按钮
    //还有是否为指定编号先写一个控制编号的存储当前按钮的编号，
    //存储当前按钮的顺序如果当前按钮顺序正确则传给其他按钮能进行下
    [SerializeField] Sprite sprite;
    [SerializeField] Sprite sprite1;

    [SerializeField] Sprite sprite3;
    [SerializeField] Sprite sprite4;
    
    public float duration = 1f; // 平移时间
    //游戏是否开始 
    public static bool isgame;
    public static bool isslider;
    public int time = 0;
    private FourFaceMange fourFaceMange;
    private int currentPosition;
    private GameObject leftdrool;
    private GameObject rightdrool;
    public bool Isgame
    {
        get => isgame;
        set => isgame = value;
    }

    //按钮
    public List<Button> sequence;

//携程控制更改图片
    private IEnumerator LightUpButton()
    {
        foreach (var btn in sequence)
        {
            btn.GetComponent<Image>().sprite = sprite4;
            yield return new WaitForSeconds(0.5f);
            btn.GetComponent<Image>().sprite = sprite1;
            yield return new WaitForSeconds(0.5f);
        }

        
       

       

    }
    //开门携程
    IEnumerator MoveToPosition(Vector3 targetPosition, float time)
    {
        float elapsedTime = 0;
        Vector3 startingPos = leftdrool.transform.position;
        
        while (elapsedTime < time)
        {
          leftdrool.transform.position = Vector3.Lerp(startingPos, targetPosition, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
       leftdrool.transform.position = targetPosition;
    }
    IEnumerator MoveToPosition1(Vector3 targetPosition, float time)
    {
        float elapsedTime = 0;
        Vector3 startingPos = rightdrool.transform.position;
        
        while (elapsedTime < time)
        {
            rightdrool.transform.position = Vector3.Lerp(startingPos, targetPosition, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
       rightdrool.transform.position = targetPosition;
    }

private void Start()
    {
    fourFaceMange = UIFaceManager.Instance.GetFourFaceMange();
    leftdrool = GameObject.Find("leftdoor").gameObject;
   rightdrool = GameObject.Find("rightdoor").gameObject;
    } 
public void OnButtonClick(Button button)
    {
        //调用携程
        
       
            if (isgame)
            {
                
                if (fourFaceMange.Face4compement == true)
                 return;
                
                if (!isslider)
                {
                    GameObject.Find("Slider").gameObject.SetActive(false);
                    isslider = true;
                }
               
                // isgame = true;



                Debug.Log("开始游戏");
                if (button == sequence[currentPosition])
                {
                   
                    sequence[currentPosition].GetComponent<Image>().sprite = sprite;
                    currentPosition++;

                    if (currentPosition == sequence.Count)
                    { //TODO:成功获得奖励面板，face4 isover
                      fourFaceMange.Face4compement  = true;
                      UIFaceManager.Instance.MessageonCtrol("机关门已开启");
                        GameObject.Find("droolsc").gameObject.SetActive(false);
                        StartCoroutine(MoveToPosition(leftdrool.gameObject.transform.position + new Vector3(0f, 0f, -40f), duration));//开门
                        StartCoroutine(MoveToPosition1(rightdrool.gameObject.transform.position + new Vector3(0f, 0f, 42f), duration));
                        for(int i=0;i<currentPosition;i++)
                        {
                            sequence[i].GetComponent<Image>().sprite = sprite3;
                       
                        }
                        currentPosition = 0;
                    }
                }
                else
                {if (fourFaceMange.Face4compement == true)
                        return;
                    UIFaceManager.Instance.MessageonCtrol("请按顺序进行！");
                    GameObject.Find("rightdoor").transform.Find("Slider").gameObject.SetActive(true);
                    isslider = false;
                   for(int i=0;i<currentPosition;i++)
                   {
                       sequence[i].GetComponent<Image>().sprite = sprite1;
                       
                   }
                    
                    currentPosition = 0;
                    
                }
            }
            else
            {
                if (fourFaceMange.Face4compement == true)
                    return;
                UIFaceManager.Instance.MessageonCtrol("请激活当前机关");
                StartCoroutine(LightUpButton());
                for(int i=0;i<currentPosition;i++)
                {
                    sequence[i].GetComponent<Image>().sprite = sprite1;
                       
                }
            }
      

    }
}
