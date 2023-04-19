using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneFaceManager
{
    //判断两种拼图是否都完成
    private Dictionary<PuzzleType, bool> puzzleFinish = new Dictionary<PuzzleType, bool>();
    // 承接四个拼图的字典
    public Dictionary<int, Puzzle> PuzzleDic = new Dictionary<int, Puzzle>();
    // 拼图任务成功的数量
    private const int SucceedSum = 4;
    // 存放第一次双击的游戏物体
    public GameObject InitialObj;
    //判断是否完成所有任务
    public bool Accomplish;

    public OneFaceManager()
    {
        puzzleFinish.Add(PuzzleType.OneJigsawPuzzle,false);
        puzzleFinish.Add(PuzzleType.TwoJigsawPuzzle,false);
    }

    /// <summary>
    /// 检验拼图是否正确
    /// </summary>
    /// <returns></returns>
    public void PuzzleComplete()
    {
        // 以第一个拼图定义整个拼图类型
        PuzzleType puzzleType = PuzzleDic[1].PuzzleType;
        // 定义一个匹配度，当匹配度达到4时，即拼图成功
        int matchingSum = 0;
        for (int i = 1; i < SucceedSum+1; i++)
        {
            // 判断是否是同一类型的拼图
            if (puzzleType != PuzzleDic[i].PuzzleType) return;
            // 判断目标与当前是否匹配
            if (PuzzleDic[i].jpTargetIndex == PuzzleDic[i].jpCurIndex)
                matchingSum++;
        }

        if (matchingSum >= SucceedSum)
        {
            //Debug.Log("拼图完成");
            if (!puzzleFinish[puzzleType])
            {
                UIFaceManager.Instance.MessageonCtrol("拼图完成");
                puzzleFinish[puzzleType] = true;
            }

            if (puzzleFinish[PuzzleType.OneJigsawPuzzle] && puzzleFinish[PuzzleType.TwoJigsawPuzzle])
            {
                Accomplish = true;
                UIFaceManager.Instance.GameOver();
            }
        }
            
        //else
            //Debug.Log("拼图未完成");
    }

    /// <summary>
    /// 交换位置时对Puzzle的属性进行交换
    /// </summary>
    /// <param name="secondPuzzle"></param>
    public void ExchangePuzzleCharacteristic(Puzzle secondPuzzle)
    {
        Puzzle firstPuzzle = InitialObj.GetComponent<Puzzle>();

        // 在字典中对Puzzle进行换位
        PuzzleDic[firstPuzzle.jpCurIndex] = secondPuzzle;
        PuzzleDic[secondPuzzle.jpCurIndex] = firstPuzzle;

        // 对Puzzle中的jpCurIndex进行交换
        ExchangeTool(ref firstPuzzle.jpCurIndex,ref secondPuzzle.jpCurIndex);

        // 交换Puzzle中的位置、尺寸、状态与父物体信息
        ExchangeTool(ref firstPuzzle.originalPos,ref secondPuzzle.originalPos);
        ExchangeTool(ref firstPuzzle.originalSize,ref secondPuzzle.originalSize);
        ExchangeTool(ref firstPuzzle.PuzzleState,ref secondPuzzle.PuzzleState);
        ExchangeTool(ref firstPuzzle.ParentObj,ref secondPuzzle.ParentObj);
        // 判断是否完成拼图
        PuzzleComplete();
    }

    /// <summary>
    /// 交换数据的工具
    /// </summary>
    /// <param name="obj1">数据一</param>
    /// <param name="obj2">数据二</param>
    /// <typeparam name="T"></typeparam>
    private void ExchangeTool<T>(ref T obj1,ref T obj2)
    {
        T t = obj1;
        obj1 = obj2;
        obj2 = t;
    }
    

    /// <summary>
    /// 变换大小时交换属性
    /// </summary>
    public void VariablePuzzleCharacteristic(Puzzle puzzle)
    {
        Puzzle parentPuzzle = puzzle.ParentObj.transform.GetComponent<Puzzle>();
        switch (puzzle.PuzzleState)
        {
            case PuzzleState.Small:
                // 在字典中对Puzzle进行换位
                PuzzleDic[parentPuzzle.jpCurIndex] = puzzle;
                // 对Puzzle中的jpCurIndex进行交换
                ExchangeTool(ref parentPuzzle.jpCurIndex,ref puzzle.jpCurIndex);
                break;
            case PuzzleState.Big:
                // 在字典中对Puzzle进行换位
                PuzzleDic[puzzle.jpCurIndex] = parentPuzzle;
                // 对Puzzle中的jpCurIndex进行交换
                ExchangeTool(ref parentPuzzle.jpCurIndex,ref puzzle.jpCurIndex);
                break;
            case PuzzleState.NotVariable:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        // 将第一次点击时存储的游戏物体释放掉
        InitialObj = null;

        // 判断是否完成拼图
        PuzzleComplete();
    }
}
