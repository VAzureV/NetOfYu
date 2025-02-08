/****************************************************
    文件：BagPanel.cs
	作者：Azure
	功能：背包界面
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class BagPanel : BasePanel 
{
    private const string BagPanellName = "BagPanel";
    private const string BagPanelPath = "Prefabs/Panel/BagPanel";
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
        //背包切换事件注册
        GameObject fishBtnObj = UIMethods.FindObjectInChild(ActiveObj, "FishImg");
        Image fishBtnImg = fishBtnObj.GetComponent<Image>(); 
        GameObject toolBtnObj = UIMethods.FindObjectInChild(ActiveObj, "ToolImg");
        Image toolBtnImg = toolBtnObj.GetComponent<Image>();
        GameObject resourceBtnObj = UIMethods.FindObjectInChild(ActiveObj, "ResourceImg");
        Image resourceBtnImg = resourceBtnObj.GetComponent<Image>();
        GameObject garbageBtnObj = UIMethods.FindObjectInChild(ActiveObj, "GarrbageImg");
        Image garrbageBtnImg = garbageBtnObj.GetComponent<Image>();
        UIMethods.AddOrGetComponent<Button>(fishBtnObj).onClick.AddListener(() =>
        {
            fishBtnImg.color = new Color(Color.white.r, Color.white.g, Color.white.b, 1);
            toolBtnImg.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.6f);
            resourceBtnImg.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.6f);
            garrbageBtnImg.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.6f);
            ReFreshBagPanel(ItemType.Fish);
        });
        UIMethods.AddOrGetComponent<Button>(toolBtnObj).onClick.AddListener(() =>
        {
            toolBtnImg.color = new Color(Color.white.r, Color.white.g, Color.white.b, 1);
            fishBtnImg.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.6f);
            resourceBtnImg.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.6f);
            garrbageBtnImg.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.6f);
            ReFreshBagPanel(ItemType.Tool);
        });
        UIMethods.AddOrGetComponent<Button>(resourceBtnObj).onClick.AddListener(() =>
        {
            resourceBtnImg.color = new Color(Color.white.r, Color.white.g, Color.white.b, 1);
            fishBtnImg.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.6f);
            toolBtnImg.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.6f);
            garrbageBtnImg.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.6f);
            ReFreshBagPanel(ItemType.Resourece);
        });
        UIMethods.AddOrGetComponent<Button>(garbageBtnObj).onClick.AddListener(() =>
        {
            garrbageBtnImg.color = new Color(Color.white.r, Color.white.g, Color.white.b, 1);
            fishBtnImg.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.6f);
            toolBtnImg.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.6f);
            resourceBtnImg.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.6f);
            ReFreshBagPanel(ItemType.Garrbage);
        });
    }
    public override void OnEnable()
    {
        base.OnEnable();
        GameObject fishBtnObj = UIMethods.FindObjectInChild(ActiveObj, "FishImg");
        UIMethods.AddOrGetComponent<Button>(fishBtnObj).onClick.Invoke();
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
        GameObject inventory = UIMethods.FindObjectInChild(ActiveObj, "Inventory");
        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Inventory/Item");
        //刷新背包
        for (int i = 0; i < inventory.transform.childCount; i++)
        {
            Transform transform = inventory.transform.GetChild(i);
            GameObject.Destroy(transform.gameObject);
        }
        switch (itemType)
        {
            case ItemType.Fish:
                for (int i = 0; i < GameData.FishNum; i++)
                {
                    if (GameManger.Instance.CurGameData.GetFishNum(i) <= 0)
                    {
                        continue;
                    }
                    GameObject newItem = GameObject.Instantiate(itemPrefab, inventory.transform);
                    GameObject numObj = newItem.transform.GetChild(0).gameObject;
                    GameObject nameObj = newItem.transform.GetChild(1).gameObject;
                    numObj.GetComponent<Text>().text = GameManger.Instance.CurGameData.GetFishNum(i).ToString();
                    nameObj.GetComponent<Text>().text = GameManger.Instance.CurBagConfig.FishItems[i].itemName;
                    newItem.GetComponent<Image>().sprite = Resources.Load<Sprite>(GameManger.Instance.CurBagConfig.FishItems[i].imagePath);
                }
                break;
            case ItemType.Tool:
                for (int i = 0; i < GameData.ToolNum; i++)
                {
                    if (GameManger.Instance.CurGameData.GetToolNum(i) <= 0)
                    {
                        continue;
                    }
                    GameObject newItem = GameObject.Instantiate(itemPrefab, inventory.transform);
                    GameObject numObj = newItem.transform.GetChild(0).gameObject;
                    GameObject nameObj = newItem.transform.GetChild(1).gameObject;
                    numObj.GetComponent<Text>().text = GameManger.Instance.CurGameData.GetToolNum(i).ToString();
                    nameObj.GetComponent<Text>().text = GameManger.Instance.CurBagConfig.ToolItems[i].itemName;
                    newItem.GetComponent<Image>().sprite = Resources.Load<Sprite>(GameManger.Instance.CurBagConfig.ToolItems[i].imagePath);
                }
                break;
            case ItemType.Resourece:
                for (int i = 0; i < GameData.ResourceNum; i++)
                {
                    if (GameManger.Instance.CurGameData.GetResourceNum(i) <= 0)
                    {
                        continue;
                    }
                    GameObject newItem = GameObject.Instantiate(itemPrefab, inventory.transform);
                    GameObject numObj = newItem.transform.GetChild(0).gameObject;
                    GameObject nameObj = newItem.transform.GetChild(1).gameObject;
                    numObj.GetComponent<Text>().text = GameManger.Instance.CurGameData.GetResourceNum(i).ToString();
                    nameObj.GetComponent<Text>().text = GameManger.Instance.CurBagConfig.ResoureceItems[i].itemName;
                    newItem.GetComponent<Image>().sprite = Resources.Load<Sprite>(GameManger.Instance.CurBagConfig.ResoureceItems[i].imagePath);
                }
                break;
            case ItemType.Garrbage:
                for (int i = 0; i < GameData.GarbageNum; i++)
                {
                    if (GameManger.Instance.CurGameData.GetGarbageNum(i) <= 0)
                    {
                        continue;
                    }
                    GameObject newItem = GameObject.Instantiate(itemPrefab, inventory.transform);
                    GameObject numObj = newItem.transform.GetChild(0).gameObject;
                    GameObject nameObj = newItem.transform.GetChild(1).gameObject;
                    numObj.GetComponent<Text>().text = GameManger.Instance.CurGameData.GetGarbageNum(i).ToString();
                    nameObj.GetComponent<Text>().text = GameManger.Instance.CurBagConfig.GarbageItems[i].itemName;
                    newItem.GetComponent<Image>().sprite = Resources.Load<Sprite>(GameManger.Instance.CurBagConfig.GarbageItems[i].imagePath);
                }
                break;
        }
    }

}