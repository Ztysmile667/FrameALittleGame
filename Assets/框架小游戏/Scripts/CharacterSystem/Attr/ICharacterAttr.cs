using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 人物属性的一个类
/// </summary>
public class ICharacterAttr
{
	protected string m_Name;//人物的名字
	protected int m_MaxHP;//人物的最大血量
	protected float m_MoveSpeed;//人物的移动速度

	protected int m_CurrentHP;//人物当前的血量
	protected string m_IconSprite;//头像
	protected int m_Lv;//人物等级，兵营升级的时候战士升级,战士血量增加，减免伤害
	protected float m_CritRate;//暴击率0——1，只有敌人有，战士暴击率为0

	//增加的最大血量，抵御的伤害，暴击增加的伤害，这三个属性我们用策略模式
	protected IAttrStrategy m_Strategy;

}
