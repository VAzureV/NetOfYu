/****************************************************
    文件：ShopPanel.cs
	作者：Azure
	功能：商店界面
*****************************************************/

using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : BasePanel
{
    private const string ShopPanelName = "ShopPanel";
    private const string ShopPanelPath = "Prefabs/Panel/ShopPanel";
    private int curBoatLevel;//当前船只等级
    private int curRodLevel;//当前渔网等级
    public ShopPanel() : base(new UIType(ShopPanelName, ShopPanelPath)) { }

    public override void OnStart()
    {
        base.OnStart();
        //读取金币数据
        UpdateGold();
        //更新商店界面
        curBoatLevel = GameManger.Instance.CurGameData.GetBoatLevel();
        curRodLevel = GameManger.Instance.CurGameData.GetFishingRodLevel();
        GameObject boatImgObj = UIMethods.FindObjectInChild(ActiveObj, "BoatImg");
        GameObject rodImgObj = UIMethods.FindObjectInChild(ActiveObj, "FishNetImg");
        UpdateShop(boatImgObj, rodImgObj);
        //商品购买事件注册
        UIMethods.FindObjectInChild(boatImgObj, "BuyBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            int boatID = 0;
            switch (curBoatLevel)
            {
                case 0: boatID = 0; break;
                case 1: boatID = 1; break;
                case 2: boatID = 2; break;
            }
            if (curBoatLevel < 3)
            {
                if (GameManger.Instance.CurGameData.Gold >= GameManger.Instance.CurBagConfig.ToolItems[boatID].value)
                {
                    //AudioManger.Instance.PlaySound(AudioType.);
                    GameManger.Instance.CurGameData.Gold -= GameManger.Instance.CurBagConfig.ToolItems[boatID].value;
                    GameManger.Instance.CurGameData.AddToolNum(boatID);
                    curBoatLevel = GameManger.Instance.CurGameData.GetBoatLevel();
                    UIManger.Instance.ShowTips("购买成功！");
                    UpdateGold();
                    UpdateShop(boatImgObj, rodImgObj);
                }
                else
                {
                    UIManger.Instance.ShowTips("金币不足！");
                }
            }
            else
            {
                UIManger.Instance.ShowTips("已经是最高级船只");
            }
        });
        UIMethods.FindObjectInChild(rodImgObj, "BuyBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            int rodID = 3;
            switch (curRodLevel)
            {
                case 0: rodID = 3; break;
                case 1: rodID = 4; break;
                case 2: rodID = 5; break;
            }
            if (curRodLevel < 3)
            {
                if (GameManger.Instance.CurGameData.Gold >= GameManger.Instance.CurBagConfig.ToolItems[rodID].value)
                {
                    GameManger.Instance.CurGameData.Gold -= GameManger.Instance.CurBagConfig.ToolItems[rodID].value;
                    GameManger.Instance.CurGameData.AddToolNum(rodID);
                    curRodLevel = GameManger.Instance.CurGameData.GetFishingRodLevel();
                    UIManger.Instance.ShowTips("购买成功！");
                    UpdateGold();
                    UpdateShop(boatImgObj, rodImgObj);
                }
                else
                {
                    UIManger.Instance.ShowTips("金币不足！");
                }
            }
            else
            {
                UIManger.Instance.ShowTips("已经是最高级渔网");
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
        UpdateGold();
        ActiveObj.SetActive(true);
    }
    private void UpdateGold()
    {
        GameObject GoldNumObj = UIMethods.FindObjectInChild(ActiveObj, "GoldNum");
        UIMethods.AddOrGetComponent<Text>(GoldNumObj).text = GameManger.Instance.CurGameData.Gold.ToString();
    }
    private void UpdateShop(GameObject boatImgObj, GameObject rodImgObj)
    {
        // 商品船更新
        if (curBoatLevel == 2)
        {
            UpdateItem(boatImgObj, 2);
        }
        else if (curBoatLevel == 1)
        {
            UpdateItem(boatImgObj, 1);
        }
        else if (curBoatLevel == 0)
        {
            UpdateItem(boatImgObj, 0);
        }
        else
        {
            boatImgObj.GetComponent<Image>().color = new Color32(70, 70, 70, 255);
            GameObject priceObj = UIMethods.FindObjectInChild(boatImgObj, "PriceTxt");
            priceObj.GetComponent<Text>().text = "";
            GameObject nameObj = UIMethods.FindObjectInChild(boatImgObj, "Name");
            nameObj.GetComponent<Text>().text = "";
        }
        // 商品网更新
        if (curRodLevel == 2)
        {
            UpdateItem(rodImgObj, 5);
        }
        else if (curRodLevel == 1)
        {
            UpdateItem(rodImgObj, 4);
        }
        else if (curRodLevel == 0)
        {
            UpdateItem(rodImgObj, 3);
        }
        else
        {
            rodImgObj.GetComponent<Image>().color = new Color32(70, 70, 70, 255);
            GameObject priceObj = UIMethods.FindObjectInChild(rodImgObj, "PriceTxt");
            priceObj.GetComponent<Text>().text = "";
            GameObject nameObj = UIMethods.FindObjectInChild(rodImgObj, "Name");
            nameObj.GetComponent<Text>().text = "";
        }

    }


    private void UpdateItem(GameObject gameObject, int itemID)
    {
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(GameManger.Instance.CurBagConfig.ToolItems[itemID].imagePath);
        GameObject priceObj = UIMethods.FindObjectInChild(gameObject, "PriceTxt");
        priceObj.GetComponent<Text>().text = GameManger.Instance.CurBagConfig.ToolItems[itemID].value.ToString() + "金币";
        GameObject nameObj = UIMethods.FindObjectInChild(gameObject, "Name");
        nameObj.GetComponent<Text>().text = GameManger.Instance.CurBagConfig.ToolItems[itemID].itemName;
    }
}