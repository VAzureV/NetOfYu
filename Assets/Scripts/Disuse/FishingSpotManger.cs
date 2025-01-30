/****************************************************
    文件：FishingSpotManger.cs
	作者：Azure
	功能：Nothing
*****************************************************/

using System.Collections.Generic;
using UnityEngine;

public class FishingSpotManger : MonoBehaviour 
{
    public GameObject fishingSpotPrefab; // 钓鱼点的预制体
    // 钓鱼点生成区域大小
    public Vector2 minAreaSize = new Vector2(10, 10); 
    public Vector2 maxAreaSize = new Vector2(10, 10); 
    public int numberOfSpots = 5; // 要生成的钓鱼点数量
    public string[] fishTypes = { "Salmon", "Tuna", "Carp", "Bass", "Trout" };
    //public Dictionary<string, string> fishTypes = new Dictionary<string, string>();
    void Start()
    {
        GenerateFishingSpots();
    }
    void GenerateFishingSpots()
    {
        for (int i = 0; i < numberOfSpots; i++)
        {
            // 随机生成钓鱼点的位置
            Vector3 randomPosition = new Vector3(
                Random.Range(minAreaSize.x, maxAreaSize.x),
                Random.Range(minAreaSize.y, maxAreaSize.y),
                0
            );

            // 实例化钓鱼点
            GameObject spot = Instantiate(fishingSpotPrefab, randomPosition, Quaternion.identity);
            spot.name = $"FishingSpot_{i + 1}";
            spot.GetComponent<FishingSpot>().FishType = fishTypes[i];
            // 随机分配鱼类
            //string randomFish = fishTypes[Random.Range(0, fishTypes.Length)];
            //FishingSpot fishingSpot = spot.GetComponent<FishingSpot>();
            //fishingSpot.SetFishType(randomFish);
        }
    }
}