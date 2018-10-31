using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandSwipeUp : GestureBase
{
    public override KinectGestures.Gestures gesture
    {
        get
        {
            return KinectGestures.Gestures.SwipeUp;
        }
    }
}
