using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class UIFaceManager : MonoBehaviour
{
   public static UIFaceManager Instance;
   
   public List<GameObject> UIFaceList;

   private void Awake()
   {
      if (Instance == null)
         Instance = this;
   }

   /// <summary>
   /// 获得当前面的对象以及相邻面的对象
   /// </summary>
   /// <param name="currentFace">当前面的编号</param>
   /// <returns></returns>
   public UIFace GetCurrentUIFace(int currentFace)
   {
      return new UIFace(currentFace);
   }
}
