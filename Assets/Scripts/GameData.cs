/****************************************************
    文件：GameData.cs
	作者：Azure
	功能：游戏数据类
*****************************************************/

using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
	private List<int> fishNumList = new List<int>();
    private List<int> toolNumList = new List<int>();
    private List<int> resourceNumList = new List<int>();
    private List<int> garbageNumList = new List<int>();
    public const int FishNum = 16;
    public const int ToolNum = 6;
    public const int ResourceNum = 6;
    public const int GarbageNum = 6;
    public float BgmVolume {  get; set; }
	public float SoundVolume { get; set; }
	public int Gold {  get; set; }
	public int PolutionVal { get; set; }
	public bool OpeningDialog {  get; set; }
    public GameData() 
    {
        BgmVolume = 0.5f;
        SoundVolume = 0.5f;
        Gold = 0;
        PolutionVal = 0;
        OpeningDialog = true;
        for (int i = 0; i < FishNum; i++)
        {
            fishNumList.Add(0);
        }
        for (int i = 0; i < ToolNum; i++)
        {
            toolNumList.Add(0);
        }
        for (int i = 0; i < ResourceNum; i++)
        {
            resourceNumList.Add(0);
        }
        for (int i = 0; i < GarbageNum; i++)
        {
            garbageNumList.Add(0);
        }
    }
    public void AddFishNum(int index, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            fishNumList[index]++;
        }
    }
    public void AddToolNum(int index, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            toolNumList[index]++;
        }
    }
    public void AddResourceNum(int index, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            resourceNumList[index]++;
        }
    }
    public void AddGarbageNum(int index, int count = 1)
    {
        for (var i = 0; i < count; i++)
        {
            garbageNumList[index]++;
        }
    }
    public void DelFishNum(int index, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            if (fishNumList[index] <= 0)
            {
                LogUtility.LogError("鱼数量不足");
                return;
            }
            fishNumList[index]--;
        }
    }
    public void DelToolNum(int index, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            if (toolNumList[index] <= 0)
            {
                LogUtility.LogError("工具数量不足");
                return;
            }
            toolNumList[index]--;
        }
    }
    public void DelResourceNum(int index, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            if (resourceNumList[index] <= 0)
            {
                LogUtility.LogError("资源数量不足");
                return;
            }
            resourceNumList[index]--;
        }
    }
    public void DelGarbageNum(int index, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            if (garbageNumList[index] <= 0)
            {
                LogUtility.LogError("垃圾数量不足");
                return;
            }
            garbageNumList[index]--;
        }
    }
    public int GetFishNum(int index)
    {
        return fishNumList[index];
    }
    public int GetToolNum(int index)
    {
        return toolNumList[index];
    }
    public int GetResourceNum(int index)
    {
        return resourceNumList[index];
    }
    public int GetGarbageNum(int index)
    {
        return garbageNumList[index];
    }
    public void Clear()
    {
        for (int i = 0; i < FishNum; i++)
        {
            fishNumList[i] = 0;
        }
        for (int i = 0; i < ToolNum; i++)
        {
            toolNumList[i] = 0;
        }
        for (int i = 0; i < ResourceNum; i++)
        {
            resourceNumList[i] = 0;
        }
        for (int i = 0; i < GarbageNum; i++)
        {
            garbageNumList[i] = 0;
        }
    }
    public int GetBoatLevel()
    {
        if (toolNumList[2] != 0) return 3;
        if (toolNumList[1] != 0) return 2;
        if (toolNumList[0] != 0) return 1;
        return 0;
    }
    public int GetFishingRodLevel()
    {
        if (toolNumList[5] != 0) return 3;
        if (toolNumList[4] != 0) return 2;
        if (toolNumList[3] != 0) return 1;
        return 0;
    }
}

