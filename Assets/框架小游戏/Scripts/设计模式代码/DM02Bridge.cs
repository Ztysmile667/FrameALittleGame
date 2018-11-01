using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region
//public class DM02Bridge : MonoBehaviour
//{
//	private void Start()
//	{
//		Sphere sphere = new Sphere();
//		sphere.Draw();
//		Cube cube = new Cube();
//		cube.Draw();
//	}

//}
///// <summary>
///// 一个球,游戏中的某个东西
///// </summary>
//public class Sphere
//{
//	public string name = "Sphere";
//	//让这个球有自我绘制的功能
//	public OpenGL openGL = new OpenGL();
//	public DirectX dx = new DirectX();
//	public void Draw()
//	{
//		openGL.Render(name);
//	}
//	public void DrawDX()
//	{
//		dx.Render(name);
//	}

//}
///// <summary>
///// ++++++++++++++++++++++++++++++++++++++++++++++++++++++
///// 假如我们在加一个cube呢，在加以恶搞这个类，
///// 但是如果改了不绘制名字了，那每一个都得改很麻烦
///// </summary>
//public class Cube
//{
//	public string name = "Cube";
//	//让这个球有自我绘制的功能
//	public OpenGL openGL = new OpenGL();
//	public DirectX dx = new DirectX();
//	public void Draw()
//	{
//		openGL.Render(name);
//	}
//	public void DrawDX()
//	{
//		dx.Render(name);
//	}

//}

///// <summary>
///// 绘制引擎
///// </summary>
//public class OpenGL
//{
//	//一个绘制的方法
//	public void Render(string name)
//	{
//		Debug.Log("OpenGL绘制出来了"+name);
//	}
//}
///// <summary>
///// ++++++++++++++++++++++++++++++
///// 加入再多一个引擎呢，就得在每一个类中在new一下，并且多写个类
///// </summary>
//public class DirectX
//{
//	public void Render(string name)
//	{
//		Debug.Log("DirectX绘制出来了" + name);
//	}
//}
#endregion
//以上是不好的，因为不管物体还是引擎都有可能扩展，每次改的代码都会跟多
//======================================================================
//下面的重构完成的

	/// <summary>
	/// 桥接模式
	/// </summary>
public class DM02Bridge : MonoBehaviour
{
	private void Start()
	{
		////只需要改这一个就可以了
		//IRerderEngine renderEngine = new OpenGL();

		//Sphere sphere = new Sphere(renderEngine);
		//sphere.Draw();
		//Cube cube = new Cube(renderEngine);
		//cube.Draw();

		ICharacter character = new SoldierCaptain();
		character.weapon = new WeaponGun();
		character.Attack(Vector3.zero);
	}

}
public class IShape
{
	public string name;
	public IRerderEngine renderEngine;
	public void Draw()
	{
		renderEngine.Render(name);
	}
	public IShape(IRerderEngine renderEngine)
	{
		this.renderEngine = renderEngine;
	}
}
public abstract class IRerderEngine
{
	public abstract void Render(string name);
}

public class Sphere: IShape
{
	public Sphere(IRerderEngine re):base(re)
	{
		name = "Sphere";
	}

}

public class Cube:IShape
{

	public Cube(IRerderEngine re) : base(re)
	{
		name = "Cube";
	}
}

/// <summary>
/// 绘制引擎
/// </summary>
public class OpenGL: IRerderEngine
{
	//一个绘制的方法
	public override void Render(string name)
	{
		Debug.Log("OpenGL绘制出来了" + name);
	}
}
/// <summary>
/// ++++++++++++++++++++++++++++++
/// 加入再多一个引擎呢，就得在每一个类中在new一下，并且多写个类
/// </summary>
public class DirectX: IRerderEngine
{
	public override void Render(string name)
	{
		Debug.Log("DirectX绘制出来了" + name);
	}
}


