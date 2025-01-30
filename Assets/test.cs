/****************************************************
    文件：test.cs
	作者：Azure
	功能：Nothing
*****************************************************/

using UnityEngine;

public class test : MonoBehaviour 
{
    private void Awake()
    {
        GameManger.Instance.Init();
        UIManger.Instance.Init();
        UIManger.Instance.Push(new WorkshopPanel());
    }
}