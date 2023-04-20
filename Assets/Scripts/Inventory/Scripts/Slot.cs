using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//格子的文字图片设置
public class Slot : MonoBehaviour
{
    //记住编号顺序
    public int slotId;//空格
    //获取到主键图片
    public Item slotItem;
    public Image SlotImage;
   private Image bagimage;
    public string slotInfo;

    public GameObject iteminsolt;
   //点击ui物品
    public void ItemOnClicked()
    {
        if (UIFaceManager.Instance.isGameOver)
        return;
        inventoryManger.UpdateItemInfo(slotInfo);
        bagimage.sprite = SlotImage.sprite;
    }

    private void Start()
    {
        bagimage = GameObject.Find("Canvas").transform.Find("Objectmange").transform.Find("Objimage")
            .GetComponent<Image>();
    }
    public void setslot(Item item)
    {
        if (item == null)
        {
            iteminsolt.SetActive(false);
            return;
        }

        SlotImage.sprite = item.itemImage;
       
        slotInfo = item.iteminfo;

    }
}