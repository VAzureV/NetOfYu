/****************************************************
    文件：LoadingPanel.cs
	作者：Azure
	功能：加载界面
*****************************************************/

using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadingPanel : BasePanel 
{
    private const string LoadingPanelName = "LoadingPanel";
    private const string LoadingPanelPath = "Prefabs/LoadingPanel";

    public LoadingPanel() : base(new UIType(LoadingPanelName, LoadingPanelPath)) { }

    public override void OnStart()
    {
        base.OnStart();
    }
    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
    public void UpdateLoadingBar(float process)
    {
        GameObject loadingSliderObj = UIMethods.FindObjectInChild(ActiveObj, "LoadingSlider");
        UIMethods.AddOrGetComponent<Slider>(loadingSliderObj).value = process;

        GameObject loadingNumObj = UIMethods.FindObjectInChild(loadingSliderObj, "LoadingNum");
        UIMethods.AddOrGetComponent<Text>(loadingNumObj).text = Convert.ToString(process * 100) + "%";
    }
}