using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New inventory",menuName = "Inventory/New inventory")]
public class Inventory : ScriptableObject
{
    //背包，用列表存储我的物品
    public List<Item> ItemList = new List<Item>();
}