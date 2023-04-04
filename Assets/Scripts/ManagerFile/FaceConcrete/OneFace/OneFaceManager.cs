using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneFaceManager
{
    // 承接四个拼图的字典
    public Dictionary<int, Puzzle> PuzzleDic = new Dictionary<int, Puzzle>();
    // 拼图任务成功的数量
    public int SucceedSum;
    // 存放第一次双击的游戏物体
    public GameObject InitialObj;

    /// <summary>
    /// 检验拼图是否正确
    /// </summary>
    /// <returns></returns>
    public bool PuzzleComplete()
    {
        // 为第一个拼图定义类型
        PuzzleType puzzleType = PuzzleDic[0].jpType;
        // 定义一个匹配度，当匹配度达到4时，即拼图成功
        int matchingSum = 0;
        foreach (var puzzle in PuzzleDic.Values)
        {
            // 判断是否是同一类型的拼图
            if (puzzleType != puzzle.jpType) return false;
            // 判断目标与当前是否匹配
            if (puzzle.jpTargetIndex == puzzle.jpCurIndex)
                matchingSum++;
        }

        if (matchingSum >= SucceedSum)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 交换时对Puzzle的属性进行交换
    /// </summary>
    /// <param name="secondPuzzle"></param>
    public void ExchangePuzzleCharacteristic(Puzzle secondPuzzle)
    {
        // 在字典中对Puzzle进行换位
        Puzzle firstPuzzle = InitialObj.GetComponent<Puzzle>();
        PuzzleDic[firstPuzzle.jpCurIndex] = secondPuzzle;
        PuzzleDic[secondPuzzle.jpCurIndex] = firstPuzzle;
        
        // 对Puzzle中的jpCurIndex进行交换
        int index = firstPuzzle.jpCurIndex;
        firstPuzzle.jpCurIndex = secondPuzzle.jpCurIndex;
        secondPuzzle.jpCurIndex = index;
        
        // 交换Puzzle中的位置与尺寸信息
        Vector3 pos = firstPuzzle.originalPos;
        firstPuzzle.originalPos = secondPuzzle.originalPos;
        secondPuzzle.originalPos = pos;
        Vector2 size = firstPuzzle.originalSize;
        firstPuzzle.originalSize = secondPuzzle.originalSize;
        firstPuzzle.originalSize = size;
    }
}
