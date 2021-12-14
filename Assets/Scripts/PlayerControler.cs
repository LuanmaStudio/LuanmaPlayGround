/*******************************************************************
* Description:��ҿ�����
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
/// ��ҿ�����
/// </summary>
public class PlayerControler : MonoBehaviour
{
    [Tooltip("�������ٶ�")]
    public float StartJumpForce = 2;
    [Tooltip("������ס��Ծ���ٶ�")]
    public float DeltaJumpForce = 2;
    [Tooltip("��Ծ����ʱ��")]
    public float JumpToleranceTime = 0.1f;
    [Tooltip("��Ծ���ٹ��̽׶�")]
    [SerializeField] private int jumpDealta = 5;
    [Tooltip("��Ծ���ٹ��̳���ʱ��")]
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
    /// ��ʼ������Ծ��ť
    /// </summary>
    private void StartJump(InputAction.CallbackContext obj)
    {
        if (groundDetection.IsOnGround)//����ڵ����Ͼ�ֱ��ִ����Ծ
        {
            Jump();
        }
        else
        {
            lastPressJumpTime = Time.time;//����ͼ�¼ʱ��
        }
    }
    private void FixedUpdate()
    {
        Vector2 inputdir =  moveInput.ReadValue<Vector2>();
        inputdir.y = 0;
        controlCollider.Move(inputdir);
    }
    /// <summary>
    /// ����һֱ��ס��Ծʱ��delta��ֵ
    /// </summary>
    private float CaculateBoostForce(int i)
    {
        var value = DeltaJumpForce * Mathf.Cos((i*1f / jumpDealta) * Mathf.PI / 2);
        return value;
    }
    /// <summary>
    /// �ж���û�г�����ס��Ծ �Գ���ʩ����
    /// </summary>
    IEnumerator JumpDealta()
    {
        for (int i = 0;i<jumpDealta;i++)
        {
            yield return new WaitForSeconds(jumpDealtaTime / jumpDealta);//�ȵȴ� �ǳ��ؼ� ����Ḳ�ǵ���
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
    /// ����غ�ִ�� �ж���û�л�������Ծ
    /// </summary>
    void CheckCacheJump()
    {
        if(Time.time-lastPressJumpTime < JumpToleranceTime)
        {
            Jump();
        }
    }
    /// <summary>
    /// ִ����Ծ
    /// </summary>
    void Jump()
    {
        isJump = true;
        controlCollider.Jump(StartJumpForce);//��������
        StartCoroutine(JumpDealta());//������¼�Ƿ�һֱ��ס��Ծ
    }

}
