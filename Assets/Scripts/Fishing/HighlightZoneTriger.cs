/****************************************************
    文件：HighlightZoneTriger.cs
	作者：Azure
	功能：成功区域触发事件管理
*****************************************************/

using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class HighlightZoneTriger : MonoBehaviour 
{
    private bool isSuccessful = false;
    private void Awake()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isSuccessful = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isSuccessful = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        isSuccessful = true;

    }
    public bool IsSuccessful()
    {
        return isSuccessful;
    }
}