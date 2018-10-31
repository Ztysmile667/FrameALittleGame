using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEMO_Main : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerManager.Instance.OnSetPrimaryPlayerEvent += Instance_OnSetPrimaryPlayerEvent;
	}
	
	private void Instance_OnSetPrimaryPlayerEvent(Player player)
	{
		player.OnGestureCompeletedEvent += Player_OnGestureCompeletedEvent;
	}

	private void Player_OnGestureCompeletedEvent(Player player,KinectGestures.Gestures gesture)
	{
		Debug.Log(gesture);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
