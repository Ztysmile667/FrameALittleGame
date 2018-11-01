using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class zhengfangti : MonoBehaviour {

	public Material mat;
	// Use this for initialization
	void Start () {
		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();
		gameObject.GetComponent<MeshRenderer>().material = mat;

		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.Clear();

		//设置顶点
		mesh.vertices = new Vector3[]
		{   new Vector3(0, 0, 0),
			new Vector3(1, 0, 0),
			new Vector3(1, 1, 0),
			new Vector3(0, 1, 0),
			new Vector3(0, 1, 1),
			new Vector3(1, 1, 1),
			new Vector3(1, 0, 1),
			new Vector3(0, 0, 1),
		};
		//设置三角形顶点顺序，顺时针设置
		mesh.triangles = new int[]
		{
		  0, 2, 1,
	  0,3,2,
	  3,4,2,
	  4,5,2,
	  4,7,5,
	  7,6,5,
	  7,0,1,
	  6,7,1,
	  4,3,0,
	  4,0,7,
	  2,5,6,
	  2,6,1

		};
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
