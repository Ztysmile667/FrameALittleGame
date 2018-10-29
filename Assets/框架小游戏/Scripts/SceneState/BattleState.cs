using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏战斗界面
/// </summary>
public class BattleState : ISceneState
{

	public BattleState(SceneStateController controller) : base("2.2BattleScene", controller)
	{

	}
	//战斗场景中会有各种系统，什么兵营啊，关卡啊，角色管理啊，行动力啊啥啥啥都啥啊

	public override void StateStart()

	{
		GameFacade.Instance.Init();
	}

	public override void StateEnd()
	{
		 GameFacade.Instance.Release();
	}
	public override void StateUpdate()
	{
		//如果游戏结束，返回主菜单
		if(GameFacade.Instance.IsGameOver)
		{
			m_Controller.SetState(new MainMeunState(m_Controller));
		}
			 GameFacade.Instance.Update();
	}
}
