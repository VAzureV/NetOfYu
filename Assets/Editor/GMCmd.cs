/****************************************************
    文件：GMCmd.cs
	作者：Azure
	功能：调试命令
*****************************************************/

using UnityEngine;
using UnityEditor;
public class GMCmd : EditorWindow
{
    private int itemIndex = 0;
    private int itemAmount = 1;

    // 菜单项，用于打开自定义的 EditorWindow
    [MenuItem("GMCmd/添加物品")]
    private static void OpenWindow()
    {
        GMCmd window = GetWindow<GMCmd>();
        window.titleContent = new GUIContent("添加物品");
        window.Show();
    }

    // 绘制 EditorWindow 的界面
    private void OnGUI()
    {
        // 显示一个标签和输入框，让用户输入要添加物品的索引
        itemIndex = EditorGUILayout.IntField("物品索引", itemIndex);
        // 显示一个标签和输入框，让用户输入要添加物品的数量
        itemAmount = EditorGUILayout.IntField("物品数量", itemAmount);

        // 显示一个按钮，点击后执行添加物品的操作
        if (GUILayout.Button("添加物品"))
        {
            // 调用 GameManger 实例的 CurGameData 的 AddFishNum 方法，并传递用户输入的索引和数量
            GameManger.Instance.CurGameData.AddFishNum(itemIndex, itemAmount);
            // 调用 GameManger 实例的 Save 方法保存数据
            GameManger.Instance.Save();
            // 关闭 EditorWindow
            Close();
        }
    }
    [MenuItem("GMCmd/读取配置数据")]
    public static void LoadConfigData()
    {
        BagConfig bagConfig = ResourceManager.Instance.Load<BagConfig>("BagConfig");
        foreach (var item in bagConfig.FishItems)
        {
            Debug.Log("FishItems: " + item.itemName);
        }
        
    }
    //[MenuItem("GMCmd/添加物品")]
    //public static void AddItem(int index)
    //{
    //    GameManger.Instance.CurGameData.AddFishNum(index);
    //    GameManger.Instance.Save();
    //}
}