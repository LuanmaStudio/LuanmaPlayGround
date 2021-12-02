
using UnityEngine;
/// <summary>
/// UI工具类
/// </summary>
public class UITool
{
    private GameObject activePanel;

    public UITool(GameObject panle)
    {
        activePanel = panle;
    }
    
    /// <summary>
    /// 获取/添加 当前活动面板的一个组件
    /// </summary>
    /// <typeparam name="T">组件</typeparam>
    /// <returns></returns>
    public T GetOrAddComponent<T>() where T : Component
    {
        return GetOrAddComponent<T>(activePanel);
    }
    public T GetOrAddComponent<T>(GameObject panel) where T : Component
    {
        T component;

        if (panel.TryGetComponent<T>(out component))
        {
            return component;
        }
        else
        {
            return activePanel.AddComponent<T>();
        }
    }
    
    /// <summary>
    /// 根据名称查找一个子物体
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject FindChildGameObject(string name)
    {
        Transform[] trans = activePanel.GetComponents<Transform>();
        foreach (var item in trans)
        {
            if (item.name == name)
            {
                return item.gameObject;
            }
        }
        
        Debug.LogWarning($"{activePanel.name}里找不到{name}的子对象");
        return null;
    }
    
    public T GetOrAddComponentInCHildren<T>(string name) where T : Component
    {
        GameObject child = FindChildGameObject(name);
        if (child)
        {
            return GetOrAddComponent<T>(child);
        }
        return null;
    }
}
