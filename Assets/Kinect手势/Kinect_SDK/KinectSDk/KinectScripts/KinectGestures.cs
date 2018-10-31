using UnityEngine;
//using Windows.Kinect;

using System.Collections;
using System.Collections.Generic;


/// <summary>
/// This interface needs to be implemented by the Kinect gesture managers, like KinectGestures-class itself
/// </summary>
public interface GestureManagerInterface
{
	/// <summary>
	/// Gets the list of gesture joint indexes.
	/// </summary>
	/// <returns>The needed joint indexes.</returns>
	/// <param name="manager">The KinectManager instance</param>
	int[] GetNeededJointIndexes (KinectManager manager);


	/// <summary>
	/// Estimate the state and progress of the given gesture.
	/// </summary>
	/// <param name="userId">User ID</param>
	/// <param name="gestureData">Gesture-data structure</param>
	/// <param name="timestamp">Current time</param>
	/// <param name="jointsPos">Joints-position array</param>
	/// <param name="jointsTracked">Joints-tracked array</param>
	void CheckForGesture (long userId, ref KinectGestures.GestureData gestureData, float timestamp, ref Vector3[] jointsPos, ref bool[] jointsTracked);
}


/// <summary>
/// KinectGestures is utility class that processes programmatic Kinect gestures
/// </summary>
public class KinectGestures : MonoBehaviour, GestureManagerInterface
{

	/// <summary>
	/// This interface needs to be implemented by all Kinect gesture listeners
	/// </summary>
	public interface GestureListenerInterface
	{
		/// <summary>
		/// Invoked when a new user is detected. Here you can start gesture tracking by invoking KinectManager.DetectGesture()-function.
		/// </summary>
		/// <param name="userId">User ID</param>
		/// <param name="userIndex">User index</param>
		void UserDetected(long userId, int userIndex);
		
		/// <summary>
		/// Invoked when a user gets lost. All tracked gestures for this user are cleared automatically.
		/// </summary>
		/// <param name="userId">User ID</param>
		/// <param name="userIndex">User index</param>
		void UserLost(long userId, int userIndex);
		
		/// <summary>
		/// Invoked when a gesture is in progress.
		/// </summary>
		/// <param name="userId">User ID</param>
		/// <param name="userIndex">User index</param>
		/// <param name="gesture">Gesture type</param>
		/// <param name="progress">Gesture progress [0..1]</param>
		/// <param name="joint">Joint type</param>
		/// <param name="screenPos">Normalized viewport position</param>
		void GestureInProgress(long userId, int userIndex, Gestures gesture, float progress, 
		                       KinectInterop.JointType joint, Vector3 screenPos);

		/// <summary>
		/// Invoked if a gesture is completed.
		/// </summary>
		/// <returns><c>true</c>, if the gesture detection must be restarted, <c>false</c> otherwise.</returns>
		/// <param name="userId">User ID</param>
		/// <param name="userIndex">User index</param>
		/// <param name="gesture">Gesture type</param>
		/// <param name="joint">Joint type</param>
		/// <param name="screenPos">Normalized viewport position</param>
		bool GestureCompleted(long userId, int userIndex, Gestures gesture,
		                      KinectInterop.JointType joint, Vector3 screenPos);

		/// <summary>
		/// Invoked if a gesture is cancelled.
		/// </summary>
		/// <returns><c>true</c>, if the gesture detection must be retarted, <c>false</c> otherwise.</returns>
		/// <param name="userId">User ID</param>
		/// <param name="userIndex">User index</param>
		/// <param name="gesture">Gesture type</param>
		/// <param name="joint">Joint type</param>
		bool GestureCancelled(long userId, int userIndex, Gestures gesture, 
		                      KinectInterop.JointType joint);
	}


	/// <summary>
	/// The gesture types.
	/// </summary>
	public enum Gestures
	{
		None = 0,
		RaiseRightHand,
		RaiseLeftHand,
		Psi,
		Tpose,
		Stop,
		Wave,
//		Click,
		SwipeLeft,
		SwipeRight,
		SwipeUp,
		SwipeDown,
        RightSwipeDown,
//		RightHandCursor,
//		LeftHandCursor,
        ZoomIn,
		ZoomOut,
		Wheel,
		Jump,
		Squat,
		Push,
		Pull,
		ShoulderLeftFront,
		ShoulderRightFront,
		LeanLeft,
		LeanRight,
		LeanForward,
		LeanBack,
		KickLeft,
		KickRight,
		Run,

