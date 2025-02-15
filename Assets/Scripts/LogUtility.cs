/****************************************************
    文件：LogUtility.cs
	作者：Azure
	功能：全局日志控制脚本
*****************************************************/

using UnityEngine;

public static class LogUtility
{
    // 仅在 Unity 编辑器或开发版本中输出日志
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    [System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
    public static void Log(object message)
    {
        Debug.Log(message);
    }

    // 可选：类似方法处理 LogWarning 和 LogError
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    [System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
    public static void LogWarning(object message)
    {
        Debug.LogWarning(message);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    [System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
    public static void LogError(object message)
    {
        Debug.LogError(message);
    }
}