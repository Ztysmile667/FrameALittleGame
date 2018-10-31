using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alpha : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerManager.Instance.OnSetPrimaryPlayerEvent += Instance_OnSetPrimaryPlayerEvent;
	}

    private void Display_onDisplaysUpdated()
    {
        Debug.Log(Time.realtimeSinceStartup);
    }

    private void Instance_OnSetPrimaryPlayerEvent(Player player)
    {
        Debug.Log(player.userId);
        player.OnGestureCompeletedEvent += Player_OnGestureCompeletedEvent;
    }

    private void Player_OnGestureCompeletedEvent(Player player, KinectGestures.Gestures gesture)
    {

    }

   
}