        RaisedRightHorizontalLeftHand,   // by Andrzej W
        RaisedLeftHorizontalRightHand, 
        RightHandCatch,
		UserGesture1 = 101,
		UserGesture2 = 102,
		UserGesture3 = 103,
		UserGesture4 = 104,
		UserGesture5 = 105,
		UserGesture6 = 106,
		UserGesture7 = 107,
		UserGesture8 = 108,
		UserGesture9 = 109,
		UserGesture10 = 110,
        ZombieGesture = 111,
    }
	
	
	/// <summary>
	/// Programmatic gesture data container.
	/// </summary>
	public struct GestureData
	{
		public long userId;
		public Gestures gesture;
		public int state;
		public float timestamp;
		public int joint;
		public Vector3 jointPos;
		public Vector3 screenPos;
		public float tagFloat;
		public Vector3 tagVector;
		public Vector3 tagVector2;
		public float progress;
		public bool complete;
		public bool cancelled;
		public List<Gestures> checkForGestures;
		public float startTrackingAtTime;
	}
	

	// Gesture related constants, variables and functions
    [HideInInspector]
	public int leftHandIndex;
    [HideInInspector]
    public int rightHandIndex;
    [HideInInspector]
    public int leftElbowIndex;
    [HideInInspector]
    public int rightElbowIndex;
    [HideInInspector]
    public int leftShoulderIndex;
    [HideInInspector]
    public int rightShoulderIndex;
    [HideInInspector]
    public int hipCenterIndex;
    [HideInInspector]
    public int shoulderCenterIndex;
    [HideInInspector]
    public int leftHipIndex;
    [HideInInspector]
    public int rightHipIndex;
    [HideInInspector]
    public int leftKneeIndex;
    [HideInInspector]
    public int rightKneeIndex;
    [HideInInspector]
    public int leftAnkleIndex;
    [HideInInspector]
    public int rightAnkleIndex;


	/// <summary>
	/// Gets the list of gesture joint indexes.
	/// </summary>
	/// <returns>The needed joint indexes.</returns>
	/// <param name="manager">The KinectManager instance</param>
	public virtual int[] GetNeededJointIndexes(KinectManager manager)
	{
		leftHandIndex = manager.GetJointIndex(KinectInterop.JointType.HandLeft);
		rightHandIndex = manager.GetJointIndex(KinectInterop.JointType.HandRight);
		
		leftElbowIndex = manager.GetJointIndex(KinectInterop.JointType.ElbowLeft);
		rightElbowIndex = manager.GetJointIndex(KinectInterop.JointType.ElbowRight);
		
		leftShoulderIndex = manager.GetJointIndex(KinectInterop.JointType.ShoulderLeft);
		rightShoulderIndex = manager.GetJointIndex(KinectInterop.JointType.ShoulderRight);
		
		hipCenterIndex = manager.GetJointIndex(KinectInterop.JointType.SpineBase);
		shoulderCenterIndex = manager.GetJointIndex(KinectInterop.JointType.SpineShoulder);

		leftHipIndex = manager.GetJointIndex(KinectInterop.JointType.HipLeft);
		rightHipIndex = manager.GetJointIndex(KinectInterop.JointType.HipRight);

		leftKneeIndex = manager.GetJointIndex(KinectInterop.JointType.KneeLeft);
		rightKneeIndex = manager.GetJointIndex(KinectInterop.JointType.KneeRight);
		
		leftAnkleIndex = manager.GetJointIndex(KinectInterop.JointType.AnkleLeft);
		rightAnkleIndex = manager.GetJointIndex(KinectInterop.JointType.AnkleRight);
		
		int[] neededJointIndexes = {
			leftHandIndex, rightHandIndex, leftElbowIndex, rightElbowIndex, leftShoulderIndex, rightShoulderIndex,
			hipCenterIndex, shoulderCenterIndex, leftHipIndex, rightHipIndex, leftKneeIndex, rightKneeIndex, 
			leftAnkleIndex, rightAnkleIndex
		};

		return neededJointIndexes;
	}
	

	public void SetGestureJoint(ref GestureData gestureData, float timestamp, int joint, Vector3 jointPos)
	{
		gestureData.joint = joint;
		gestureData.jointPos = jointPos;
		gestureData.timestamp = timestamp;
		gestureData.state++;
	}

    public void SetGestureCancelled(ref GestureData gestureData)
	{
		gestureData.state = 0;
		gestureData.progress = 0f;
		gestureData.cancelled = true;
	}

    public void CheckPoseComplete(ref GestureData gestureData, float timestamp, Vector3 jointPos, bool isInPose, float durationToComplete)
	{
		if(isInPose)
		{
			float timeLeft = timestamp - gestureData.timestamp;
			gestureData.progress = durationToComplete > 0f ? Mathf.Clamp01(timeLeft / durationToComplete) : 1.0f;
	
			if(timeLeft >= durationToComplete)
			{
				gestureData.timestamp = timestamp;
				gestureData.jointPos = jointPos;
				gestureData.state++;
				gestureData.complete = true;
			}
		}
		else
		{
			SetGestureCancelled(ref gestureData);
		}
	}
	
