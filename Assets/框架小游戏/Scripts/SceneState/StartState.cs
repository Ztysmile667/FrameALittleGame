using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartState : ISceneState
{
	public StartState(SceneStateController controller) : base("2.0", controller)
	{

	}


	private Image m_Logo;
	private float m_SmoothSpeed = 1;//动画时间，时间越大动画越快
	private float m_WaitTime = 2;
	public override void StateStart()
	{
		m_Logo = GameObject.Find("Logo").GetComponent<Image>();
		m_Logo.color = Color.black;
	}

	public override void StateUpdate()
	{
		m_Logo.color = Color.Lerp(m_Logo.color,Color.white, m_SmoothSpeed*Time.deltaTime);
		m_WaitTime -= Time.deltaTime;
		if(m_WaitTime<=0)
		{
			m_Controller.SetState(new MainMeunState(m_Controller));
		}
	}

}
