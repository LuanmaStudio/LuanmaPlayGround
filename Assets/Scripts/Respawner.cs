
using System;
using UnityEngine;
/// <summary>
/// 重生组件
/// </summary>
public class Respawner : MonoBehaviour
{
    /// <summary>
    /// 重生点
    /// </summary>
    private Vector3 respawnPoint;
    

    private void Start()
    {
        RespawnManager.Instance.Add(this);
        respawnPoint = transform.position;
    }

    private void OnDestroy()
    {
        RespawnManager.Instance.Add(this);
    }

    public virtual void Respawn()
    {
        transform.position = respawnPoint;
        gameObject.SetActive(true);
        if (TryGetComponent(out HealthBase health))
        {
            health.Healing(health.MaxHp);
        }
    }
}
