using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : _ICharacterState {

	public IdleState(_ConText context):base("IdleState", context)
	{

	}
	public override void StartState()
	{
		base.StartState();
	}
	public override void UpdateState()
	{
		//在这里面写条件切换状态
		base.UpdateState();
	}
	public override void EndState()
	{
		base.EndState();
	}
}
