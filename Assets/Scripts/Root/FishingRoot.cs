/****************************************************
    文件：FishingRoot.cs
	作者：Azure
	功能：钓鱼场景根节点
*****************************************************/

using UnityEngine;

public class FishingRoot : MonoBehaviour 
{
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
    }
}