/****************************************************
    文件：GameRoot.cs
	作者：Azure
	功能：开始场景入口
*****************************************************/

using System;
using UnityEngine;

public class GameRoot : MonoBehaviour 
{
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        //管理器初始化
        GameManger.Instance.Init();
        MonoManager.Instance.Init();
        ResourceManager.Instance.Init();
        EventManger.Instance.Init();
        AudioManger.Instance.Init();
        UIManger.Instance.Init();

        //推入开始界面
        UIManger.Instance.Push(new StartGamePanel());

        //播放BGM
        AudioManger.Instance.PlayBGM(AudioType.Bgm1);
    }
}