/****************************************************
    文件：WorkshopPanel.cs
	作者：Azure
	功能：加工坊界面
*****************************************************/
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WorkshopPanel : BasePanel 
{
    private const string WorkshopPanelName = "WorkshopPanel";
    private const string WorkshopPanelPath = "Prefabs/WorkshopPanel";
    public WorkshopPanel() : base(new UIType(WorkshopPanelName, WorkshopPanelPath)) { }

    private List<Sprite> images = new List<Sprite>();   // 动态加载的图片数组
    private int currentIndex = 0; // 当前图片索引

    public override void OnStart()
    {
        base.OnStart();

        //鱼种类选择按钮点击
        
        LoadImagesFromResources();
        
        GameObject FishTypeObj = UIMethods.FindObjectInChild(ActiveObj, "FishType");
        Image FishTypeImg = UIMethods.AddOrGetComponent<Image>(FishTypeObj);
        if (images.Count > 0)
        {
            FishTypeImg.sprite = images[currentIndex];
        }
        GameObject LeftArrowObj = UIMethods.FindObjectInChild(FishTypeObj, "LeftArrow");
        GameObject RightArrowObj = UIMethods.FindObjectInChild(FishTypeObj, "RightArrow");
        UIMethods.AddOrGetComponent<Button>(LeftArrowObj).onClick.AddListener(() =>
        {
            // 更新索引到上一张图片
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = images.Count - 1; // 循环到最后一张
            }
            FishTypeImg.sprite = images[currentIndex];
        });
        UIMethods.AddOrGetComponent<Button>(RightArrowObj).onClick.AddListener(() =>
        {
            // 更新索引到下一张图片
            currentIndex++;
            if (currentIndex >= images.Count)
            {
                currentIndex = 0; // 循环到第一张
            }
            FishTypeImg.sprite = images[currentIndex];
        });

        //鱼数量选择按钮点击
        GameObject FishNumObj = UIMethods.FindObjectInChild(ActiveObj, "FishNum");
        Text FishNumTxt = UIMethods.AddOrGetComponent<Text>(FishNumObj);
        int fishNum = 0;
        FishNumTxt.text = fishNum.ToString();
        GameObject LeftArrow1Obj = UIMethods.FindObjectInChild(FishNumObj, "LeftArrow");
        GameObject RightArrow1Obj = UIMethods.FindObjectInChild(FishNumObj, "RightArrow");
        
        UIMethods.AddOrGetComponent<Button>(LeftArrow1Obj).onClick.AddListener(() =>
        {
            if (fishNum > 0) fishNum--;
            FishNumTxt.text = fishNum.ToString();
        });
        UIMethods.AddOrGetComponent<Button>(RightArrow1Obj).onClick.AddListener(() =>
        {
            //if (fishNum < GameManger.Instance.CurGameData.FishNumDict[(FishType)currentIndex]) fishNum++;
            FishNumTxt.text = fishNum.ToString();
        });

        //加工按钮点击
        GameObject WorkObj = UIMethods.FindObjectInChild(ActiveObj, "WorkBtn");
        UIMethods.AddOrGetComponent<Button>(WorkObj).onClick.AddListener(() =>
        {
            GameManger.Instance.CurGameData.PolutionVal++;
            int val = 0;
            switch (currentIndex)
            {
                case 0:
                    val = 10;
                    break;
                case 1:
                    val = 15;
                    break;
                case 2:
                    val = 20;
                    break;
                case 3:
                    val = 25;
                    break;
                case 4:
                    val = 30;
                    break;
            }
            GameManger.Instance.CurGameData.Gold += val * fishNum;
            FishNumTxt.text = "0";
        });

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

    void LoadImagesFromResources()
    {
        // 从 Resources/Images 文件夹加载图片
        //images = Resources.LoadAll<Sprite>("Image/Fish");
        images.Add(ResourceManager.Instance.Load<Sprite>("Image/Fish/小黄鱼"));
        images.Add(ResourceManager.Instance.Load<Sprite>("Image/Fish/石斑鱼"));
        images.Add(ResourceManager.Instance.Load<Sprite>("Image/Fish/银鲳鱼"));
        images.Add(ResourceManager.Instance.Load<Sprite>("Image/Fish/鳗鱼"));
        images.Add(ResourceManager.Instance.Load<Sprite>("Image/Fish/海兔"));
        if (images.Count == 0)
        {
            Debug.LogError("No images found in Resources/Images!");
        }
    }

    
}