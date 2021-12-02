using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
/// <summary>
/// 普通血条
/// </summary>
public class NormalHPBar : MonoBehaviour
{
    public Transform target;

    private HealthBase _healthBase;
    private Slider _slider;
    /// <summary>
    /// 创建一个血条并返回
    /// </summary>
    public static NormalHPBar Create(HealthBase bindHp)
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        GameObject obj = Resources.Load<GameObject>("HPBar");
        GameObject go = Instantiate(obj, canvas);

        var bar = go.GetComponent<NormalHPBar>();
        bar._healthBase = bindHp;
        return bar;
    }
    void Start()
    {
        _slider = GetComponent<Slider>();
        target = _healthBase.transform;
    }
    
    

    
    void Update()
    {
        if (_healthBase == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 barPos = target.position + new Vector3(0,target.GetComponent<Collider>().bounds.size.y,0);
        
        Vector3 trgetPosition = Camera.main.WorldToScreenPoint(barPos);

        transform.position = Vector3.Lerp(transform.position, trgetPosition, Time.deltaTime*20);
        

        _slider.value = 1f *_healthBase.HP / _healthBase.MaxHp;
    }
}
