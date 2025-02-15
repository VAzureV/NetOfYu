/****************************************************
    文件：FishingRoot.cs
	作者：Azure
	功能：钓鱼场景根节点
*****************************************************/

using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class FishingRoot : MonoBehaviour 
{
    public const int FishNum = 16;
    public const int GarbageNum = 6;
    private int[] fishNumList = new int[FishNum];
    private int[] garbageNumList = new int[GarbageNum];
    private GameObject navPanel;
    private void Awake()
    {
        //初始化
        Init();
    }
    private void Init()
    {
        //播放出海BGM
        AudioManger.Instance.PlayBGM(AudioType.FishingBgm);
        //更新UI管理器
        UIManger.Instance.DestroyAll();
        UIManger.Instance.UpdateCanvas();

        //载入航行UI
        GameObject navPanelPrefab = ResourceManager.Instance.Load<GameObject>("Prefabs/Panel/NavigatePanel");
        navPanel = GameObject.Instantiate(navPanelPrefab, UIManger.Instance.CanvasObj.transform);

        //在地图上生成钓鱼点
        FishingSpotManger.Instance.GenerateFishingSpots();

        //生成垃圾
        GenerateGarbage();

        //初始化当前所捕鱼数量
        for (int i = 0; i < FishNum; i++)
        {
            fishNumList[i] = 0;
        }
        //初始化当前所获得垃圾数量
        for (int i = 0; i < GarbageNum; i++)
        {
            garbageNumList[i] = 0;
        }
        
    }
    private void GenerateGarbage()
    {
        //生成垃圾
        GameObject garbagePrefab = ResourceManager.Instance.Load<GameObject>("Prefabs/Fishing/Garbage");
        int numberOfGarbage = Random.Range(15, 20);
        for (int i = 0; i < numberOfGarbage; i++)
        {
            // 全图随机生成垃圾位置
            Vector3 randomPosition = new Vector3(
            Random.Range(-14, 14),
            Random.Range(-2, 90),
                0
            );
            // 实例化垃圾
            GameObject garbage = GameObject.Instantiate(garbagePrefab, randomPosition, Quaternion.identity);
            // 设置垃圾ID
            garbage.GetComponent<GarbageTriger>().garbageID = Random.Range(0, GarbageNum);
            garbage.GetComponent<GarbageTriger>().fishingRoot = this;
        }
    }
    public int GetFishNum(int fishID)
    {
        return fishNumList[fishID];
    }
    public int GetGarbageNum(int garbageID)
    {
        return garbageNumList[garbageID];
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