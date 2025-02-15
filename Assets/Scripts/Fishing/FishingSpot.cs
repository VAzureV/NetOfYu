/****************************************************
    文件：FishingSpot.cs
	作者：Azure
	功能：捕鱼点
*****************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingSpot : MonoBehaviour 
{
    private GameObject tipsE;

    public float blinkSpeed = 2f; // 闪烁速度
    private float alphaValue = 1f; // 当前透明度
    private bool isFadingOut = true;
    private bool isEnter = false;
    private bool isInit = false;
    // 捕鱼点配置
    public OceanZoneType BelongOcean { get; set; }
    public int FishNum { get; set; }
    public List<int> FishTypes { get; set; }
    // QTE参数
    public float CursorSpeed { get; set; }
    public float successZoneSize { get; set; }
    public float successZonePos { get; set; }
    

    private void Awake()
    {
        FishTypes = new List<int>();
        Transform[] transforms = GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < transforms.Length; i++)
        {
            if (transforms[i].gameObject.name == "TipsE")
            {
                tipsE = transforms[i].gameObject;
                break;
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boat"))
        {
            tipsE.SetActive(true);
            isEnter = true;
        }
    }
    private void Update()
    {
        if (isEnter)
        {
            Blink();
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isInit)
                {
                    OceanZoneConfig oceanZoneConfig = null;
                    //根据所属海域初始化QTE配置
                    switch (BelongOcean)
                    {
                        case OceanZoneType.Shallow:
                            // 浅滩
                            oceanZoneConfig = FishingSpotManger.Instance.shallowConfig;
                            break;
                        case OceanZoneType.MidOcean:
                            // 中海
                            oceanZoneConfig = FishingSpotManger.Instance.midOceanConfig;
                            break;
                        case OceanZoneType.OpenSea:
                            // 远洋
                            oceanZoneConfig = FishingSpotManger.Instance.openSeaConfig;
                            break;
                    }
                    CursorSpeed = Random.Range(oceanZoneConfig.qteParams.cursorSpeedRange.x, oceanZoneConfig.qteParams.cursorSpeedRange.y);
                    successZoneSize = Random.Range(oceanZoneConfig.qteParams.zoneSizeRange.x, oceanZoneConfig.qteParams.zoneSizeRange.y);
                    successZonePos = Random.Range(oceanZoneConfig.qteParams.zonePosRange.x, oceanZoneConfig.qteParams.zonePosRange.y);
                    FishNum = Random.Range((int)oceanZoneConfig.qteParams.FishNum.x, (int)oceanZoneConfig.qteParams.FishNum.y);
                    for (int i = 0; i < FishNum; i++)
                    {
                        FishTypes.Add(Random.Range((int)oceanZoneConfig.FishTypes.x, (int)oceanZoneConfig.FishTypes.y));
                    }
                    isInit = true;
                }
                
                //显示玩法UI
                UIManger.Instance.Push(new FishingPanel(this));
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Boat"))
        {
            tipsE.SetActive(false);
            isEnter = false;
        }
    }

    private void Blink()
    {
        // 控制透明度变化
        if (isFadingOut)
            alphaValue -= blinkSpeed * Time.deltaTime;
        else
            alphaValue += blinkSpeed * Time.deltaTime;

        // 限制透明度范围在 0 到 1
        alphaValue = Mathf.Clamp01(alphaValue);

        // 反转方向
        if (alphaValue <= 0)
            isFadingOut = false;
        else if (alphaValue >= 1)
            isFadingOut = true;

        // 更新图片的透明度
        SpriteRenderer targetSprite = tipsE.GetComponent<SpriteRenderer>();
        Color color = targetSprite.color;
        color.a = alphaValue;
        targetSprite.color = color;
    }

}