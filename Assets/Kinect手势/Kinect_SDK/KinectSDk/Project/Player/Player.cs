using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{

    public event Action<Player,KinectGestures.Gestures> OnGestureCompeletedEvent;

    public delegate void HandleDispose();
    public event HandleDispose DisposeEvent;

    public long userId { get; set; }

     public void Dispose()
    {
        if (DisposeEvent != null) DisposeEvent();
        OnGestureCompeletedEvent = null;
    }

    public void CheckPoseComplete(KinectGestures.Gestures gesture)
    {
        if (OnGestureCompeletedEvent != null) OnGestureCompeletedEvent(this,gesture);
    }

}
