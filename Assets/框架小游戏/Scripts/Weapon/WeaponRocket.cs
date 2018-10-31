using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRocket: IWeapon
{
	public override void Fire(Vector3 targetPosition)
	{
		Debug.Log("Fire Show Effect:Rocket");
		Debug.Log("Play Music:Rocket");
	}

}
