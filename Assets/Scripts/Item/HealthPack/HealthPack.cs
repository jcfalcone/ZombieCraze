using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : ItemTemplate 
{
	[SerializeField]
	[Range(0f, 10f)]
	int healthAmount;
	
	public override void Picked (CharacterTemplate _char) 
	{
		if(_char.IsFullHealth())
		{
			return;
		}

		_char.AddHealth(this.healthAmount);

		base.Picked(_char);
	}
}
