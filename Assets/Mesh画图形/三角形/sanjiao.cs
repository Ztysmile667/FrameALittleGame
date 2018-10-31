using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sanjiao : MonoBehaviour {
	public Material mat;
	// Use this for initialization
	void Start () {
		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();
		gameObject.GetComponent<MeshRenderer>().material = mat;

		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.Clear();

		//设置顶点
		mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0) };
		//设置三角形顶点顺序，顺时针设置
		mesh.triangles = new int[] { 0, 1, 2 };
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
