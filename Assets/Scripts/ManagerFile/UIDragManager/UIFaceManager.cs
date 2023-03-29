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
   public void GetCurrentUIFace(int currentFace,DragDirection dragDirection,out GameObject newFace,out int newFaceIndex)
   {
       UIFace uiFace = new UIFace(currentFace);
       switch (dragDirection)
       {
           case DragDirection.UP:
               newFace = UIFaceList[uiFace.UpFaceIndex];
               newFaceIndex = uiFace.UpFaceIndex;
               break;
           case DragDirection.DOWN:
               newFace = UIFaceList[uiFace.DownFaceIndex];
               newFaceIndex = uiFace.DownFaceIndex;
               break;
           case DragDirection.RIGHT:
               newFace = UIFaceList[uiFace.RightFaceIndex];
               newFaceIndex = uiFace.RightFaceIndex;
               break;
           case DragDirection.LEFT:
               newFace = UIFaceList[uiFace.LeftFaceIndex];
               newFaceIndex = uiFace.LeftFaceIndex;
               break;
           default:
               throw new ArgumentOutOfRangeException(nameof(dragDirection), dragDirection, null);
       }
   }
}
