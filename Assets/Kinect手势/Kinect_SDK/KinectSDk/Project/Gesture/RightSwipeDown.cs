using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSwipeDown : GestureBase
{
    public override KinectGestures.Gestures gesture
    {
        get
        {
            return KinectGestures.Gestures.RightSwipeDown;
        }
    }
    public override void CheckForGesture(long userId, ref KinectGestures.GestureData gestureData, float timestamp, ref Vector3[] jointsPos, ref bool[] jointsTracked)
    {

        switch (gestureData.state)
        {
            case 0:
                if (jointsTracked[kinectGestures.rightHandIndex] && jointsTracked[kinectGestures.rightShoulderIndex] &&
                   Mathf.Abs((jointsPos[kinectGestures.rightHandIndex].x - jointsPos[kinectGestures.rightShoulderIndex].x)) < 0.2f)
                {
                    kinectGestures.SetGestureJoint(ref gestureData, timestamp, kinectGestures.rightHandIndex, jointsPos[kinectGestures.rightHandIndex]);
                    gestureData.progress = 0.3f;
                    gestureData.jointPos = jointsPos[kinectGestures.rightHandIndex];
                    gestureData.timestamp = timestamp;
                }
                break;
            case 1:
                if ((timestamp - gestureData.timestamp) < 0.5f)
                {
                    bool isInpos = jointsTracked[kinectGestures.rightHandIndex] && jointsTracked[kinectGestures.rightShoulderIndex] &&
                  Mathf.Abs((jointsPos[kinectGestures.rightHandIndex].x - jointsPos[kinectGestures.rightShoulderIndex].x)) < 0.2f &&
                  gestureData.jointPos.y - jointsPos[kinectGestures.rightHandIndex].y > 0.05f;

                    if (isInpos)
                    {
                        Vector3 pos = jointsPos[kinectGestures.rightHandIndex];
                        kinectGestures.CheckPoseComplete(ref gestureData, timestamp, pos, isInpos, 0f);
                        gestureData.timestamp = timestamp;
                        gestureData.progress = 0.7f;
                    }
                }
                else
                {
                    kinectGestures.SetGestureCancelled(ref gestureData);
                }
                break;
        }
        // base.CheckForGesture(userId, ref gestureData, timestamp, ref jointsPos, ref jointsTracked);
    }
}
