/****************************************************
    文件：BagPanel.cs
	作者：Azure
	功能：Nothing
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class BagPanel : BasePanel 
{
    private const string BagPanellName = "BagPanel";
    private const string BagPanelPath = "Prefabs/BagPanel";
    public BagPanel() : base(new UIType(BagPanellName, BagPanelPath)) { }
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
    public override void OnEnable()
    {
        base.OnEnable();

    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        ActiveObj.SetActive(false);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        ActiveObj.SetActive(false);
    }

    private void ReFreshBagPanel(ItemType itemType)
    {
        //刷新背包
        switch (itemType)
        {
            case ItemType.Fish:

                break;
            case ItemType.Tool:
                break;
            case ItemType.Resourece:
                break;
            case ItemType.Garrbage:
                break;
        }
    }
}