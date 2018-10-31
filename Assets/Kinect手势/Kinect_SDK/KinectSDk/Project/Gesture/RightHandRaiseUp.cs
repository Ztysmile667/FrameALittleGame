using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandRaiseUp : GestureBase
{
    public override KinectGestures.Gestures gesture
    {
        get
        {
            return KinectGestures.Gestures.RaiseRightHand;
        }
    }
    public override void CheckForGesture(long userId, ref KinectGestures.GestureData gestureData, float timestamp, ref Vector3[] jointsPos, ref bool[] jointsTracked)
    {

        switch (gestureData.state)
        {
            case 0:  // gesture detection
                if (jointsTracked[kinectGestures.rightHandIndex] && jointsTracked[kinectGestures.leftHandIndex] && jointsTracked[kinectGestures.leftShoulderIndex] &&
                    (jointsPos[kinectGestures.rightHandIndex].y - jointsPos[kinectGestures.leftShoulderIndex].y) > 0.1f &&
                       (jointsPos[kinectGestures.leftHandIndex].y - jointsPos[kinectGestures.leftShoulderIndex].y) < 0f)
                {
                   kinectGestures.SetGestureJoint(ref gestureData, timestamp, kinectGestures.rightHandIndex, jointsPos[kinectGestures.rightHandIndex]);
                }
                break;

            case 1:  // gesture complete
                bool isInPose = jointsTracked[kinectGestures.rightHandIndex] && jointsTracked[kinectGestures.leftHandIndex] && jointsTracked[kinectGestures.leftShoulderIndex] &&
                    (jointsPos[kinectGestures.rightHandIndex].y - jointsPos[kinectGestures.leftShoulderIndex].y) > 0.1f &&
                    (jointsPos[kinectGestures.leftHandIndex].y - jointsPos[kinectGestures.leftShoulderIndex].y) < 0f;

                Vector3 jointPos = jointsPos[gestureData.joint];
                kinectGestures.CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, KinectInterop.Constants.PoseCompleteDuration);
                break;
        }
       
    }


}
