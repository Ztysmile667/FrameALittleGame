using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMeunState : ISceneState
{

	public MainMeunState(SceneStateController controller) : base("2.1MainMeunScene", controller)
	{

	}

	public override void StateStart()
	{
		GameObject.Find("StartButton").GetComponent<Button>().onClick.AddListener(OnSetButtonClick);
	}
	private void OnSetButtonClick()
	{
		m_Controller.SetState(new BattleState(m_Controller));
	}
}
