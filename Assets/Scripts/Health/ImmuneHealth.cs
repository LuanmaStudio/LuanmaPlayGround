using CharacterEditorPackage.Code;
using UnityEngine.Events;

/// <summary>
/// 有无敌时间的生命类型
/// </summary>
public class ImmuneHealth : HealthBase
{
    public int ImmuneTime = 500;
    public UnityEvent<HitInstance> OnImmuneHit;

    private CoolDownTime collDown;

    protected override void Start()
    {
        base.Start();
        collDown = new CoolDownTime(ImmuneTime);
    }

    public override void TakeDamage(HitInstance hit)
    {
        if (collDown.isReady)
        {
            _ = collDown.Start();
            base.TakeDamage(hit);
        }
        else
        {
            OnImmuneHit.Invoke(hit);
        }
    }
    
}
