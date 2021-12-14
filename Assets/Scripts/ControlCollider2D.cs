/*******************************************************************
* Description:�ɿ��Ƶ���ײ��
* Version: 1.0.0
* Date: 2021/12/13
* Author: Ender
*******************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �ɿ��Ƶ���ײ����� ��������ʹ�����ƶ��������
/// </summary>
[RequireComponent(typeof(Rigidbody2D),typeof(Collider2D))]
public class ControlCollider2D : MonoBehaviour
{
    [Tooltip("����Ħ��ϵ��")]
    public float GroundFriction = 0.2f;
    [Tooltip("����Ħ��ϵ��")]
    public float AirFriction = 0.2f;
    [Tooltip("������ٶ�")]
    public float maxLateralSpeed;
    [Tooltip("����½��ٶ�")]
    public float maxFallSpeed;
    [Tooltip("��������ٶ�")]
    public float maxRiseSpeed;

    public Collider2D collider;
    public Rigidbody2D rigidbody;
    [HideInInspector]
    public GroundDetection GroundDetection { get; private set; }

    private bool isInput = false;//�Ƿ��������� ����������� ��ʩ������
    private bool jump = false; //�Ƿ���Ҫ��Ծ �����Ҫ��һ��FixedUpdate��ִ��
    private float jumpHight; //��Ծ���ٶ�

    void Start()
    {
        collider = GetComponent<Collider2D>(); 
        rigidbody = GetComponent<Rigidbody2D>();
        GroundDetection = GetComponent<GroundDetection>();
    }
    /// <summary>
    /// �������
    /// </summary>
    public void Launch(Vector2 direction)
    {
        rigidbody.velocity = direction;
    }
    /// <summary>
    /// ��Ծ ������������
    /// </summary>
    public void Jump(float hight)
    {
        jump = true;
        jumpHight = hight;
    }

    public void FixedUpdate()
    {
        //�����ִ��˳�����Ҫ
        GroundDetection.RayCastGround(collider.bounds, GroundDetection.contactFilter);
        ApllyJump();
        if(!isInput) ApplyFirtion();
        LimitLateralVelocity();
        LimitVerticalVelocity();
    }

    /// <summary>
    /// �ƶ� ������������
    /// </summary>
    public void Move(Vector2 dirserVolocity)
    {
        rigidbody.velocity += dirserVolocity;
        if(dirserVolocity.magnitude > 0.5f) isInput = true;
        else isInput = false;
    }
    /// <summary>
    /// Ӧ����Ծ
    /// </summary>
    private void ApllyJump()
    {
        if(jump)
        {
            jump = false;
            //��Ծʱ��������� ��ֱ����������ٶ���ʩ�� �����������ֱ�Ӽ���
            if(rigidbody.velocity.y<0)
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpHight);
            else rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y + jumpHight);
        }
    }
    /// <summary>
    /// Ӧ��Ħ����
    /// </summary>
    private void ApplyFirtion()
    {
        if (GroundDetection.IsOnGround)
            rigidbody.velocity -= new Vector2(rigidbody.velocity.x, 0) * GroundFriction;
        else
            rigidbody.velocity -= new Vector2(rigidbody.velocity.x,0)* AirFriction;
    }
    /// <summary>
    /// �������ٶ�
    /// </summary>
    private void LimitLateralVelocity()
    {
        var speed = rigidbody.velocity;
        rigidbody.velocity = new Vector2(Mathf.Clamp(speed.x,-maxLateralSpeed,maxLateralSpeed), speed.y);
    }
    /// <summary>
    /// ���ƴ�ֱ�ٶ�
    /// </summary>
    private void LimitVerticalVelocity()
    {
        if (GroundDetection.IsOnGround)
            return;
        var speed = rigidbody.velocity;
        rigidbody.velocity = new Vector2(speed.x, Mathf.Clamp(speed.y, -maxFallSpeed, maxRiseSpeed));
    }
}
