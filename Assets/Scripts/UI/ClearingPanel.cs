/****************************************************
    文件：ClearingPanel.cs
	作者：Azure
	功能：结算界面
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class ClearingPanel : BasePanel 
{
    private const string ClearingPanelName = "ClearingPanel";
    private const string ClearingPanelPath = "Prefabs/Panel/ClearingPanel";
    public ClearingPanel() : base(new UIType(ClearingPanelName, ClearingPanelPath)) { }

    public override void OnStart()
    {
        base.OnStart();
        //确定按钮事件注册
        //GameObject backBtnObj = UIMethods.FindObjectInChild(ActiveObj, "BackBtn");
        //UIMethods.AddOrGetComponent<Button>(backBtnObj).onClick.AddListener(() =>
        //{
        //    UIManger.Instance.Pop();
        //});
        
    }
    public override void OnEnable()
    {
        base.OnEnable();
    }
    public override void OnDisable()
    {
        base.OnDisable();
    }
}