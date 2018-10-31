using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandCacth : GestureBase
{
    public override KinectGestures.Gestures gesture
    {
        get
        {
            return KinectGestures.Gestures.RightHandCatch;
        }
    }
    private KinectInterop.HandState oldState;
    public override void CheckForGesture(long userId, ref KinectGestures.GestureData gestureData, float timestamp, ref Vector3[] jointsPos, ref bool[] jointsTracked)
    {
        KinectInterop.HandState state = KinectManager.Instance.GetRightHandState(userId);
        switch (state)
        {
            case KinectInterop.HandState.Closed:
                if (oldState == KinectInterop.HandState.Open)
                    kinectGestures.CheckPoseComplete(ref gestureData, timestamp, Vector3.zero, true, KinectInterop.Constants.PoseCompleteDuration);
                break;

            case KinectInterop.HandState.Open:
                break;
            case KinectInterop.HandState.NotTracked:

            case KinectInterop.HandState.Lasso:

                kinectGestures.CheckPoseComplete(ref gestureData, timestamp, Vector3.zero, false, KinectInterop.Constants.PoseCompleteDuration);

                break;
        }
        oldState = state;
        base.CheckForGesture(userId, ref gestureData, timestamp, ref jointsPos, ref jointsTracked);
    }
}
