using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIFaceManager : MonoBehaviour
{
   public static UIFaceManager Instance;
   
   public List<GameObject> UIFaceList;
   
   private OneFaceManager oneFaceManager;
   private TwoFaceManager twoFaceManager;
   private ThreeFaceMange threeFaceMange;
   private FourFaceMange fourFaceMange;
   public CanvasGroup m_canvasGroup;
   private GameObject message;
   public bool isGameOver;
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
       m_canvasGroup.transform.GetChild(0).GetComponent<Text>().text =Text;
       // 使消息提示框出现
       m_canvasGroup.alpha = 1;
       // 提示消息自动消失
       m_canvasGroup.DOFade(0, 3);
   }

   private void Start()
   {
       //测试用
       GameObject.Find("Canvas").transform.Find("GameOverBtnTest").GetComponent<Button>().onClick.AddListener(() =>
       {
           oneFaceManager = new OneFaceManager();
           twoFaceManager = new TwoFaceManager();
           threeFaceMange = new ThreeFaceMange();
           fourFaceMange = new FourFaceMange();
           oneFaceManager.Accomplish = true;
           twoFaceManager.Accomplish = true;
           threeFaceMange.Face3compement = true;
           fourFaceMange.Face4compement = true;
           GameOver();
       });
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


   #region 获得某个面的管理者

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

    /// <summary>
    /// 获得ThreeFaceMange的实例
    /// </summary>
    /// <returns></returns>
    public ThreeFaceMange GetThreeFaceMange()
    {
        if(threeFaceMange == null)
            threeFaceMange = new ThreeFaceMange();
        return threeFaceMange;
    }
    
    /// <summary>
    /// 获得FourFaceMange的实例
    /// </summary>
    /// <returns></returns>
    public FourFaceMange GetFourFaceMange()
    {
        if(fourFaceMange == null)
           fourFaceMange = new FourFaceMange();
        return fourFaceMange;
    }
    
    
    #endregion

    /// <summary>
    /// 判断四个面是否全部完成
    /// </summary>
    public void GameOver()
    {
        if (oneFaceManager.Accomplish && twoFaceManager.Accomplish && threeFaceMange.Face3compement && fourFaceMange.Face4compement)
        {
            //全部完成触发事件
            GameObject.Find("Canvas").transform.Find("EndPanel").gameObject.SetActive(true);
            isGameOver = true;
        }
        
    }
   
}
