using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
//背包系统的主体设为一个单例可以供道具和背包格显示
public class inventoryManger : MonoBehaviour
{public  Inventory playerInventory;
    public GameObject bagmange;//背包信息面板
    public Image bagmangimage;//信息面板图片
    private static inventoryManger instance;
    
    public Inventory mybag;
    public GameObject slotGrid;
    // public Slot slotPrefab;
    public GameObject Emptyslot;
    public Text itemInfromation;
    public List<GameObject> slots = new List<GameObject>();

    void Awake()
    {NULLItem();
        if(instance!=null)
            Destroy(this);
        instance = this;
    }

    
        
    
    public void NULLItem()
    {
        for (int k = 0; k < playerInventory.ItemList.Count;k++)
        {
           
            playerInventory.ItemList[k] = null;
            
          
            
        }
    }
    void OnEnable()
    {
        //清空提示栏，并更新背包物品列表
        instance.itemInfromation.text = "";
        Refreshtiem();
    }

    void Update()
    {
      
    }
    public static void UpdateItemInfo(string itemDescrition)
    {
       instance.bagmange.gameObject.SetActive(true);
       
        //更新文字信息并且在下面提示栏显示
        instance.itemInfromation.text = itemDescrition;
    }
    

//背包里面数据更新并且每次拖动的时候从新排列
    public static void Refreshtiem()
    {
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount==0)
            {//没有放东西时
                break;
            }  Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            instance.slots.Clear();
        }
//c重新生成物品
        for (int  i= 0;  i< instance.mybag.ItemList.Count; i++)
        {
            //遍历列表自己创建的背包里面一个个找
            // createNewItem(instance.mybag.ItemList[i]);
            instance.slots.Add(Instantiate(instance.Emptyslot));
            instance.slots[i].transform.SetParent(instance.slotGrid.transform);

            instance.slots[i].GetComponent<Slot>().slotId = i;
            
            instance.slots[i].GetComponent<Slot>().setslot(instance.mybag.ItemList[i]);
        }
    }
}