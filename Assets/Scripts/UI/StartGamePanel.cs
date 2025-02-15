/****************************************************
    文件：StartGamePanel.cs
	作者：Azure
	功能：游戏开始界面
*****************************************************/

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartGamePanel : BasePanel
{
    private const string StartGamePanelName = "StartGamePanel";
    private const string StartGamePanelPath = "Prefabs/Panel/StartGamePanel";

    public StartGamePanel() : base(new UIType(StartGamePanelName, StartGamePanelPath)) {}
    public override void OnStart()
    {
        base.OnStart();

        ActiveObj.SetActive(true);

        // 点击任意位置进入游戏事件注册
        GameObject startBglObj = UIMethods.FindObjectInChild(ActiveObj, "StartBg");
        EventTrigger eventTrigger = UIMethods.AddOrGetComponent<EventTrigger>(startBglObj);
        // 创建一个新的 Entry 用于点击事件
        EventTrigger.Entry clickEntry = new EventTrigger.Entry();
        clickEntry.eventID = EventTriggerType.PointerClick;  // 设置为点击事件
        // 定义点击事件的方法
        clickEntry.callback.AddListener((eventData) => 
        { 
            AudioManger.Instance.PlaySound(AudioType.startGameSound);
            //载入加载界面并跳转到下一场景
            LoadingPanel loadingPanel = new LoadingPanel();
            UIManger.Instance.Push(loadingPanel);
            LoadSceneManager.Instance.LoadSceneAsync(1, loadingPanel.UpdateLoadingBar, null, true);
        });
        // 将点击事件添加到 EventTrigger
        eventTrigger.triggers.Add(clickEntry);

        // 音量界面按钮
        GameObject volumeBtnObj = UIMethods.FindObjectInChild(ActiveObj, "VolumeBtn");
        UIMethods.AddOrGetComponent<Button>(volumeBtnObj).onClick.AddListener(() =>
        {
            UIManger.Instance.Push(new VolumePanel());
        });

        GameObject resetBtnObj = UIMethods.FindObjectInChild(ActiveObj, "ResetBtn");
        UIMethods.AddOrGetComponent<Button>(resetBtnObj).onClick.AddListener(() =>
        {
            GameManger.Instance.ResetData();
        });

    }

    public override void OnEnable()
    {
        base.OnEnable();
        ActiveObj.SetActive(true);
    }
    public override void OnDisable()
    {
        base.OnDisable();
        //ActiveObj.SetActive(false);
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        //GameObject.Destroy(ActiveObj);
    }

    
}