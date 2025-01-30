/****************************************************
    文件：MainSceneRoot.cs
	作者：Azure
	功能：主场景入口
*****************************************************/

using Fungus;
using UnityEngine;

public class MainSceneRoot : MonoBehaviour 
{
    private void Awake()
    {
        UIManger.Instance.Init();
        GameManger.Instance.SetFlowchartObjInScene();//获取场景中的对话Flowchart
        GameManger.Instance.CurFlowchat.SetBooleanVariable("Opening", GameManger.Instance.CurGameData.OpeningDialog);
        if (GameManger.Instance.CurGameData.OpeningDialog == false) UIManger.Instance.Push(new MainPanel());
    }
    private void Update()
    {

    }
}