using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class ICharacter 
{
	protected ICharacterAttr m_Attr;
	protected GameObject m_GameObjent;//当前角色关联的游戏物体
	protected NavMeshAgent m_NavAgent;//导航组件
	protected AudioSource m_Audio;//播放声音


	protected IWeapon m_Weapon;
	public IWeapon weapon { set { m_Weapon = value; } }
	//备注的是没有定义IWeapon接口前的操作
	//protected WeaponGun gun;
	//protected WeaponRifle rifle;
	//每一个角色都有攻击的功能
	public void Attack(Vector3 targetPosition)
	{
		//if(gun!= null)
		//{
		//	gun.Fire(targetPosition);
		//}
		//else if (rifle != null)
		//{
		//	rifle.Fire(targetPosition);
		//}
		m_Weapon.Fire(targetPosition);
	}
}
