using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttrStrategy : IAttrStrategy
{
	public int GetAddMaxHPValue(int lv)
	{
		return (lv - 1) * 10;
	}

	public int GetCritDmg(int critRate)
	{
		return 0;
	}

	public int GetDmgDescValue(int lv)
	{
		return (lv - 1) * 5;
	}
}
