/*******************************************************************
* Description:可控制的碰撞体
* Version: 1.0.0
* Date: 2021/12/13
* Author: Ender
*******************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 可控制的碰撞体组件 被控制器使用来移动这个物体
/// </summary>
[RequireComponent(typeof(Rigidbody2D),typeof(Collider2D))]
public class ControlCollider2D : MonoBehaviour
{
    [Tooltip("地面摩擦系数")]
    public float GroundFriction = 0.2f;
    [Tooltip("空气摩擦系数")]
    public float AirFriction = 0.2f;
    [Tooltip("最大线速度")]
    public float maxLateralSpeed;
    [Tooltip("最大下降速度")]
    public float maxFallSpeed;
    [Tooltip("最大上升速度")]
    public float maxRiseSpeed;

    public Collider2D collider;
    public Rigidbody2D rigidbody;
    [HideInInspector]
    public GroundDetection GroundDetection { get; private set; }

    private bool isInput = false;//是否正在输入 如果正在输入 不施加阻力
    private bool jump = false; //是否需要跳跃 如果需要在一次FixedUpdate中执行
    private float jumpHight; //跳跃的速度

    void Start()
    {
        collider = GetComponent<Collider2D>(); 
        rigidbody = GetComponent<Rigidbody2D>();
        GroundDetection = GetComponent<GroundDetection>();
    }
    /// <summary>
    /// 发射刚体
    /// </summary>
    public void Launch(Vector2 direction)
    {
        rigidbody.velocity = direction;
    }
    /// <summary>
    /// 跳跃 供控制器调用
    /// </summary>
    public void Jump(float hight)
    {
        jump = true;
        jumpHight = hight;
    }

    public void FixedUpdate()
    {
        //这里的执行顺序很重要
        GroundDetection.RayCastGround(collider.bounds, GroundDetection.contactFilter);
        ApllyJump();
        if(!isInput) ApplyFirtion();
        LimitLateralVelocity();
        LimitVerticalVelocity();
    }

    /// <summary>
    /// 移动 供控制器调用
    /// </summary>
    public void Move(Vector2 dirserVolocity)
    {
        rigidbody.velocity += dirserVolocity;
        if(dirserVolocity.magnitude > 0.5f) isInput = true;
        else isInput = false;
    }
    /// <summary>
    /// 应用跳跃
    /// </summary>
    private void ApllyJump()
    {
        if(jump)
        {
            jump = false;
            //跳跃时如果在下落 则直接清空下落速度再施加 如果在上升则直接加上
            if(rigidbody.velocity.y<0)
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpHight);
            else rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y + jumpHight);
        }
    }
    /// <summary>
    /// 应用摩擦力
    /// </summary>
    private void ApplyFirtion()
    {
        if (GroundDetection.IsOnGround)
            rigidbody.velocity -= new Vector2(rigidbody.velocity.x, 0) * GroundFriction;
        else
            rigidbody.velocity -= new Vector2(rigidbody.velocity.x,0)* AirFriction;
    }
    /// <summary>
    /// 限制线速度
    /// </summary>
    private void LimitLateralVelocity()
    {
        var speed = rigidbody.velocity;
        rigidbody.velocity = new Vector2(Mathf.Clamp(speed.x,-maxLateralSpeed,maxLateralSpeed), speed.y);
    }
    /// <summary>
    /// 限制垂直速度
    /// </summary>
    private void LimitVerticalVelocity()
    {
        if (GroundDetection.IsOnGround)
            return;
        var speed = rigidbody.velocity;
        rigidbody.velocity = new Vector2(speed.x, Mathf.Clamp(speed.y, -maxFallSpeed, maxRiseSpeed));
    }
}
