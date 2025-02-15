/****************************************************
    文件：SingletonFgsCmd.cs
	作者：Azure
	功能：Nothing
*****************************************************/
using Fungus;
using System.Data.Common;
using System.Reflection.Emit;
using UnityEngine;

//在fungus block中调用UIManger.Instance.Push(new MainPanel)
[CommandInfo("Custom", "Call UIMangerMainPanel Push Method", "Calls a method in a UIManger class.")]
public class CallUIMangerPushMethod : Command
{
    public override void OnEnter()
    {
        if (UIManger.Instance != null)
        {
            // 调用方法
            UIManger.Instance.Push(new MainPanel());
            // 弹出tips
            UIManger.Instance.ShowTips("先去商店购买网和船", 3f);
        }
        else
        {
            LogUtility.LogError("UIManger instance is null!");
        }

        Continue();
    }

    public override string GetSummary()
    {
        return string.IsNullOrEmpty("methodName") ? "No method set" : "methodName";
    }

    public override Color GetButtonColor()
    {
        return new Color32(235, 191, 217, 255); // 自定义按钮颜色
    }
}
