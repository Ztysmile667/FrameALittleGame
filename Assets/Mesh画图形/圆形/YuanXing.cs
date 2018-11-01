using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class YuanXing : MonoBehaviour {

	public Material mat;
	// Use this for initialization
	void Start () {

		DrawCircle(1, 10, new Vector3(0, 0, 0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	/// <summary>
	/// 画圆
	/// </summary>
	/// <param name="radius">圆的半径</param>
	/// <param name="segments">圆的分割数</param>
	/// <param name="centerCircle">圆心得位置</param>
	void DrawCircle(float radius, int segments, Vector3 centerCircle)
	{
		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();
		gameObject.GetComponent<MeshRenderer>().material = mat;

		//顶点
		Vector3[] vertices = new Vector3[segments + 1];
		vertices[0] = centerCircle;
		float deltaAngle = Mathf.Deg2Rad * 360f / segments;
		float currentAngle = 0;
		for (int i = 1; i < vertices.Length; i++)
		{
			float cosA = Mathf.Cos(currentAngle);
			float sinA = Mathf.Sin(currentAngle);
			vertices[i] = new Vector3(cosA * radius + centerCircle.x, sinA * radius + centerCircle.y, 0);
			currentAngle += deltaAngle;
		}

		//三角形
		int[] triangles = new int[segments * 3];
		for (int i = 0, j = 1; i < segments * 3 - 3; i += 3, j++)
		{
			triangles[i] = 0;
			triangles[i + 1] = j + 1;
			triangles[i + 2] = j;
		}
		triangles[segments * 3 - 3] = 0;
		triangles[segments * 3 - 2] = 1;
		triangles[segments * 3 - 1] = segments;


		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.Clear();

		mesh.vertices = vertices;
		mesh.triangles = triangles;
	}
}
