/****************************************************
    文件：FishingPanel.cs
	作者：Azure
	功能：捕鱼玩法界面
*****************************************************/

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum QTEGameState
{
    Idle,       // 空闲状态
    Running,    // QTE 进行中
    Success,    // 成功
    Failed      // 失败
}
public class FishingPanel : BasePanel 
{
    private const string FishingPanelName = "FishingPanel";
    private const string FishingPanelPath = "Prefabs/Panel/FishingPanel";
    private FishingSpot curFishingSpot;
    private GameObject fishingbar;
    private GameObject highlightZone;
    private Slider ropeSlider;
    private Coroutine currentCoroutine;

    private QTEGameState qteState = QTEGameState.Idle;
    private float cursorPosition = 0f;       // 游标当前位置
    private bool isMovingRight = true;       // 游标移动方向
    private int successCount = 0;
    private int curSuccessCount = 0;
    private string OceanName;
    public FishingPanel(FishingSpot fishingSpot) : base(new UIType(FishingPanelName, FishingPanelPath))
    {
        curFishingSpot = fishingSpot;
        switch (fishingSpot.BelongOcean)
        {
            case OceanZoneType.Shallow:
                OceanName = "浅滩";
                break;
            case OceanZoneType.MidOcean:
                OceanName = "中海";
                break;
            case OceanZoneType.OpenSea:
                OceanName = "远洋";
                break;
        }
        if (GameManger.Instance.CurGameData.GetFishingRodLevel() == 1)
        {
            successCount = 3;
        }
        else if (GameManger.Instance.CurGameData.GetFishingRodLevel() == 2)
        {
            successCount = 2;
        }
        else if (GameManger.Instance.CurGameData.GetFishingRodLevel() == 3)
        {
            successCount = 1;
        }
        curSuccessCount = successCount;
    }

    public override void OnStart()
    {
        base.OnStart();
        fishingbar = UIMethods.FindObjectInChild(ActiveObj, "Fishingbar");
        highlightZone = UIMethods.FindObjectInChild(ActiveObj, "HighlightZone");
        ropeSlider = UIMethods.FindObjectInChild(ActiveObj, "RopeSlider").GetComponent<Slider>();
        qteState = QTEGameState.Running;
        MonoManager.Instance.AddUpdateListener(UpdateQTEGame);
        MonoManager.Instance.AddUpdateListener(InputCheck);
        UpdateQTEZoneUI(curFishingSpot.successZonePos, curFishingSpot.successZoneSize);
        
        // 关闭按钮
        GameObject exitBtnObj = UIMethods.FindObjectInChild(ActiveObj, "ExitBtn");
        UIMethods.AddOrGetComponent<Button>(exitBtnObj).onClick.AddListener(() =>
        {
            UIManger.Instance.Pop();
        });
        // 钓鱼按钮
        GameObject fishBtnObj = UIMethods.FindObjectInChild(ActiveObj, "PullBtn");
        UIMethods.AddOrGetComponent<Button>(fishBtnObj).onClick.AddListener(() => {
            qteState = QTEGameState.Idle;
            if (highlightZone.GetComponent<HighlightZoneTriger>().IsSuccessful() && curFishingSpot.FishNum != 0)
            {
                curSuccessCount--;
                SetValueSmoothly((float)curSuccessCount / successCount, 0.5f);
                if (curSuccessCount == 0)
                {
                    curSuccessCount = successCount;
                    curFishingSpot.FishNum--;
                    UIMethods.FindObjectInChild(ActiveObj, "FishesNum").GetComponent<Text>().text = $"余量：{curFishingSpot.FishNum}";
                    OnQTESuccess();
                    SetValueSmoothly(1, 0.2f);
                }
            }
            else
            {
                if (curFishingSpot.FishNum == 0)
                {
                    UIManger.Instance.ShowTips("鱼已经被捕完了");
                }
                else
                {
                    OnQTEFailed();
                    curSuccessCount = successCount;
                }
                
            }
        });
        // 信息显示
        UIMethods.FindObjectInChild(ActiveObj, "SeaArea").GetComponent<Text>().text = OceanName;
        UIMethods.FindObjectInChild(ActiveObj, "FishesNum").GetComponent<Text>().text = $"余量：{curFishingSpot.FishNum}";

        

    }
    public override void OnEnable()
    {
        base.OnEnable();
        ActiveObj.SetActive(true);
        
    }
    public override void OnDisable()
    {
        base.OnDisable();
        MonoManager.Instance.RemoveUpdateListener(UpdateQTEGame);
        MonoManager.Instance.RemoveUpdateListener(InputCheck);
        UIManger.Instance.DestroyPanel(FishingPanelName);
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        ActiveObj.SetActive(false);
    }
    private void UpdateQTEGame()
    {
        if (qteState != QTEGameState.Running) return;

        // 更新游标位置
        cursorPosition += isMovingRight ? curFishingSpot.CursorSpeed * Time.deltaTime : -curFishingSpot.CursorSpeed * Time.deltaTime;

        // 边界反弹
        if (cursorPosition >= 1f)
        {
            cursorPosition = 1f;
            isMovingRight = false;
        }
        else if (cursorPosition <= 0f)
        {
            cursorPosition = 0f;
            isMovingRight = true;
        }

        // 更新 UI 显示
        UpdateQTECursorUI(cursorPosition);
        
    }
    
