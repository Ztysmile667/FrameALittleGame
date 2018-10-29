using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneStateController
{
	private ISceneState m_State;
	private AsyncOperation m_AO;
	private bool m_IsRunStart = false;
	/// <summary>
	/// 
	/// </summary>
	/// <param name="state"></param>
	/// <param name="isLoadScene"></param>是否需要加载场景
	public void SetState(ISceneState state, bool isLoadScene = true)
	{
		if (m_AO != null && m_AO.isDone == false) return;

		if(m_State!= null)
		{
			m_State.StateEnd();//让上一个场景状态做一下清理工作
		}
		m_State = state;
		if (isLoadScene)
		{
			m_AO = SceneManager.LoadSceneAsync(m_State.SceneName);
			m_IsRunStart = false;
		}
		else
		{
			m_State.StateStart();
			isLoadScene = true;
		}
	  
	}

	public void StateUpdate()
	{
		if(m_AO!= null&&m_AO.isDone == true&& m_IsRunStart == false)
		{
			m_State.StateStart();
			m_IsRunStart = true;
		}
		if(m_State!=null)
		{
			m_State.StateUpdate();
		}
	}
}
