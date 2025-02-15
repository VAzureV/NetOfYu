/****************************************************
    文件：ShallowBorder.cs
	作者：Azure
	功能：浅滩区域边界
*****************************************************/

using UnityEngine;

public class ShallowBorder : MonoBehaviour 
{
    private bool inShallow = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (inShallow)
        {
            UIManger.Instance.ShowTips("-----已进入中海-----", 2f);
            //等级不够限制速度
            if (GameManger.Instance.CurGameData.GetBoatLevel() <= 1)
            {
                collision.GetComponent<BoatController>().maxSpeed = 1f;
            }
        }
        else
        {
            UIManger.Instance.ShowTips("-----已进入浅滩-----", 2f);
            if (GameManger.Instance.CurGameData.GetBoatLevel() <= 1)
            {
                collision.GetComponent<BoatController>().maxSpeed = 2f;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (inShallow) inShallow = false;
    }
}