	protected void SetScreenPos(long userId, ref GestureData gestureData, ref Vector3[] jointsPos, ref bool[] jointsTracked)
	{
		Vector3 handPos = jointsPos[rightHandIndex];
		bool calculateCoords = false;
		
		if(gestureData.joint == rightHandIndex)
		{
			if(jointsTracked[rightHandIndex] /**&& jointsTracked[rightElbowIndex] && jointsTracked[rightShoulderIndex]*/)
			{
				calculateCoords = true;
			}
		}
		else if(gestureData.joint == leftHandIndex)
		{
			if(jointsTracked[leftHandIndex] /**&& jointsTracked[leftElbowIndex] && jointsTracked[leftShoulderIndex]*/)
			{
				handPos = jointsPos[leftHandIndex];
				calculateCoords = true;
			}
		}
		
		if(calculateCoords)
		{
			if(jointsTracked[hipCenterIndex] && jointsTracked[shoulderCenterIndex] && 
				jointsTracked[leftShoulderIndex] && jointsTracked[rightShoulderIndex])
			{
				Vector3 shoulderToHips = jointsPos[shoulderCenterIndex] - jointsPos[hipCenterIndex];
				Vector3 rightToLeft = jointsPos[rightShoulderIndex] - jointsPos[leftShoulderIndex];
				
				gestureData.tagVector2.x = rightToLeft.x; // * 1.2f;
				gestureData.tagVector2.y = shoulderToHips.y; // * 1.2f;
				
				if(gestureData.joint == rightHandIndex)
				{
					gestureData.tagVector.x = jointsPos[rightShoulderIndex].x - gestureData.tagVector2.x / 2;
					gestureData.tagVector.y = jointsPos[hipCenterIndex].y;
				}
				else
				{
					gestureData.tagVector.x = jointsPos[leftShoulderIndex].x - gestureData.tagVector2.x / 2;
					gestureData.tagVector.y = jointsPos[hipCenterIndex].y;
				}
			}
	
			if(gestureData.tagVector2.x != 0 && gestureData.tagVector2.y != 0)
			{
				Vector3 relHandPos = handPos - gestureData.tagVector;
				gestureData.screenPos.x = Mathf.Clamp01(relHandPos.x / gestureData.tagVector2.x);
				gestureData.screenPos.y = Mathf.Clamp01(relHandPos.y / gestureData.tagVector2.y);
			}
			
		}
	}
	
	protected void SetZoomFactor(long userId, ref GestureData gestureData, float initialZoom, ref Vector3[] jointsPos, ref bool[] jointsTracked)
	{
		Vector3 vectorZooming = jointsPos[rightHandIndex] - jointsPos[leftHandIndex];
		
		if(gestureData.tagFloat == 0f || gestureData.userId != userId)
		{
			gestureData.tagFloat = 0.5f; // this is 100%
		}

		float distZooming = vectorZooming.magnitude;
		gestureData.screenPos.z = initialZoom + (distZooming / gestureData.tagFloat);
	}
	
	protected void SetWheelRotation(long userId, ref GestureData gestureData, Vector3 initialPos, Vector3 currentPos)
	{
		float angle = Vector3.Angle(initialPos, currentPos) * Mathf.Sign(currentPos.y - initialPos.y);
		gestureData.screenPos.z = angle;
	}

	
	/// <summary>
	/// Estimate the state and progress of the given gesture.
	/// </summary>
	/// <param name="userId">User ID</param>
	/// <param name="gestureData">Gesture-data structure</param>
	/// <param name="timestamp">Current time</param>
	/// <param name="jointsPos">Joints-position array</param>
	/// <param name="jointsTracked">Joints-tracked array</param>
	public virtual void CheckForGesture(long userId, ref GestureData gestureData, float timestamp, ref Vector3[] jointsPos, ref bool[] jointsTracked)
	{
		if(gestureData.complete)
			return;
        GestureBase gesture = GestureManager.Instance.GetGesture(gestureData.gesture);
        if (gesture == null) return;
        gesture.bandTopY = jointsPos[rightShoulderIndex].y > jointsPos[leftShoulderIndex].y ? jointsPos[rightShoulderIndex].y : jointsPos[leftShoulderIndex].y;
        gesture.bandBotY = jointsPos[rightHipIndex].y < jointsPos[leftHipIndex].y ? jointsPos[rightHipIndex].y : jointsPos[leftHipIndex].y;

        gesture.bandCenter = (gesture.bandTopY + gesture.bandBotY) / 2f;
        gesture.bandSize = (gesture.bandTopY - gesture.bandBotY);

        gesture.gestureTop = gesture.bandCenter + gesture.bandSize * 1.2f / 2f;
        gesture.gestureBottom = gesture.bandCenter - gesture.bandSize * 1.3f / 4f;
        gesture.gestureRight = jointsPos[rightHipIndex].x;
        gesture.gestureLeft = jointsPos[leftHipIndex].x;
        gesture.kinectGestures = this;
        gesture.CheckForGesture(userId, ref gestureData, timestamp, ref jointsPos, ref jointsTracked);

	}

}
