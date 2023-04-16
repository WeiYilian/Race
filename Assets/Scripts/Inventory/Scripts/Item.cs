using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//背包菜单
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item :ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int itemHeld;
    //文本框
    [TextArea]
    public string iteminfo;

    public bool equip;

}

