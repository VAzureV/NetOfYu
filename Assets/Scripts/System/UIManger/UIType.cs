/****************************************************
    文件：UIType.cs
	作者：Azure
	功能：UI界面属性
*****************************************************/

/// <summary>
/// UI界面的属性（名称和路径）
/// </summary>
public class UIType
{
    //自动属性
	public string Name { get;}
	public string Path { get;}
    /// <summary>
    /// UIType构造函数
    /// </summary>
    /// <param name="name">当前UI界面的名称</param>
    /// <param name="path">当前UI界面Prefab的路径</param>
	public UIType(string name, string path)
    {
        Name = name; 
        Path = path;
    }

}