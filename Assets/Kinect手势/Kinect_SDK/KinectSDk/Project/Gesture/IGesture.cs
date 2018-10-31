using UnityEngine;




public interface IGesture
{
    void CheckForGesture(long userId, ref KinectGestures.GestureData gestureData, float timestamp, ref Vector3[] jointsPos, ref bool[] jointsTracked);

}
