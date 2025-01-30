/****************************************************
    文件：FishingSpot.cs
	作者：Azure
	功能：Nothing
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class FishingSpot : MonoBehaviour 
{
    private GameObject tipsE;
    private string fishType;
    public string FishType { get;set; }

    public float blinkSpeed = 2f; // 闪烁速度
    private float alphaValue = 1f; // 当前透明度
    private bool isFadingOut = true;
    private bool isEnter = false;
    private void Awake()
    {
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
                //TODO:显示玩法UI
                Debug.Log("start QTE current Fish：" + FishType);
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