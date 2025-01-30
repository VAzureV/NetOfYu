/****************************************************
    文件：GameData.cs
	作者：Azure
	功能：游戏数据类
*****************************************************/

using UnityEngine;
using System;
using System.Collections.Generic;
public enum FishType
{
    Croaker,
    Grouper,
    Pomfret,
    Seaeel,
    SeaRabbit,
}
[System.Serializable]
public class GameData
{
	public float BgmVolume {  get; set; }
	public float SoundVolume { get; set; }
	public int Gold {  get; set; }
	public int PolutionVal { get; set; }
	public bool OpeningDialog {  get; set; }
}

