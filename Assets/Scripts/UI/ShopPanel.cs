/****************************************************
    文件：ShopPanel.cs
	作者：Azure
	功能：商店界面
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : BasePanel 
{
    private const string ShopPanelName = "ShopPanel";
    private const string ShopPanelPath = "Prefabs/ShopPanel";
    public ShopPanel() : base(new UIType(ShopPanelName, ShopPanelPath)) { }

    public override void OnStart()
    {
        base.OnStart();
        //返回事件注册
        GameObject backBtnObj = UIMethods.FindObjectInChild(ActiveObj, "BackBtn");
        UIMethods.AddOrGetComponent<Button>(backBtnObj).onClick.AddListener(() =>
        {
            UIManger.Instance.Pop();
        });
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override void OnDisable()
    {
        base.OnDisable();
        ActiveObj.SetActive(false);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        ActiveObj.SetActive(true);
    }

    
}