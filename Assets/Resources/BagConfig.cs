/****************************************************
    文件：BagConfig.cs
	作者：Azure
	功能：物品表格配置
*****************************************************/

using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Fish,
    Tool,
    Resourece,
    Garrbage
}
[CreateAssetMenu(fileName = "BagConfig", menuName = "Create BagConfig")]
public class BagConfig : ScriptableObject 
{
    public List<Item> FishItems = new List<Item>();
    public List<Item> ToolItems = new List<Item>();
    public List<Item> ResoureceItems = new List<Item>();
    public List<Item> GarbageItems = new List<Item>();
}
[System.Serializable]
public class Item
{
    public ItemType itemType;
    public int itemID;
    public string itemName;
    public string itemDescription;
    public string imagePath;
    public int value;
}