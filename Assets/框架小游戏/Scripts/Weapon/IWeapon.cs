using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IWeapon
{
	protected int m_Atk;//武器攻击力
	protected float m_AtkRange;//攻击距离
	protected int m_AtkPlusValue;//额外的伤害，只有敌人有，比如暴击比普通攻击多的那一部分

	protected GameObject m_GameObject;//每把武器控制的游戏物体
	protected ICharacter m_Owner;//拥有者
	//武器身上的组件
	protected ParticleSystem m_Pariticle;
	protected LineRenderer m_Line;
	protected Light m_Light;
	protected AudioSource m_Audio;

	public abstract void Fire(Vector3 targetPosition);

}
