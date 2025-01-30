/****************************************************
    文件：BasePanel.cs
	作者：Azure
	功能：UI界面基类
*****************************************************/

using UnityEngine;

public class BasePanel
{
	public UIType UIType {get;set;}
	public GameObject ActiveObj {get;set;}
	/// <summary>
	/// UI基类构造函数
	/// </summary>
	/// <param name="uIType">当前界面类型信息</param>
	public BasePanel(UIType uIType)
    {
        UIType = uIType;
    }
	public virtual void OnStart()
	{
        Debug.Log($"_____{UIType.Name}开始使用_____");
        if (ActiveObj.GetComponent<CanvasGroup>() == null)
        {
            ActiveObj.AddComponent<CanvasGroup>();
        }
        ActiveObj.SetActive(true);
        UIMethods.AddOrGetComponent<CanvasGroup>(ActiveObj).interactable = true;
    }
	public virtual void OnEnable() 
	{
        ActiveObj.SetActive(true);
        UIMethods.AddOrGetComponent<CanvasGroup>(ActiveObj).interactable = true;
    }
	public virtual void OnDisable() 
	{
        UIMethods.AddOrGetComponent<CanvasGroup>(ActiveObj).interactable = false;
    }
	public virtual void OnDestroy()
    {
        UIMethods.AddOrGetComponent<CanvasGroup>(ActiveObj).interactable = false;
    }
}