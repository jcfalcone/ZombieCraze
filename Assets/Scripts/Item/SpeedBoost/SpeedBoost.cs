using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : ItemTemplate 
{
	[SerializeField]
	[Range(0f, 1000f)]
	int speedAmount;

	public override void Picked (CharacterTemplate _char) 
	{
		
		_char.AddSpeed(this.speedAmount);

		base.Picked(_char);
	}
}
