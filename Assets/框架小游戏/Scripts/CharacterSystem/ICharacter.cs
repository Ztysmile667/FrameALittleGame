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
}