    private void UpdateQTECursorUI(float position)
    {
        if (fishingbar == null)
        {
            Debug.LogError("fishingbar is null");
        }
        fishingbar.GetComponent<Scrollbar>().value = position;
    }

    private void UpdateQTEZoneUI(float pos, float size)
    {
        RectTransform successZone = UIMethods.FindObjectInChild(ActiveObj, "HighlightZone").GetComponent<RectTransform>();
        successZone.anchoredPosition = new Vector2(pos, 0);
        successZone.sizeDelta = new Vector2(size * successZone.sizeDelta.x, successZone.sizeDelta.y);
        BoxCollider2D boxCollider = UIMethods.FindObjectInChild(ActiveObj, "HighlightZone").GetComponent<BoxCollider2D>();
        boxCollider.size = successZone.sizeDelta;

    }
    private void InputCheck()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject fishBtnObj = UIMethods.FindObjectInChild(ActiveObj, "PullBtn");
            UIMethods.AddOrGetComponent<UnityEngine.UI.Button>(fishBtnObj).onClick.Invoke();
        }
    }
    private void OnQTESuccess()
    {
        // 显示成功 UI
        UIManger.Instance.ShowTips("成功捕获 " + GameManger.Instance.CurBagConfig.FishItems[curFishingSpot.FishTypes[curFishingSpot.FishNum]].itemName);
        // 对应数据处理
        //GameManger.Instance.CurGameData.AddFishNum(curFishingSpot.FishTypes[curFishingSpot.FishNum]);
    }

    private void OnQTEFailed()
    {
        // 显示失败 UI
        UIManger.Instance.ShowTips("捕获失败!!!");
    }

    // 公共方法：平滑过渡Slider的值
    private void SetValueSmoothly(float targetValue, float duration)
    {
        if (ropeSlider == null)
        {
            Debug.LogError("Slider未赋值！");
            return;
        }

        // 如果已有动画在进行，先停止
        if (currentCoroutine != null)
        {
            MonoManager.Instance.StopCoroutine(currentCoroutine);
        }

        // 启动新的协程
        currentCoroutine = MonoManager.Instance.StartCoroutine(SmoothValueCoroutine(targetValue, duration));
    }

    // 协程处理过渡
    private IEnumerator SmoothValueCoroutine(float targetValue, float duration)
    {
        float startValue = ropeSlider.value;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            // 计算插值比例（线性）
            float t = timeElapsed / duration;
            // 可选：应用缓动函数（例如SmoothStep）
            // t = Mathf.SmoothStep(0f, 1f, t);
            ropeSlider.value = Mathf.Lerp(startValue, targetValue, t);
            timeElapsed += Time.deltaTime;
            yield return null; // 等待下一帧
        }
        qteState = QTEGameState.Running;
        // 确保最终值准确
        ropeSlider.value = targetValue;
        currentCoroutine = null;
    }

}