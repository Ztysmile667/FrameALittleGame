using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GestureBase : IGesture
{
    public abstract KinectGestures.Gestures gesture
    {
        get;
    }

    public KinectGestures kinectGestures;

    public float bandTopY;
    public float bandBotY;

    public float bandCenter;
    public float bandSize;
    public float gestureTop;
    public float gestureBottom;
    public float gestureRight;
    public float gestureLeft;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="gestureData"></param>
    /// <param name="timestamp"></param>
    /// <param name="jointsPos"></param>
    /// <param name="jointsTracked"></param>
    public virtual void CheckForGesture(long userId, ref KinectGestures.GestureData gestureData, float timestamp, ref Vector3[] jointsPos, ref bool[] jointsTracked)
    {

    }

}
