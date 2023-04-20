using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//游戏开始对背包初始化生成相对应的背包格子对其物品的存储
public class itemOnword : MonoBehaviour
{
    public Item ThisItem;

    public  Inventory playerInventory;

    public void onclik()
    {
        
            AddNewItem();
            Destroy(gameObject);

        
      
    }

    public void Start()
    {
        NULLItem();
    }

    public void AddNewItem()
    {
        if (!playerInventory.ItemList.Contains(ThisItem))
        {
            // playerInventory.ItemList.Add(ThisItem);
            // inventoryManger.createNewItem(ThisItem);
            for (int i = 0; i < playerInventory.ItemList.Count; i++)
            {
                if (playerInventory.ItemList[i] == null)
                {
                    playerInventory.ItemList[i] = ThisItem;
                    break;
                }
            }
        }
        else
        {
            ThisItem.itemHeld += 1;
        }
        inventoryManger.Refreshtiem();
    }

    public void NULLItem()
    {
        for (int k = 0; k < playerInventory.ItemList.Count;)
        {
           
                playerInventory.ItemList[k] = null;
                k = k + 1;
                break;
            
        }
    }
}
