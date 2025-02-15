/****************************************************
    文件：CountdownTimer.cs
	作者：Azure
	功能：航行倒计时
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour 
{
    public float totalTime = 60.0f;  // 总计时长（秒）
    private Text countdownText;       // 倒计时文本组件
    private Button endBtn;
    private float currentTime;      // 当前剩余时间


    void Start()
    {
        currentTime = totalTime;
        countdownText = this.gameObject.transform.Find("Time").GetComponent<Text>();
        endBtn = this.gameObject.transform.Find("End").GetComponent<Button>();
        endBtn.onClick.AddListener(EndToClearing);
    }   

    void Update()
    {
        if (currentTime > 0)
        {
            // 更新倒计时
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();

            // 倒计时结束时触发
            if (currentTime <= 0)
            {
                currentTime = 0;
                OnTimerEnd();
            }
        }
    }

    // 更新显示文本
    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        countdownText.text = string.Format("可航行时间：{0:00}:{1:00}", minutes, seconds);
    }

    // 倒计时结束逻辑
    void OnTimerEnd()
    {
        Time.timeScale = 0;           // 暂停游戏（可选）
        //endPanel.SetActive(true);     // 显示结束面板
        Debug.Log("倒计时结束！");
    }
    public void EndToClearing()
    {
        // 打开结算面板

        // 关闭航行面板
        this.gameObject.SetActive(false);
    }
    // 提供给"确定"按钮调用的方法（需在按钮事件中绑定）
    public void OnConfirmButtonClick()
    {
        Time.timeScale = 1;           // 恢复游戏时间（如果暂停了）
        //endPanel.SetActive(false);    // 关闭面板
        // 这里可以添加其他逻辑（如重新加载场景）
    }
}