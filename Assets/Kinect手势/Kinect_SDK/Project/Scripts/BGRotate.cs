using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGRotate : MonoBehaviour
{

   

    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate(0,0,1.5f);
	}
}
