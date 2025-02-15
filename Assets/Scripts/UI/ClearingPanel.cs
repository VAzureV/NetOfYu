/****************************************************
    文件：ClearingPanel.cs
	作者：Azure
	功能：结算界面
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class ClearingPanel : BasePanel 
{
    private const string ClearingPanelName = "ClearingPanel";
    private const string ClearingPanelPath = "Prefabs/Panel/ClearingPanel";
    private FishingRoot fishingRoot;
    public ClearingPanel() : base(new UIType(ClearingPanelName, ClearingPanelPath)) { }

    public override void OnStart()
    {
        base.OnStart();

        GameObject.FindGameObjectWithTag("Boat").GetComponent<BoatController>().canMove = false;
        GameObject.FindGameObjectWithTag("Boat").GetComponent<BoatController>().StopBoat();

        fishingRoot = GameObject.Find("FishingRoot").GetComponent<FishingRoot>();
        GameObject resContent = UIMethods.FindObjectInChild(ActiveObj, "ResContent");
        GameObject FishContent = UIMethods.FindObjectInChild(ActiveObj, "FishContent");
        ShowFishingResult(FishContent);
        ShowGarbageResult(resContent);
        //确定按钮事件注册
        GameObject backBtnObj = UIMethods.FindObjectInChild(ActiveObj, "OKAndReturn");
        UIMethods.AddOrGetComponent<Button>(backBtnObj).onClick.AddListener(() =>
        {
            UIManger.Instance.Pop();
            //载入加载界面并跳转到下一场景
            LoadingPanel loadingPanel = new LoadingPanel();
            UIManger.Instance.Push(loadingPanel);
            LoadSceneManager.Instance.LoadSceneAsync(1, loadingPanel.UpdateLoadingBar, null, true);
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

    private void ShowFishingResult(GameObject content)
    {
        GameObject showItemPrefab = ResourceManager.Instance.Load<GameObject>("Prefabs/Inventory/ClearItem");
        for (int i = 0; i < FishingRoot.FishNum; i++)
        {
            if (fishingRoot.GetFishNum(i) <= 0) continue;
            GameObject newItem = GameObject.Instantiate(showItemPrefab, content.transform);
            GameObject nameObj = newItem.transform.GetChild(0).gameObject;
            GameObject numObj = newItem.transform.GetChild(1).gameObject;
            numObj.GetComponent<Text>().text = "×" + fishingRoot.GetFishNum(i).ToString();
            nameObj.GetComponent<Text>().text = GameManger.Instance.CurBagConfig.FishItems[i].itemName;
            newItem.GetComponent<Image>().sprite = Resources.Load<Sprite>(GameManger.Instance.CurBagConfig.FishItems[i].imagePath);
        }
    }
    private void ShowGarbageResult(GameObject content)
    {
        GameObject showItemPrefab = ResourceManager.Instance.Load<GameObject>("Prefabs/Inventory/ClearItem");
        for (int i = 0; i < FishingRoot.GarbageNum; i++)
        {
            if (fishingRoot.GetGarbageNum(i) <= 0) continue;
            GameObject newItem = GameObject.Instantiate(showItemPrefab, content.transform);
            GameObject nameObj = newItem.transform.GetChild(0).gameObject;
            GameObject numObj = newItem.transform.GetChild(1).gameObject;
            numObj.GetComponent<Text>().text = "×" + fishingRoot.GetGarbageNum(i).ToString();
            nameObj.GetComponent<Text>().text = GameManger.Instance.CurBagConfig.GarbageItems[i].itemName;
            newItem.GetComponent<Image>().sprite = Resources.Load<Sprite>(GameManger.Instance.CurBagConfig.GarbageItems[i].imagePath);
        }
    }


}