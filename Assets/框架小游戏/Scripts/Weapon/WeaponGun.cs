using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGun : IWeapon
{
	//一个攻击的方法
	public override void Fire(Vector3 targetPosition)
	{
		Debug.Log("Fire Show Effect:Gun");
		Debug.Log("Play Music:Gun");
	}

}
