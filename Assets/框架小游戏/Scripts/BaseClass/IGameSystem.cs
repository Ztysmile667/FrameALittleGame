using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏系统的公共基类
/// </summary>
public abstract class IGameSystem
{
	public virtual void Init() { }
	public virtual void Update() { }
	public virtual void Release() { }

}
