/****************************************************
    文件：MidOceanBorder.cs
	作者：Azure
	功能：中海区域边界
*****************************************************/

using UnityEngine;

public class MidOceanBorder : MonoBehaviour 
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO:船等级判定
        Debug.Log("已进入远洋");
    }
}