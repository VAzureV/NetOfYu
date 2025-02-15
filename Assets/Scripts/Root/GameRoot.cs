/****************************************************
    文件：GameRoot.cs
	作者：Azure
	功能：开始场景入口
*****************************************************/

using System;
using UnityEngine;

public class GameRoot : MonoBehaviour 
{
    private static bool isInit = false;
    private void Awake()
    {
        //初始化
        if (!isInit) Init();
        //推入开始界面
        UIManger.Instance.Push(new StartGamePanel());
        //播放BGM
        AudioManger.Instance.PlayBGM(AudioType.Bgm1);
    }

    private void Init()
    {
        //管理器初始化
        MonoManager.Instance.Init();
        ResourceManager.Instance.Init();
        GameManger.Instance.Init();
        EventManger.Instance.Init();
        AudioManger.Instance.Init();
        UIManger.Instance.Init();
        FishingSpotManger.Instance.Init();
        //标记已初始化
        isInit = true;
    }
}