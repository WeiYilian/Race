using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFace
{
    public UIFace(int currentFace)
    {
        CalculateOtherIndex(currentFace);
    }
    
   

    #region 相邻面的编号
    //当前面的编号
    private int faceIndex;
    
    //相邻面的编号
    private int upFaceIndex;
    private int downFaceIndex;
    private int rightFaceIndex;
    private int leftFaceIndex;
    
    public int FaceIndex => faceIndex;
    public int UpFaceIndex => upFaceIndex;
    public int DownFaceIndex => downFaceIndex;
    public int RightFaceIndex => rightFaceIndex;
    public int LeftFaceIndex => leftFaceIndex;
    
    #endregion

    private void CalculateOtherIndex(int currentFace)
    {
        faceIndex = currentFace;
        switch (faceIndex)
        {
            case 1 :
                upFaceIndex = 5;
                downFaceIndex = 6;
                leftFaceIndex = 4;
                rightFaceIndex = 2;
                break;
            case 2 :
                upFaceIndex = 5;
                downFaceIndex = 6;
                leftFaceIndex = 1;
                rightFaceIndex = 3;
                break;
            case 3 :
                upFaceIndex = 5;
                downFaceIndex = 6;
                leftFaceIndex = 2;
                rightFaceIndex = 4;
                break;
            case 4 :
                upFaceIndex = 5;
                downFaceIndex = 6;
                leftFaceIndex = 3;
                rightFaceIndex = 1;
                break;
            case 5 :
                upFaceIndex = 3;
                downFaceIndex = 1;
                leftFaceIndex = 4;
                rightFaceIndex = 2;
                break;
            case 6 :
                upFaceIndex = 3;
                downFaceIndex = 1;
                leftFaceIndex = 4;
                rightFaceIndex = 2;
                break;
            default:
                break;
        }
    }

}
