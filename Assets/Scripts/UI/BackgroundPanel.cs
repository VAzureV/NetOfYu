/****************************************************
    文件：BackgroundPanel.cs
	作者：Azure
	功能：Nothing
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class BackgroundPanel : BasePanel
{
    private const string BackgroundPanelName = "BackgroundPanel";
    private const string BackgroundPanelPath = "Prefabs/BackgroundPanel";
    private string bgImageName;
    private string backgroundImagePath;
    public BackgroundPanel(string bgImageName) : base(new UIType(BackgroundPanelName, BackgroundPanelPath))
    { 
        this.bgImageName = bgImageName;
        this.backgroundImagePath = "Image/Bg/" + bgImageName;
    }

    public override void OnStart()
    {
        base.OnStart();
        GameObject bgImageObj = UIMethods.FindObjectInChild(ActiveObj, "BgImage");
        UIMethods.AddOrGetComponent<RawImage>(bgImageObj).texture = ResourceManager.Instance.Load<Texture>(backgroundImagePath);
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

}