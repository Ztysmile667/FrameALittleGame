using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRifle : IWeapon
{
	public override void Fire(Vector3 targetPosition)
	{
		Debug.Log("Fire Show Effect:Rifle");
		Debug.Log("Play Music:Rifle");
	}

}
