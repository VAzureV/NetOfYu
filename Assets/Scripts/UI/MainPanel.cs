/****************************************************
    文件：MainPanel.cs
	作者：Azure
	功能：码头主界面
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class MainPanel : BasePanel 
{
    private const string MainPanellName = "MainPanel";
    private const string MainPanelPath = "Prefabs/MainPanel";
    public MainPanel() : base(new UIType(MainPanellName, MainPanelPath)) { }

    public override void OnStart()
    {
        base.OnStart();

        //读取金币数据
        GameObject GoldNumObj = UIMethods.FindObjectInChild(ActiveObj, "GoldNum");
        UIMethods.AddOrGetComponent<Text>(GoldNumObj).text = GameManger.Instance.CurGameData.Gold.ToString();
        //写入数据
        GameManger.Instance.CurGameData.OpeningDialog = GameManger.Instance.CurFlowchat.GetBooleanVariable("Opening");
        
        //进入背包界面
        GameObject BagBtnObj = UIMethods.FindObjectInChild(ActiveObj, "BagBtn");
        UIMethods.AddOrGetComponent<Button>(BagBtnObj).onClick.AddListener(() =>
        {
            UIManger.Instance.Push(new BagPanel());
        });
        //进入商店界面
        GameObject ShopBtnObj = UIMethods.FindObjectInChild(ActiveObj, "ShopBtn");
        UIMethods.AddOrGetComponent<Button>(ShopBtnObj).onClick.AddListener(() =>
        {
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
            UIManger.Instance.Push(new WorkshopPanel());
        });
        //进入出海界面
        GameObject ToSeaBtnObj = UIMethods.FindObjectInChild(ActiveObj, "ToSeaBtn");
        UIMethods.AddOrGetComponent<Button>(ToSeaBtnObj).onClick.AddListener(() =>
        {
            LoadingPanel loadingPanel = new LoadingPanel();
            UIManger.Instance.Push(loadingPanel);
            LoadSceneManager.Instance.LoadSceneAsync(2, loadingPanel.UpdateLoadingBar, null, true);
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