using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFace
{
    public UIFace(int currentFaceIndex)
    {
        mUIFaceManager = UIFaceManager.Instance;
        faceIndex = currentFaceIndex;
        currentFace = mUIFaceManager.UIFaceList[faceIndex];
        CalculateOtherIndex(currentFaceIndex);
    }

    private UIFaceManager mUIFaceManager;

    #region 存储面的编号
    //当前面
    private int faceIndex;
    private GameObject currentFace;
    //相邻面
    private GameObject upFace;
    private GameObject downFace;
    private GameObject rightFace;
    private GameObject leftFace;

    public GameObject CurrentFace => currentFace;
    public int FaceIndex => faceIndex;
    public GameObject UpFace => upFace;
    public GameObject DownFace => downFace;
    public GameObject RightFace => rightFace;
    public GameObject LeftFace => leftFace;

    #endregion

    //根据当前面的编号得到相邻面的编号
    private void CalculateOtherIndex(int currentFaceIndex)
    {
        switch (faceIndex)
        {
            case 1 :
                upFace = mUIFaceManager.UIFaceList[5];
                downFace = mUIFaceManager.UIFaceList[6];
                leftFace = mUIFaceManager.UIFaceList[4];
                rightFace = mUIFaceManager.UIFaceList[2];
                break;
            case 2 :
                upFace = mUIFaceManager.UIFaceList[5];
                downFace = mUIFaceManager.UIFaceList[6];
                leftFace = mUIFaceManager.UIFaceList[1];
                rightFace = mUIFaceManager.UIFaceList[3];
                break;
            case 3 :
                upFace = mUIFaceManager.UIFaceList[5];
                downFace = mUIFaceManager.UIFaceList[6];
                leftFace = mUIFaceManager.UIFaceList[2];
                rightFace = mUIFaceManager.UIFaceList[4];
                break;
            case 4 :
                upFace = mUIFaceManager.UIFaceList[5];
                downFace = mUIFaceManager.UIFaceList[6];
                leftFace = mUIFaceManager.UIFaceList[3];
                rightFace = mUIFaceManager.UIFaceList[1];
                break;
            case 5 :
                upFace = mUIFaceManager.UIFaceList[3];
                downFace = mUIFaceManager.UIFaceList[1];
                leftFace = mUIFaceManager.UIFaceList[4];
                rightFace = mUIFaceManager.UIFaceList[2];
                break;
            case 6 :
                upFace = mUIFaceManager.UIFaceList[1];
                downFace = mUIFaceManager.UIFaceList[3];
                leftFace = mUIFaceManager.UIFaceList[2];
                rightFace = mUIFaceManager.UIFaceList[4];
                break;
            default:
                break;
        }
    }

}
