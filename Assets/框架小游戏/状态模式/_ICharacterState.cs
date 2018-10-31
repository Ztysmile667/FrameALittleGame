using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态的共用代码
/// </summary>
public class _ICharacterState
{
	private string m_StateName;
	public string StateName
	{
		get { return m_StateName; }	
	}
	protected _ConText m_ConText;
	public _ICharacterState(string name, _ConText context)
	{
		m_StateName = name;
		m_ConText = context;
	}

	public virtual void StartState() { }
	public virtual void EndState() { }
	public virtual void UpdateState() { }

}
