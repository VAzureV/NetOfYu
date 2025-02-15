/****************************************************
    文件：MapPanel.cs
	作者：Azure
	功能：Nothing
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class MapPanel : BasePanel 
{
    private const string MapPanelName = "MapPanel";
    private const string MapPanelPath = "Prefabs/Panel/MapPanel";
    public MapPanel() : base(new UIType(MapPanelName, MapPanelPath)) { }
    public override void OnStart()
    {
        base.OnStart();
        UIMethods.FindObjectInChild(ActiveObj, "DockBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            AudioManger.Instance.PlaySound(AudioType.buttonSound);
            UIManger.Instance.Push(new MainPanel());
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