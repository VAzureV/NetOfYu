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
        for (int i = 0; i < 10; i++)
        {
            fishNumList.Add(0);
        }
        for (int i = 0; i < 6; i++)
        {
            toolNumList.Add(0);
        }
        for (int i = 0; i < 6; i++)
        {
            resourceNumList.Add(0);
        }
        for (int i = 0; i < 6; i++)
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
                Debug.LogError("鱼数量不足");
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
                Debug.LogError("工具数量不足");
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
                Debug.LogError("资源数量不足");
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
                Debug.LogError("垃圾数量不足");
                return;
            }
            garbageNumList[index]--;
        }
    }
}

