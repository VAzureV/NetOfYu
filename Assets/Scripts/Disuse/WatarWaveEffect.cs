/****************************************************
    文件：WatarWaveEffect.cs
	作者：Azure
	功能：Nothing
*****************************************************/

using UnityEngine;

public class WaterWaveEffect : MonoBehaviour
{
    public float waveSpeed = 1.0f;
    public float waveStrength = 0.1f;

    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * waveSpeed) * waveStrength;
        material.mainTextureOffset = new Vector2(offset, 0);
    }
}
