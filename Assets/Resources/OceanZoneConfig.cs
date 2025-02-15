/****************************************************
    文件：OceanZoneConfig.cs
	作者：Azure
	功能：捕鱼玩法相关配置
*****************************************************/

using UnityEngine;

// 海域类型枚举
public enum OceanZoneType
{
    Shallow,   // 浅滩（低难度）
    MidOcean,   // 中海（中等难度）
    OpenSea     // 远洋（高难度）
}

// QTE参数范围配置
[System.Serializable]
public class QTEParameterRange
{
    [Header("游标参数")]
    [Tooltip("游标移动速度（单位/秒）")]
    public Vector2 cursorSpeedRange = new Vector2(0.8f, 1.2f);

    [Header("成功区域参数")]
    [Tooltip("区域宽度比例")]
    public Vector2 zoneSizeRange = new Vector2(0.3f, 0.5f);

    [Tooltip("区域起始位置范围（0~1）")]
    public Vector2 zonePosRange = new Vector2(0.2f, 0.8f);

    [Header("鱼数量范围")]
    public Vector2 FishNum = new Vector2(1, 5);
}

// 海域类型配置资产
[CreateAssetMenu(menuName = "Fishing/Ocean Zone Config")]
public class OceanZoneConfig : ScriptableObject
{
    public OceanZoneType zoneType;
    public QTEParameterRange qteParams;
    public Vector2 MinZoneRange = new Vector2(-14, -4);
    public Vector2 MaxZoneRange = new Vector2(14, 95);
    public Vector2 FishTypes = new Vector2(0, 5);
    public Color zoneColor = Color.white; // UI高亮色
}