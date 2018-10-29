using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializeField_show : MonoBehaviour {
	//序列化，数据可以保存
	[Tooltip("呐，SerializeField跟public差不多，就是可以序列化一下，基本用不到的")]
	[SerializeField]
	private float timer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
