/****************************************************
    文件：MainPanel.cs
	作者：Azure
	功能：码头主界面
*****************************************************/

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : BasePanel 
{
    private const string MainPanellName = "MainPanel";
    private const string MainPanelPath = "Prefabs/Panel/MainPanel";
    public MainPanel() : base(new UIType(MainPanellName, MainPanelPath)) { }

    public override void OnStart()
    {
        base.OnStart();

        //读取金币数据
        UpdateGold();
        //写入数据
        GameManger.Instance.CurGameData.OpeningDialog = GameManger.Instance.CurFlowchat.GetBooleanVariable("Opening");

        //作弊按钮（增加金币）
        GameObject addGoldBtnObj = UIMethods.FindObjectInChild(ActiveObj, "AddGoldBtn");
        UIMethods.AddOrGetComponent<Button>(addGoldBtnObj).onClick.AddListener(() =>
        {
            GameManger.Instance.CurGameData.Gold += 1000;
            UpdateGold();
        });

        //返回大地图按钮事件注册
        GameObject toMapBtnObj = UIMethods.FindObjectInChild(ActiveObj, "ToMapBtn");
        UIMethods.AddOrGetComponent<Button>(toMapBtnObj).onClick.AddListener(() =>
        {
            //AudioManger.Instance.PlaySound(AudioType.buttonSound);
            UIManger.Instance.Push(new MapPanel());
        });


        //进入背包界面
        GameObject BagBtnObj = UIMethods.FindObjectInChild(ActiveObj, "BagBtn");
        UIMethods.AddOrGetComponent<Button>(BagBtnObj).onClick.AddListener(() =>
        {
            AudioManger.Instance.PlaySound(AudioType.buttonSound);
            UIManger.Instance.Push(new BagPanel());
        });
        //进入商店界面
        GameObject ShopBtnObj = UIMethods.FindObjectInChild(ActiveObj, "ShopBtn");
        UIMethods.AddOrGetComponent<Button>(ShopBtnObj).onClick.AddListener(() =>
        {
            AudioManger.Instance.PlaySound(AudioType.buttonSound);
            UIManger.Instance.Push(new ShopPanel());
        });
        //进入图鉴界面
        GameObject HandbookBtnObj = UIMethods.FindObjectInChild(ActiveObj, "HandbookBtn");
        UIMethods.AddOrGetComponent<Button>(HandbookBtnObj).onClick.AddListener(() =>
        {
            
        });
        //进入工作坊界面
        GameObject WorkshopBtnObj = UIMethods.FindObjectInChild(ActiveObj, "WorkshopBtn");
        UIMethods.AddOrGetComponent<Button>(WorkshopBtnObj).onClick.AddListener(() =>
        {
            AudioManger.Instance.PlaySound(AudioType.buttonSound);
            UIManger.Instance.Push(new WorkshopPanel());
        });
        //进入出海界面
        GameObject ToSeaBtnObj = UIMethods.FindObjectInChild(ActiveObj, "ToSeaBtn");
        UIMethods.AddOrGetComponent<Button>(ToSeaBtnObj).onClick.AddListener(() =>
        {
            AudioManger.Instance.PlaySound(AudioType.buttonSound);
            if (GameManger.Instance.CurGameData.GetBoatLevel() == 0 || GameManger.Instance.CurGameData.GetFishingRodLevel() == 0)
            {
                UIManger.Instance.ShowTips("请先到商店购买船和网!", 2f);
            }
            else
            {
                LoadingPanel loadingPanel = new LoadingPanel();
                UIManger.Instance.Push(loadingPanel);
                LoadSceneManager.Instance.LoadSceneAsync(2, loadingPanel.UpdateLoadingBar, null, true);
            }
                
        });
        //点击音量调节按钮事件注册
        GameObject volumeBtnObj = UIMethods.FindObjectInChild(ActiveObj, "VolumeBtn");
        UIMethods.AddOrGetComponent<Button>(volumeBtnObj).onClick.AddListener(() =>
        {
            UIManger.Instance.Push(new VolumePanel());
        });

    }
    public override void OnEnable()
    {
        base.OnEnable();
        UpdateGold();
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    private void UpdateGold()
    {
        GameObject GoldNumObj = UIMethods.FindObjectInChild(ActiveObj, "GoldNum");
        UIMethods.AddOrGetComponent<Text>(GoldNumObj).text = GameManger.Instance.CurGameData.Gold.ToString();
    }
}