/*******************************************************************
* Description:地面检测
* Version: 1.0.0
* Date: 2021/12/13
* Author: Ender
*******************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 落地检测组件
/// </summary>
public class GroundDetection : MonoBehaviour
{
    /// <summary>
    /// 物体是否在地面上
    /// </summary>
    public bool IsOnGround { get; private set; }
    /// <summary>
    /// 物体是否在坡阶上
    /// </summary>
    public bool IsOnStep { get; private set; }
    /// <summary>
    /// 物体是否在边缘
    /// </summary>
    public bool isOnLead { get; private set; }
    /// <summary>
    /// 方向 规定 true为递增 false为递减
    /// </summary>
    public bool StepDirection { get; private set; }
    /// <summary>
    /// 方向 规定 true为右 false为左
    /// </summary>
    public bool LeadDirection { get; private set; }
    [Tooltip("碰撞过滤器")]

    public ContactFilter2D contactFilter;
    /// <summary>
    /// 射线状态
    /// </summary>
    public RayState RayState { get; private set; } = new RayState();
    /// <summary>
    /// 当离开地面时
    /// </summary>
    public UnityEvent OnLeaveGround;
    /// <summary>
    /// 当进入地面时
    /// </summary>
    public UnityEvent OnEnterGround;
    //射线长度
    private const float GROUND_THRESHOLD = 0.1f;
    //碰撞框收缩长度
    private float skinWidth = 0.01f;

    /// <summary>
    /// 用射线扫描检测地面
    /// </summary>
    /// <param name="bounds">碰撞体的bound</param>
    /// <param name="filter2D">碰撞体过滤器</param>
    public void RayCastGround(Bounds bounds,ContactFilter2D filter2D)
    {

        RayState.Clear();

        bounds.Expand(Vector3.one * -skinWidth);

        //脚下的射线检测
        Vector2 origin = new Vector2(Mathf.Lerp(bounds.min.x, bounds.max.x,0), bounds.min.y);
        Physics2D.Raycast(origin, Vector2.down, filter2D, RayState.leftFoot, GROUND_THRESHOLD);
        //DebugRay
        Debug.DrawLine(origin, origin+ Vector2.down*GROUND_THRESHOLD);
        

        origin = new Vector2(Mathf.Lerp(bounds.min.x, bounds.max.x, .5f), bounds.min.y);
        Physics2D.Raycast(origin, Vector2.down, filter2D, RayState.midFoot, GROUND_THRESHOLD);
        //DebugRay
        Debug.DrawLine(origin, origin + Vector2.down * GROUND_THRESHOLD);

        origin = new Vector2(Mathf.Lerp(bounds.min.x, bounds.max.x, 1), bounds.min.y);
        Physics2D.Raycast(origin, Vector2.down, filter2D, RayState.rightFoot, GROUND_THRESHOLD);
        //DebugRay
        Debug.DrawLine(origin, origin + Vector2.down * GROUND_THRESHOLD);

        //左边的射线检测
        origin = new Vector2(bounds.min.x, Mathf.Lerp(bounds.min.y, bounds.max.y, 0));
        Physics2D.Raycast(origin, Vector2.left, filter2D, RayState.leftDown, GROUND_THRESHOLD);
        //DebugRay
        Debug.DrawLine(origin, origin + Vector2.left * GROUND_THRESHOLD);

        origin = new Vector2(bounds.min.x, Mathf.Lerp(bounds.min.y, bounds.max.y, .5f));
        Physics2D.Raycast(origin, Vector2.left, filter2D, RayState.leftMid, GROUND_THRESHOLD);
        //DebugRay
        Debug.DrawLine(origin, origin + Vector2.left * GROUND_THRESHOLD);

        origin = new Vector2(bounds.min.x, Mathf.Lerp(bounds.min.y, bounds.max.y, 1));
        Physics2D.Raycast(origin, Vector2.left, filter2D, RayState.leftUp, GROUND_THRESHOLD);
        //DebugRay
        Debug.DrawLine(origin, origin + Vector2.left * GROUND_THRESHOLD);

        //右边的射线检测
        origin = new Vector2(bounds.max.x, Mathf.Lerp(bounds.min.y, bounds.max.y, 0));
        Physics2D.Raycast(origin, Vector2.right, filter2D, RayState.rightDown, GROUND_THRESHOLD);
        //DebugRay
        Debug.DrawLine(origin, origin + Vector2.right * GROUND_THRESHOLD);

        origin = new Vector2(bounds.max.x, Mathf.Lerp(bounds.min.y, bounds.max.y, .5f));
        Physics2D.Raycast(origin, Vector2.right, filter2D, RayState.rightMid, GROUND_THRESHOLD);
        //DebugRay
        Debug.DrawLine(origin, origin + Vector2.right * GROUND_THRESHOLD);

        origin = new Vector2(bounds.max.x, Mathf.Lerp(bounds.min.y, bounds.max.y, 1));
        Physics2D.Raycast(origin, Vector2.right, filter2D, RayState.rightUp, GROUND_THRESHOLD);
        //DebugRay
        Debug.DrawLine(origin, origin + Vector2.right * GROUND_THRESHOLD);



        Cheack();

        //Debug.Log(this);
    }
    /// <summary>
    /// 根据射线的碰撞状态 设定各种状态
    /// </summary>
    private void Cheack()
    {
        //保存上次落地状态
        bool lastGroundState = IsOnGround;
        //清空各种状态
        IsOnGround = false;
        IsOnStep = false;
        isOnLead = false;
        //判断是否在地上
        if (RayState.midFoot.Count + RayState.leftFoot.Count+RayState.rightFoot.Count>0) IsOnGround = true;

        //判断是否在坡上
        if(RayState.leftFoot.Count>0&&RayState.midFoot.Count==0&&RayState.leftDown.Count>0)
        {
            IsOnStep = true;
            StepDirection = false;
        }
        else if (RayState.rightFoot.Count > 0 && RayState.midFoot.Count == 0 && RayState.rightDown.Count > 0)
        {
            IsOnStep = true;
            StepDirection = true;
        }
        //判断是否在边缘上
        else if(RayState.leftFoot.Count>0&&RayState.rightFoot.Count==0)
        {
            isOnLead = true;
            LeadDirection = false;
        }
        else if(RayState.rightFoot.Count>0&&RayState.leftFoot.Count==0)
        {
            isOnLead = true;
            LeadDirection = true;
        }
        //触发进入地面和退出事件
        if(lastGroundState!=IsOnGround)
        {
            if(IsOnGround) OnEnterGround.Invoke();
            else OnLeaveGround.Invoke();
        }
        

    }

    public override string ToString()
    {
        return $"OnGround:{IsOnGround.FillColor()} OnStep:{IsOnStep.FillColor()} Dir:{StepDirection.ParseDir()} OnLead:{isOnLead.FillColor()} Dir:{LeadDirection.ParseDir()}";
    }
}
