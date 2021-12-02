

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI管理器,负责管理每个UI元素
/// </summary>
public class UIManager
{
    private Dictionary<UIInfo, GameObject> UIdir = new Dictionary<UIInfo, GameObject>();

    
    /// <summary>
    /// 获取/创建一个UI对象
    /// </summary>
    /// <param name="info">UI信息</param>
    /// <returns>获取到的UI对象</returns>
    public GameObject GetSingleUI(UIInfo info)
    {
        GameObject parent = GameObject.Find("Canvas");
        if (!parent)
        {
            Debug.LogError("Canvas 不存在");
            return null;
        }

        if (UIdir.ContainsKey(info)) return UIdir[info];

        GameObject ui = GameObject.Instantiate(Resources.Load<GameObject>(info.Path), parent.transform);
        ui.name = info.Name;

        return ui;
    }
    
    /// <summary>
    /// 销毁一个UI对象
    /// </summary>
    /// <param name="info"></param>
    public void DestoryUI(UIInfo info)
    {
        if (UIdir.ContainsKey(info))
        {
            //TODO: 删除UI对象
            UIdir.Remove(info);
        }
    }


}
