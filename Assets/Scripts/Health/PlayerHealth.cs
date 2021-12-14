/*******************************************************************
* Description:玩家的血量
* Version: 1.0.0
* Date: 2021/12/13
* Author: Ender
*******************************************************************/
using UnityEngine;

public class PlayerHealth : ImmuneHealth
{
    protected override void Start()
    {
        base.Start();
        OnHit.AddListener(Hit);
    }

    public override void Death()
    {
        GetComponent<Respawner>().Respawn();
    }
    
    private void Hit(bool isDead,HitInstance hit)
    {
        
    }
}
