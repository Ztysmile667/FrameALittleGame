using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GameStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		_ConText context = new _ConText();
		context.SetState(new IdleState( context));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
