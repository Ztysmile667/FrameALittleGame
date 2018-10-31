using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScale : MonoBehaviour
{

    private bool isBig =false;

    private bool issmall = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
	    if (transform.localScale.x >= 1f)
	    {
	        isBig = false;
	        issmall = true;
	    }
	    if (transform.localScale.x <= 0.6f)
	    {
	        isBig = true;
	        issmall = false;
        }
        if (isBig == true)
	    {
	       
	        transform.localScale += new Vector3(0.008f, 0.008f, 0);
        }
	    if (issmall == true)
	    {
	        transform.localScale -= new Vector3(0.008f, 0.008f, 0);
        }
	   
        
       
	}
}
