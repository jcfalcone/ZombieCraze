using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : ItemTemplate
{
	[SerializeField]
	[Range(0f, 500f)]
	int bulletAmount;

	public override void Picked (CharacterTemplate _char) 
	{
		WeaponTemplate weapon = _char.GetWeapon();

		if(weapon != null)
		{
			weapon.AddBullet(this.bulletAmount);

			base.Picked(_char);
		}
	}
}
