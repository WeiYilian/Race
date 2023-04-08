using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIFaceManager : MonoBehaviour
{
   public static UIFaceManager Instance;
   
   public List<GameObject> UIFaceList;

   private TwoFaceManager twoFaceManager;
   
   private OneFaceManager oneFaceManager;
   
   public CanvasGroup m_canvasGroup;
   
   private void Awake()
   {
      if (Instance == null)
         Instance = this;
      
   }
   

   /// <summary>
   /// 打开消息提示框，并控制消息自动消失
   /// </summary>
   public void MessageonCtrol(string Text)
   {
       // 填充文字
       m_canvasGroup.transform.Find("MessageText").GetComponent<Text>().text = Text;
       // 使消息提示框出现
       m_canvasGroup.alpha = 1;
       // 提示消息自动消失
     

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

   
   
   /// <summary>
   /// 获得OneFaceManager的实例
   /// </summary>
   /// <returns></returns>
   public OneFaceManager GetOneFaceManager()
   {
       if(oneFaceManager == null)
           oneFaceManager = new OneFaceManager();
       return oneFaceManager;
   }
   
   
   /// <summary>
   /// 获得TwoFaceManager的实例
   /// </summary>
   /// <returns></returns>
   public TwoFaceManager GetTwoFaceManager()
   {
       if(twoFaceManager == null)
           twoFaceManager = new TwoFaceManager();
       return twoFaceManager;
   }
   
}
