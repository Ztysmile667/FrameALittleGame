using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态的共用接口
/// </summary>
public class ISceneState
{
	private string m_SceneName;
	protected SceneStateController m_Controller;//状态的拥有者
	public ISceneState(string sceneName, SceneStateController controller)
	{
		m_SceneName = sceneName;
		m_Controller = controller;
	}


	public string SceneName
	{
		get { return m_SceneName; }
	}


	//每次进入到这个状态的时候调用
	public virtual void StateStart()
	{

	}
	//状态切换时消除的东西
	public virtual void StateEnd()
	{

	}
	//处理当前状态的事情
	public virtual void StateUpdate()
	{

	}
}
