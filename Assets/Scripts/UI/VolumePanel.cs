/****************************************************
    文件：VolumePanel.cs
	作者：Azure
	功能：音量调节界面
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class VolumePanel : BasePanel 
{
    private const string VolumePanellName = "VolumePanel";
    private const string VolumePanelPath = "Prefabs/Panel/VolumePanel";
    public VolumePanel() : base(new UIType(VolumePanellName, VolumePanelPath)) { }
    public override void OnStart()
    {
        base.OnStart();

        ActiveObj.SetActive(true);

        //音量控制
        GameObject bgmObj = UIMethods.FindObjectInChild(ActiveObj, "BGMSlider");
        Slider bgmSlider = UIMethods.AddOrGetComponent<Slider>(bgmObj);
        bgmSlider.onValueChanged.AddListener((value) =>
        {
            AudioManger.Instance.SetBgmVolume(value);
        });
        bgmSlider.value = GameManger.Instance.CurGameData.BgmVolume;

        GameObject soundObj = UIMethods.FindObjectInChild(ActiveObj, "SoundSlider");
        Slider soundSlider = UIMethods.AddOrGetComponent<Slider>(soundObj);
        soundSlider.onValueChanged.AddListener((value) =>
        {
            AudioManger.Instance.SoundVolume = value;
        });
        soundSlider.value = GameManger.Instance.CurGameData.SoundVolume;
        
        //返回主界面事件注册
        GameObject exitBtnObj = UIMethods.FindObjectInChild(ActiveObj, "ExitBtn");
        UIMethods.AddOrGetComponent<Button>(exitBtnObj).onClick.AddListener(() =>
        {
            UIManger.Instance.Pop();
            GameManger.Instance.CurGameData.BgmVolume = bgmSlider.value;
            GameManger.Instance.CurGameData.SoundVolume = soundSlider.value;
        });
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