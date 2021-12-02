using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于保存UI面板的信息
/// </summary>
public class UIInfo
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// 路径
    /// </summary>
    public string Path { get; private set; }

    public UIInfo(string path)
    {
        Path = path;
        Name = path.Substring(path.LastIndexOf('/') + 1);
    }
}
