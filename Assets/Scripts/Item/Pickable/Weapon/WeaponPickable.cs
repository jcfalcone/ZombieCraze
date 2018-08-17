using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickable : PickableTemplate
{
	[SerializeField]
	string weaponID;

	[SerializeField]
	WeaponTemplate weaponPrefab;

	public override void Picked(CharacterTemplate _controller)
	{
		_controller.AddWeapon(this.weaponID, this.weaponPrefab);

		ObjectManager.instance.Destroy(gameObject);	
	}
}
