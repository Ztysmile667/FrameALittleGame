using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGesture : GestureBase
{
    public override KinectGestures.Gestures gesture
    {
        get
        {
            return KinectGestures.Gestures.ZombieGesture;
        }
    }
    public override void CheckForGesture(long userId, ref KinectGestures.GestureData gestureData, float timestamp, ref Vector3[] jointsPos, ref bool[] jointsTracked)
    {

        switch (gestureData.state)
        {
            case 0:
                if (jointsTracked[kinectGestures.rightHandIndex] && jointsTracked[kinectGestures.leftHandIndex] &&
                    Mathf.Abs(jointsPos[kinectGestures.leftHandIndex].x - jointsPos[kinectGestures.leftShoulderIndex].x) < 0.15f &&
                     Mathf.Abs(jointsPos[kinectGestures.rightHandIndex].x - jointsPos[kinectGestures.rightShoulderIndex].x) < 0.15f &&
                     jointsPos[kinectGestures.rightHandIndex].y - jointsPos[kinectGestures.rightShoulderIndex].y < -0.05f &&
                       jointsPos[kinectGestures.leftHandIndex].y - jointsPos[kinectGestures.leftShoulderIndex].y < -0.05f
                    )
                {

                    kinectGestures.SetGestureJoint(ref gestureData, timestamp, kinectGestures.rightHandIndex, jointsPos[kinectGestures.rightHandIndex]);
                    gestureData.timestamp = timestamp;
                }
                break;
            case 1:
               
                if (timestamp - gestureData.timestamp < 1f)
                {
                    if (jointsTracked[kinectGestures.rightHandIndex] && jointsTracked[kinectGestures.leftHandIndex] &&
                       Mathf.Abs(jointsPos[kinectGestures.leftHandIndex].x - jointsPos[kinectGestures.leftShoulderIndex].x) < 0.15f &&
                        Mathf.Abs(jointsPos[kinectGestures.rightHandIndex].x - jointsPos[kinectGestures.rightShoulderIndex].x) < 0.15f &&
                         Mathf.Abs(jointsPos[kinectGestures.rightHandIndex].y - jointsPos[kinectGestures.rightShoulderIndex].y) < 0.05f &&
                           Mathf.Abs(jointsPos[kinectGestures.leftHandIndex].y - jointsPos[kinectGestures.leftShoulderIndex].y) < 0.05f
                       )
                    {
                        kinectGestures.CheckPoseComplete(ref gestureData, timestamp, Vector3.zero, true,0);
                    }
                }else
                {
                    kinectGestures.SetGestureCancelled(ref gestureData);
                }
                break;
        }
    }
}
