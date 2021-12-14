/*******************************************************************
* Description:玩家控制器
* Version: 1.0.0
* Date: 2021/12/13
* Author: Ender
*******************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
/// <summary>
/// 玩家控制器
/// </summary>
public class PlayerControler : MonoBehaviour
{
    [Tooltip("起跳的速度")]
    public float StartJumpForce = 2;
    [Tooltip("持续按住跳跃的速度")]
    public float DeltaJumpForce = 2;
    [Tooltip("跳跃缓存时间")]
    public float JumpToleranceTime = 0.1f;
    [Tooltip("跳跃加速过程阶段")]
    [SerializeField] private int jumpDealta = 5;
    [Tooltip("跳跃加速过程持续时间")]
    [SerializeField] private float jumpDealtaTime = 0.1f;

    private PlayerInput input;
    private ControlCollider2D controlCollider;
    private GroundDetection groundDetection;
    private InputAction moveInput;
    private InputAction jumpInput;

    private float lastPressJumpTime;
    private bool isJump = false;

    void Start()
    {
        input = GetComponent<PlayerInput>();
        groundDetection = GetComponent<GroundDetection>();
        controlCollider = GetComponent<ControlCollider2D>();
        
        jumpInput = input.actions["Jump"];
        moveInput = input.actions["Move"];
        jumpInput.started += StartJump;
        groundDetection.OnEnterGround.AddListener(CheckCacheJump);
    }

    /// <summary>
    /// 开始按下跳跃按钮
    /// </summary>
    private void StartJump(InputAction.CallbackContext obj)
    {
        if (groundDetection.IsOnGround)//如果在地面上就直接执行跳跃
        {
            Jump();
        }
        else
        {
            lastPressJumpTime = Time.time;//否则就记录时间
        }
    }
    private void FixedUpdate()
    {
        Vector2 inputdir =  moveInput.ReadValue<Vector2>();
        inputdir.y = 0;
        controlCollider.Move(inputdir);
    }
    /// <summary>
    /// 计算一直按住跳跃时的delta数值
    /// </summary>
    private float CaculateBoostForce(int i)
    {
        var value = DeltaJumpForce * Mathf.Cos((i*1f / jumpDealta) * Mathf.PI / 2);
        return value;
    }
    /// <summary>
    /// 判断有没有持续按住跳跃 以持续施加力
    /// </summary>
    IEnumerator JumpDealta()
    {
        for (int i = 0;i<jumpDealta;i++)
        {
            yield return new WaitForSeconds(jumpDealtaTime / jumpDealta);//先等代 非常关键 否则会覆盖掉起步
            if (isJump && jumpInput.ReadValue<float>() > 0)
            {
                controlCollider.Jump(CaculateBoostForce(i));
            }
            else
            {
                isJump = false;
                break;
            }
        }
        isJump = false;
    }
    /// <summary>
    /// 在落地后执行 判断有没有缓存了跳跃
    /// </summary>
    void CheckCacheJump()
    {
        if(Time.time-lastPressJumpTime < JumpToleranceTime)
        {
            Jump();
        }
    }
    /// <summary>
    /// 执行跳跃
    /// </summary>
    void Jump()
    {
        isJump = true;
        controlCollider.Jump(StartJumpForce);//立即起跳
        StartCoroutine(JumpDealta());//持续记录是否一直按住跳跃
    }

}
