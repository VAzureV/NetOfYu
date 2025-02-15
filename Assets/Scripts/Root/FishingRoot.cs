/****************************************************
    文件：FishingRoot.cs
	作者：Azure
	功能：钓鱼场景根节点
*****************************************************/

using System.Collections.Generic;
using UnityEngine;

public class FishingRoot : MonoBehaviour 
{
    private List<int> fishNumList = new List<int>(FishNum);
    private List<int> garbageNumList = new List<int>(GarbageNum);
    private const int FishNum = 10;
    private const int GarbageNum = 6;
    private GameObject navPanel;
    private void Awake()
    {
        //初始化
        Init();
    }
    private void Init()
    {
        ResourceManager.Instance.Init();
        UIManger.Instance.Init();
        GameManger.Instance.Init();
        FishingSpotManger.Instance.Init();
        //初始化鱼数量
        for (int i = 0; i < FishNum; i++)
        {
            fishNumList[i] = 0;
        }
        //初始化垃圾数量
        for (int i = 0; i < GarbageNum; i++)
        {
            garbageNumList[i] = 0;
        }
        //载入航行UI
        GameObject navPanelPrefab = ResourceManager.Instance.Load<GameObject>("Prefabs/Panel/NavigationPanel");
        navPanel = GameObject.Instantiate(navPanelPrefab, UIManger.Instance.CanvasObj.transform);
    }
    public void AddFishNum(int fishID)
    {
        fishNumList[fishID]++;
    }
    public void AddGarbageNum(int garbageID)
    {
        garbageNumList[garbageID]++;
    }
}