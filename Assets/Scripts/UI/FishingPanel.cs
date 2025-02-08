/****************************************************
    文件：FishingPanel.cs
	作者：Azure
	功能：Nothing
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class FishingPanel : BasePanel 
{
    private const string FishingPanelName = "FishingPanel";
    private const string FishingPanelPath = "Prefabs/Panel/FishingPanel";
    private FishingSpot curFishingSpot;
    public FishingPanel(FishingSpot fishingSpot) : base(new UIType(FishingPanelName, FishingPanelPath))
    {
        curFishingSpot = fishingSpot;
    }

    public override void OnStart()
    {
        base.OnStart();
        // 关闭按钮
        GameObject exitBtnObj = UIMethods.FindObjectInChild(ActiveObj, "ExitBtn");
        UIMethods.AddOrGetComponent<Button>(exitBtnObj).onClick.AddListener(() =>
        {
            UIManger.Instance.Pop();
        });
        // 钓鱼按钮
    }
    public override void OnEnable()
    {
        base.OnEnable();
    }
    public override void OnDisable()
    {
        base.OnDisable();
        ActiveObj.SetActive(false);
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        ActiveObj.SetActive(false);
    }

}