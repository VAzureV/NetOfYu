/****************************************************
    文件：GarbageTriger.cs
	作者：Azure
	功能：出海垃圾触发
*****************************************************/

using Unity.VisualScripting;
using UnityEngine;

public class GarbageTriger : MonoBehaviour 
{
    [HideInInspector]
    public int garbageID;
    public FishingRoot fishingRoot;
    
    private GameObject tipsE;
    public float blinkSpeed = 2f; // 闪烁速度
    private float alphaValue = 1f; // 当前透明度
    private bool isFadingOut = true;
    private bool isEnter = false;

    private void Start()
    {
        tipsE = gameObject.transform.GetChild(0).gameObject;
        gameObject.GetComponent<Animator>().SetInteger("ID", garbageID);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boat"))
        {
            tipsE.SetActive(true);
            isEnter = true;
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
    private void Update()
    {
        if (isEnter)
        {
            Blink();
            if (Input.GetKeyDown(KeyCode.E))
            {
                AudioManger.Instance.PlaySound(AudioType.GarbageSound);
                //更新垃圾数量
                fishingRoot.AddGarbageNum(garbageID);
                GameManger.Instance.CurGameData.AddGarbageNum(garbageID);
                //销毁垃圾
                Destroy(gameObject);
            }
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