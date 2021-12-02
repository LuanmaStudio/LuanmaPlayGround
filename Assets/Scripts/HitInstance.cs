using UnityEngine;

/// <summary>
/// 攻击实例
/// </summary>
public struct HitInstance
{
    //攻击来源
    public GameObject Source;
    //攻击类型
    public AttackTypes AttackType;

    public bool CircleDirection;
    //造成的伤害
    public int DamageDealt;
    //无视无敌
    public bool IgnoreInvulnerable;
    //攻击强度 用于计算后坐力
    public float MagnitudeMultiplier;

    public float MoveAngle;

    public bool MoveDirection;
    //攻击增强倍数
    public float Multiplier;
    
    public bool IsExtraDamage;

    public Vector2 GetActualDirection(Transform target)
    {
        return (target.position - Source.transform.position).normalized;
    }
}

public enum AttackTypes
{
    //轻攻击,重击,刺
    Lit,Heavy,Sting
}
