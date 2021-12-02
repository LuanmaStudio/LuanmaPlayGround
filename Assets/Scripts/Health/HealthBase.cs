using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthBase : MonoBehaviour
{
    /// <summary>
    /// 当收到攻击时,bool?该次攻击死亡:不死
    /// </summary>
    public UnityEvent<bool,HitInstance> OnHit;
    public UnityEvent OnDeath;
    [SerializeField] protected int max_hp;
    [SerializeField] protected int hp;
    /// <summary>
    /// 当前生命值
    /// </summary>
    public int HP
    {
        get=>hp;
        private set
        {
            hp = Mathf.Clamp(value, 0, max_hp);
        }
    }
    /// <summary>
    /// 最大生命值
    /// </summary>
    public int MaxHp
    {
        get => max_hp;
        private set => max_hp =value;
    }

    protected virtual void Start()
    {
        NormalHPBar.Create(this);
        hp = max_hp;
    }


    public virtual void  TakeDamage(HitInstance hit)
    {
        HP -= (int)(hit.DamageDealt * hit.Multiplier);
        if (HP <= 0) OnHit.Invoke(true,hit);
        else OnHit.Invoke(false,hit);
        if(HP<=0) Death();
    }

    public virtual void Healing(int heal)
    {
        HP += heal;
    }

    public virtual void Death()
    {
        Debug.Log(gameObject.name + "Dead");
        OnDeath.Invoke();
        Destroy(gameObject);
    }
    
}
