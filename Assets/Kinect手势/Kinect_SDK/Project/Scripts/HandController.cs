using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour, KinectGestures.GestureListenerInterface
{
    public bool GestureCancelled(long userId, int userIndex, KinectGestures.Gestures gesture, KinectInterop.JointType joint)
    {
        throw new NotImplementedException();
    }
   //检测用户的手势
    public bool GestureCompleted(long userId, int userIndex, KinectGestures.Gestures gesture, KinectInterop.JointType joint, Vector3 screenPos)
    {
        throw new NotImplementedException();
    }

    public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture, float progress, KinectInterop.JointType joint, Vector3 screenPos)
    {
        throw new NotImplementedException();
    }

    //是否检测到用户
    public void UserDetected(long userId, int userIndex)
    {
        throw new NotImplementedException();
    }
    //用户是否离开了摄像头
    public void UserLost(long userId, int userIndex)
    {
        throw new NotImplementedException();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
