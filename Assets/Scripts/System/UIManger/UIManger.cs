/****************************************************
    文件：UIManger.cs
	作者：Azure
	功能：利用字典保存界面，并且通过栈维护界面的层级关系
*****************************************************/

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManger : Singleton<UIManger>
{
	private Dictionary<string, GameObject> panleObjDict;
	private Stack<BasePanel> panelStack;
	
    private GameObject tipsPanelObj;
    private GameObject tipsObj;
    public BasePanel CurPanel {  get; private set; }
    public GameObject CanvasObj;

    public void Init()
	{
		panleObjDict = new Dictionary<string, GameObject>();
		panelStack = new Stack<BasePanel>();
        CanvasObj = UIMethods.FindCanvas();
        tipsPanelObj = GameObject.Instantiate<GameObject>(ResourceManager.Instance.Load<GameObject>("Prefabs/Panel/TipsPanel"), CanvasObj.transform);
        tipsObj = UIMethods.FindObjectInChild(tipsPanelObj, "Tips");
        LogUtility.Log("____Init UIManger Success____");
	}
    public void UpdateCanvas()
    {
        CanvasObj = UIMethods.FindCanvas();
        tipsPanelObj = GameObject.Instantiate<GameObject>(ResourceManager.Instance.Load<GameObject>("Prefabs/Panel/TipsPanel"), CanvasObj.transform);
        tipsObj = UIMethods.FindObjectInChild(tipsPanelObj, "Tips");
    }
    /// <summary>
    /// ag:startpanel将其UIType传入basepanel,basepanel和UIManager交互的时候将其UIType参数传入到此
    /// </summary>
    /// <param name="basePanel_push">使用Push的panel的UIType</param>
    public void Push(BasePanel panel)
    {
        if (panelStack.Count > 0)
        {
            if (panelStack.Peek().UIType.Name == panel.UIType.Name) return;//防止多次点击导致同一界面多次进栈
            panelStack.Peek().OnDisable();
        }
        CurPanel = panel;
        GameObject panelObj = GetSingleObject(panel.UIType);
        panel.ActiveObj = panelObj;
        if (!panleObjDict.ContainsKey(panel.UIType.Name))
        {
            panel.OnStart();
            panleObjDict.Add(panel.UIType.Name, panelObj);
        }
        panelStack.Push(panel);
        panel.OnEnable();
    }
    /// <summary>
    /// 弹出
    /// </summary>
    public void Pop()
    {
        if (panelStack.Count > 0)
        {
            panelStack.Peek().OnDisable();
            panelStack.Pop();

            if (panelStack.Count > 0)
            {
                panelStack.Peek().OnEnable();
                CurPanel = panelStack.Peek();
            }
        }
    }
    /// <summary>
    /// 弹出全部元素
    /// </summary>
    public void PopAll()
    {
        while (panelStack.Count > 0)
        {
            panelStack.Pop();
        }
    }
    /// <summary>
    /// 销毁所有panel对象，清空栈和字典
    /// </summary>
    public void DestroyAll()
    {
        PopAll();
        foreach (var obj in panleObjDict.Values)
        {
            if (obj != null) GameObject.Destroy(obj);
        }
        panleObjDict.Clear();
    }
    public void DestroyPanel(string panelName)
    {
        if (panleObjDict.ContainsKey(panelName))
        {
            GameObject.Destroy(panleObjDict[panelName]);
            panleObjDict.Remove(panelName);
        }
    }
    public void ShowTips(string tips, float time = 1f)
    {
        tipsPanelObj.transform.SetAsLastSibling();
        tipsObj.GetComponent<UnityEngine.UI.Text>().text = tips;
        tipsObj.SetActive(true);
        MonoManager.Instance.StartCoroutine(TipsHide(time));
    }
    private System.Collections.IEnumerator TipsHide(float time)
    {
        yield return new WaitForSeconds(time);
        tipsObj.SetActive(false);
    }
    private GameObject GetSingleObject(UIType uiInfo)
    {
        if (panleObjDict.ContainsKey(uiInfo.Name) && panleObjDict[uiInfo.Name] != null)
        {
            return panleObjDict[uiInfo.Name];
        }

        if (CanvasObj == null)
        {
            LogUtility.Log("____load Canvas____");
            CanvasObj = UIMethods.FindCanvas();
        }

        if (!panleObjDict.ContainsKey(uiInfo.Name))
        {
            if (CanvasObj == null)
            {
                return null;
            }
            else
            {
                //从本地加载一个物体并在场景中实例化
                GameObject uiObj = GameObject.Instantiate<GameObject>(ResourceManager.Instance.Load<GameObject>(uiInfo.Path), CanvasObj.transform);
                return uiObj;
            }
        }
        return null;
    }

}