using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态的拥有者
/// </summary>
public class _ConText
{
	private _ICharacterState m_CharacterState;

	public void SetState(_ICharacterState state)
	{
		if (m_CharacterState != null)
		{
			m_CharacterState.EndState();
		}
		m_CharacterState = state;
	}
	public void UpdateState()
	{
		m_CharacterState.UpdateState();
	}
}
