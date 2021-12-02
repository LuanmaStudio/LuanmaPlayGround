
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
