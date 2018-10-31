using UnityEngine;
using System.Collections;
using System;
//using Windows.Kinect;

public class CubeGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
    [Header("是否按玩家排序队列进行进行手势操作")]
    public bool Queue = false;
	[Header("玩家序号")]
	public int playerIndex = 0;

	[Tooltip("GUI-Text to display gesture-listener messages and gesture information.")]
	public GUIText gestureInfo;

	// singleton instance of the class
	private static CubeGestureListener instance = null;

	// internal variables to track if progress message has been displayed
	private bool progressDisplayed;
	private float progressGestureTime;


	

	/// <summary>
	/// Gets the singleton CubeGestureListener instance.
	/// </summary>
	/// <value>The CubeGestureListener instance.</value>
	public static CubeGestureListener Instance
	{
		get
		{
			return instance;
		}
	}
	
	/// <summary>
	/// Determines whether swipe left is detected.
	/// </summary>
	/// <returns><c>true</c> if swipe left is detected; otherwise, <c>false</c>.</returns>

	

	/// <summary>
	/// Invoked when a new user is detected. Here you can start gesture tracking by invoking KinectManager.DetectGesture()-function.
	/// </summary>
	/// <param name="userId">User ID</param>
	/// <param name="userIndex">User index</param>
	public void UserDetected(long userId, int userIndex)
	{
		// the gestures are allowed for the primary user only
		KinectManager manager = KinectManager.Instance;
		if(!manager)
			return;
        if(Queue)
        {
            if (userIndex != playerIndex) return;
        }
		
		// detect these user specific gestures
		manager.DetectGesture(userId, KinectGestures.Gestures.SwipeLeft);
		manager.DetectGesture(userId, KinectGestures.Gestures.SwipeRight);
		manager.DetectGesture(userId, KinectGestures.Gestures.RaiseRightHand);
        manager.DetectGesture(userId, KinectGestures.Gestures.RightHandCatch);
        manager.DetectGesture(userId, KinectGestures.Gestures.RightSwipeDown);
        manager.DetectGesture(userId, KinectGestures.Gestures.ZombieGesture);
        if (gestureInfo != null)
		{
			gestureInfo.text = "Swipe left, right or up to change the slides.";
		}
	}

	/// <summary>
	/// Invoked when a user gets lost. All tracked gestures for this user are cleared automatically.
	/// </summary>
	/// <param name="userId">User ID</param>
	/// <param name="userIndex">User index</param>
	public void UserLost(long userId, int userIndex)
	{
        // the gestures are allowed for the primary user only
        if (Queue)
        {
            if (userIndex != playerIndex) return;
        }
        if (gestureInfo != null)
		{
			gestureInfo.text = string.Empty;
		}
	}

	/// <summary>
	/// Invoked when a gesture is in progress.
	/// </summary>
	/// <param name="userId">User ID</param>
	/// <param name="userIndex">User index</param>
	/// <param name="gesture">Gesture type</param>
	/// <param name="progress">Gesture progress [0..1]</param>
	/// <param name="joint">Joint type</param>
	/// <param name="screenPos">Normalized viewport position</param>
	public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture, 
	                              float progress, KinectInterop.JointType joint, Vector3 screenPos)
	{
        if (Queue)
        {
            if (userIndex != playerIndex) return;
        }
        if ((gesture == KinectGestures.Gestures.ZoomOut || gesture == KinectGestures.Gestures.ZoomIn) && progress > 0.5f)
		{
			if(gestureInfo != null)
			{
				string sGestureText = string.Format ("{0} - {1:F0}%", gesture, screenPos.z * 100f);
				gestureInfo.text = sGestureText;
				
				progressDisplayed = true;
				progressGestureTime = Time.realtimeSinceStartup;
			}
		}
		else if((gesture == KinectGestures.Gestures.Wheel || gesture == KinectGestures.Gestures.LeanLeft || 
		         gesture == KinectGestures.Gestures.LeanRight) && progress > 0.5f)
		{
			if(gestureInfo != null)
			{
				string sGestureText = string.Format ("{0} - {1:F0} degrees", gesture, screenPos.z);
				gestureInfo.text = sGestureText;
				
				progressDisplayed = true;
				progressGestureTime = Time.realtimeSinceStartup;
			}
		}
		else if(gesture == KinectGestures.Gestures.Run && progress > 0.5f)
		{
			if(gestureInfo != null)
			{
				string sGestureText = string.Format ("{0} - progress: {1:F0}%", gesture, progress * 100);
				gestureInfo.text = sGestureText;
				
				progressDisplayed = true;
				progressGestureTime = Time.realtimeSinceStartup;
			}
		}
	}

	/// <summary>
	/// Invoked if a gesture is completed.
	/// </summary>
	/// <returns>true</returns>
	/// <c>false</c>
	/// <param name="userId">User ID</param>
	/// <param name="userIndex">User index</param>
	/// <param name="gesture">Gesture type</param>
	/// <param name="joint">Joint type</param>
	/// <param name="screenPos">Normalized viewport position</param>
	public bool GestureCompleted (long userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectInterop.JointType joint, Vector3 screenPos)
	{
        if (Queue)
        {
            if (userIndex != playerIndex) return false;
        }
        if (gestureInfo != null)
		{
			string sGestureText = gesture + " detected";
			gestureInfo.text = sGestureText;
		}

        Player player = PlayerManager.Instance.GetPlayer(userId);
        if(player!=null)
        {
            player.CheckPoseComplete(gesture);
        }

		return true;
	}

	/// <summary>
	/// Invoked if a gesture is cancelled.
	/// </summary>
	/// <returns>true</returns>
	/// <c>false</c>
	/// <param name="userId">User ID</param>
	/// <param name="userIndex">User index</param>
	/// <param name="gesture">Gesture type</param>
	/// <param name="joint">Joint type</param>
	public bool GestureCancelled (long userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectInterop.JointType joint)
	{
	
		if(progressDisplayed)
		{
			progressDisplayed = false;
			
			if(gestureInfo != null)
			{
				gestureInfo.text = String.Empty;
			}
		}
		
		return true;
	}

	
	void Awake()
	{
		instance = this;
	}

	void Update()
	{
		if(progressDisplayed && ((Time.realtimeSinceStartup - progressGestureTime) > 2f))
		{
			progressDisplayed = false;
			gestureInfo.text = String.Empty;

			Debug.Log("Forced progress to end.");
		}
	}

}
