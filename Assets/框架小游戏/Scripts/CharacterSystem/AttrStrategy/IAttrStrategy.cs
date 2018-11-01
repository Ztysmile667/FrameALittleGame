using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttrStrategy
{
	 int GetAddMaxHPValue(int lv) ;
	int GetDmgDescValue(int lv);
	int GetCritDmg(int critRate);
}
