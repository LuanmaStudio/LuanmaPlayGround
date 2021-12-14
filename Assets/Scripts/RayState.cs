using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RayState
{
    public List<RaycastHit2D> leftFoot = new List<RaycastHit2D>();
    public List<RaycastHit2D> rightFoot = new List<RaycastHit2D>();
    public List<RaycastHit2D> midFoot = new List<RaycastHit2D>();

    public List<RaycastHit2D> leftUp = new List<RaycastHit2D>();
    public List<RaycastHit2D> leftMid = new List<RaycastHit2D>();
    public List<RaycastHit2D> leftDown = new List<RaycastHit2D>();

    public List<RaycastHit2D> rightUp = new List<RaycastHit2D>();
    public List<RaycastHit2D> rightMid = new List<RaycastHit2D>();
    public List<RaycastHit2D> rightDown = new List<RaycastHit2D>();

    public void Clear()
    {
        leftFoot.Clear();
        rightFoot.Clear();
        midFoot.Clear();
        leftUp.Clear();
        leftMid.Clear();
        leftDown.Clear();
        rightDown.Clear();
        rightMid.Clear();
        rightUp.Clear();
    }
}
