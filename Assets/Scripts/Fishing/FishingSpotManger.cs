/****************************************************
    文件：FishingSpotManger.cs
	作者：Azure
	功能：捕鱼点生成管理器
*****************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishingSpotManger : Singleton<FishingSpotManger> 
{
    private GameObject fishingSpotPrefab; // 钓鱼点的预制体
    public OceanZoneConfig shallowConfig;
    public OceanZoneConfig midOceanConfig;
    public OceanZoneConfig openSeaConfig;

    //private Vector2 numberOfSpotsRange = new Vector2(3, 4); // 要生成的捕鱼点数量范围
    public void Init()
    {
        fishingSpotPrefab = ResourceManager.Instance.Load<GameObject>("Prefabs/Fishing/FishingSpot");
        shallowConfig = ResourceManager.Instance.Load<OceanZoneConfig>("Config/ShallowsConfig");
        midOceanConfig = ResourceManager.Instance.Load<OceanZoneConfig>("Config/MidOceanConfig");
        openSeaConfig = ResourceManager.Instance.Load<OceanZoneConfig>("Config/OpenSeaConfig");
    }
    public void GenerateFishingSpots()
    {
        foreach (OceanZoneType zone in Enum.GetValues(typeof(OceanZoneType)))
        {
            switch (zone)
            {
                case OceanZoneType.Shallow:
                    GenerateFishingSpots(shallowConfig);
                    break;
                case OceanZoneType.MidOcean:
                    GenerateFishingSpots(midOceanConfig);
                    break;
                case OceanZoneType.OpenSea:
                    GenerateFishingSpots(openSeaConfig);
                    break;
            }

        }
        

        
    }
    private void GenerateFishingSpots(OceanZoneConfig config)
    {
        int numberOfSpots = Random.Range(3, 5);
        for (int i = 0; i < numberOfSpots; i++)
        {
            // 随机生成钓鱼点的位置
            Vector3 randomPosition = new Vector3(
                Random.Range(config.MinZoneRange.x, config.MaxZoneRange.x),
                Random.Range(config.MinZoneRange.y, config.MaxZoneRange.y),
                0
            );
            // 实例化钓鱼点
            GameObject spot = GameObject.Instantiate(fishingSpotPrefab, randomPosition, Quaternion.identity);
            spot.GetComponent<FishingSpot>().BelongOcean = config.zoneType;
            LogUtility.Log("生成了一个钓鱼点" + spot.GetComponent<FishingSpot>().BelongOcean.ToString());
            // 随机分配鱼类
        }

    }
}