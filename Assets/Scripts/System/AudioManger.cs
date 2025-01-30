/****************************************************
    文件：AudioManger.cs
	作者：Azure
	功能：音频管理器
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AudioType
{
    Bgm1,

    buttonSound,
    startGameSound,
}
public class AudioManger : MonoSingleton<AudioManger> 
{
    //各个声道的AudioSource组件
    private AudioSource bgmAudioSource;
    //各个声道的游戏对象
    private GameObject bgmController;
    private GameObject soundController;
    //存放音频的字典
    private Dictionary<AudioType, AudioClip> audioDict;

    public float SoundVolume;

    public void Init()
    {
        audioDict = new Dictionary<AudioType, AudioClip>();
        LoadAllAudioClip();
        
        //创建并设置背景音乐的控制器
        bgmController = CreateController("BgmController", transform);
        bgmAudioSource = bgmController.AddComponent<AudioSource>();
        bgmAudioSource.playOnAwake = false;
        bgmAudioSource.loop = true;

        //创建音效控制器
        soundController = CreateController("SoundController", transform);

        SoundVolume = 1;

        Debug.Log("_____Init AudioManger Success___");
    }
    GameObject CreateController(string name, Transform parent)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(parent);
        return go;
    }
    private void LoadAllAudioClip()
    {
        AudioClip bgm1AC = ResourceManager.Instance.Load<AudioClip>("Audio/BGM/bgm-1");
        audioDict.Add(AudioType.Bgm1, bgm1AC);
        AudioClip btnSoundAC = ResourceManager.Instance.Load<AudioClip>("Audio/Sound/ButtonSound");
        audioDict.Add(AudioType.buttonSound, btnSoundAC);
        AudioClip startGameSoundAC = ResourceManager.Instance.Load<AudioClip>("Audio/Sound/StartGameSound");
        audioDict.Add(AudioType.startGameSound, startGameSoundAC);
    }

    #region BGM
    /// <summary>
    /// 播放BGM
    /// </summary>
    public void PlayBGM(AudioType audioType, bool loop = true)
    {
        if (!audioDict.ContainsKey(audioType))
        {
            Debug.LogWarning("播放BGM失败！要播放的BGM不存在");
            return;
        }
        
        bgmAudioSource.loop = loop;
        bgmAudioSource.clip = audioDict[audioType];
        bgmAudioSource.Play();
    }

    /// <summary>
    /// 暂停BGM
    /// </summary>
    public void PauseBGM()
    {
        bgmAudioSource.Pause();
    }

    /// <summary>
    /// 取消暂停BGM
    /// </summary>
    public void UnPauseBGM()
    {
        bgmAudioSource.UnPause();
    }

    /// <summary>
    /// 停止BGM
    /// </summary>
    public void StopBGM()
    {
        bgmAudioSource.Stop();
        bgmAudioSource.clip = null;
    }

    public void SetBgmVolume(float volume)
    {
        bgmAudioSource.volume = volume;
    }
    #endregion
    public void PlaySound(AudioType audioType)
    {
        if (!audioDict.ContainsKey(audioType))
        {
            Debug.LogWarning("播放Sound失败！要播放的Sound不存在");
            return;
        }

        //临时的空物体，用来播放音效。
        GameObject go = new GameObject("SoundClip");
        go.transform.SetParent(soundController.transform);

        //如果该游戏对象身上没有AudioSource组件，则添加AudioSource组件并设置参数。
        if (!go.TryGetComponent<AudioSource>(out AudioSource audioSource))
        {
            audioSource = go.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }

        //设置要播放的音效
        audioSource.clip = audioDict[audioType];
        
        //设置音量
        audioSource.volume = SoundVolume;
        
        //播放音效
        audioSource.Play();

        //每隔1秒检测一次，如果该音效播放完毕，则销毁音效的游戏对象。
        StartCoroutine(DestroyWhenFinished());

        //每隔1秒检测一次，如果该音效播放完毕，则销毁音效的游戏对象。
        IEnumerator DestroyWhenFinished()
        {
            do
            {
                yield return new WaitForSeconds(1);

                if (go == null || audioSource == null) yield break;//如果播放音频的游戏对象，或者AudioSource组件被销毁了，则直接跳出协程。
            } while (audioSource != null && audioSource.time > 0);

            if (go != null)
            {
                audioSource.clip = null;
                Destroy(go);
            }

        }
    }
}