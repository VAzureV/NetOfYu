/****************************************************
    文件：GameManger.cs
	作者：Azure
	功能：管理整个游戏进程
*****************************************************/

using Fungus;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameManger : MonoSingleton<GameManger> 
{
    public GameData CurGameData { get; set; }
    public Flowchart CurFlowchat { get; set; }
    public BagConfig CurBagConfig { get; set; }
    void OnApplicationQuit()
    {
        Debug.Log("应用程序正在退出...");
        // 在这里编写退出时需要执行的代码
        Save();
    }

    public void Init()
	{
        Read();//读取数据
        if (CurGameData == null)//第一次进入游戏
        {
            CurGameData = new GameData();
            InitData();
            Save();
        }
        CurBagConfig = ResourceManager.Instance.Load<BagConfig>("Config/BagConfig");//读取配置数据
    }
    public void SetFlowchartObjInScene()
    {
        CurFlowchat = GameObject.FindGameObjectWithTag("Flowchart").GetComponent<Flowchart>();
    }
    /// <summary>
    /// 保存数据
    /// </summary>
    public void Save()
	{
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.Create(Application.persistentDataPath + "/GameData.data"))
            {
                bf.Serialize(fs, CurGameData);
            }

        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    /// <summary>
    /// 读取
    /// </summary>
    private void Read()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.Open(Application.persistentDataPath + "/GameData.data", FileMode.Open))
            {
                CurGameData = (GameData)bf.Deserialize(fs);
            }
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }
    }
    /// <summary>
    /// 重置数据
    /// </summary>
    public void ResetData()
    {
        InitData();
        Save();
    }
    private void InitData()
    {
        CurGameData.BgmVolume = 0.5f;
        CurGameData.SoundVolume = 0.5f;
        CurGameData.Gold = 0;
        CurGameData.PolutionVal = 0;
        CurGameData.OpeningDialog = true;
    }
}