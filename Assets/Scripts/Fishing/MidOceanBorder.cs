/****************************************************
    文件：MidOceanBorder.cs
	作者：Azure
	功能：中海区域边界
*****************************************************/

using UnityEngine;

public class MidOceanBorder : MonoBehaviour 
{
    private bool inMidOcean = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (inMidOcean)
        {
            UIManger.Instance.ShowTips("-----已进入远洋-----", 2f);
            //等级不够限制速度
            if (GameManger.Instance.CurGameData.GetBoatLevel() <= 2)
            {
                collision.GetComponent<BoatController>().maxSpeed = 1f;
            }
        }
        else
        {
            UIManger.Instance.ShowTips("-----已进入中海-----", 2f);
            if (GameManger.Instance.CurGameData.GetBoatLevel() == 1)
            {
                collision.GetComponent<BoatController>().maxSpeed = 2f;
            }
            else if (GameManger.Instance.CurGameData.GetBoatLevel() == 2)
            {
                collision.GetComponent<BoatController>().maxSpeed = 4f;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (inMidOcean) inMidOcean = false;
    }
}