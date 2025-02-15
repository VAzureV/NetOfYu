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
    private const string WorkshopPanelPath = "Prefabs/Panel/WorkshopPanel";
    public WorkshopPanel() : base(new UIType(WorkshopPanelName, WorkshopPanelPath)) { }

    private List<Sprite> images = new List<Sprite>();   // 动态加载的图片数组
    private int currentIndex = 0; // 当前图片索引

    public override void OnStart()
    {
        base.OnStart();

        //读取金币数据
        UpdateGold();
        //展示当前鱼价格
        GameObject content = UIMethods.FindObjectInChild(ActiveObj, "FishShowContent");
        ShowFishPrice(content);
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
            if (fishNum < GameManger.Instance.CurGameData.GetFishNum(currentIndex) ) fishNum++;
            FishNumTxt.text = fishNum.ToString();
        });

        //加工按钮点击
        GameObject WorkObj = UIMethods.FindObjectInChild(ActiveObj, "WorkBtn");
        UIMethods.AddOrGetComponent<Button>(WorkObj).onClick.AddListener(() =>
        {
            if (fishNum != 0 && fishNum <= GameManger.Instance.CurGameData.GetFishNum(currentIndex))
            {
                //GameManger.Instance.CurGameData.PolutionVal++;
                int val = fishNum * GameManger.Instance.CurBagConfig.FishItems[currentIndex].value;
                AudioManger.Instance.PlaySound(AudioType.GoldSound);
                UIManger.Instance.ShowTips($"加工成功，获得{val}金币");
                GameManger.Instance.CurGameData.DelFishNum(currentIndex, fishNum);
                GameManger.Instance.CurGameData.Gold += val;
                fishNum = 0;
                FishNumTxt.text = "0";
                UpdateGold();
            }
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

    private void LoadImagesFromResources()
    {
        // 从 Resources/Images 文件夹加载图片
        for (int i = 0; i < GameData.FishNum; i++)
        {
            images.Add(ResourceManager.Instance.Load<Sprite>(GameManger.Instance.CurBagConfig.FishItems[i].imagePath));
        }
        if (images.Count == 0)
        {
            LogUtility.LogError("No images found in Resources/Images!");
        }
    }
    private void UpdateGold()
    {
        GameObject GoldNumObj = UIMethods.FindObjectInChild(ActiveObj, "GoldNum");
        UIMethods.AddOrGetComponent<Text>(GoldNumObj).text = GameManger.Instance.CurGameData.Gold.ToString();
    }
    private void ShowFishPrice(GameObject content)
    {
        GameObject showItemPrefab = ResourceManager.Instance.Load<GameObject>("Prefabs/Inventory/ShowItem");
        for (int i = 0; i < GameData.FishNum; i++)
        {
            GameObject newItem = GameObject.Instantiate(showItemPrefab, content.transform);
            GameObject fishImgObj = newItem.transform.GetChild(0).gameObject;
            GameObject fishPriceObj = newItem.transform.GetChild(1).gameObject;
            fishPriceObj.transform.GetChild(0).GetComponent<Text>().text = GameManger.Instance.CurBagConfig.FishItems[i].itemName + "$" + GameManger.Instance.CurBagConfig.FishItems[i].value;
            fishImgObj.GetComponent<Image>().sprite = Resources.Load<Sprite>(GameManger.Instance.CurBagConfig.FishItems[i].imagePath);
        }
    }

}