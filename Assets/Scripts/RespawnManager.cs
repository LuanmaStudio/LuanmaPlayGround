using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 重生管理器
/// </summary>
public class RespawnManager : MonoSingleton<RespawnManager>
{
    private List<Respawner> Respawners = new List<Respawner>();

    public void Awake()
    {
        Instance = this;
    }
    /// <summary>
    /// 注册函数
    /// </summary>
    public void Add(Respawner respawn)
    {
        Respawners.Add(respawn);
    }
    /// <summary>
    /// 移除函数
    /// </summary>
    public void Remove(Respawner respawn)
    {
        Respawners.Remove(respawn);
    }
    /// <summary>
    /// 全部重生
    /// </summary>
    public void RespawnAll()
    {
        foreach (var item in Respawners)
        {
            item.Respawn();
        }
    }
}
