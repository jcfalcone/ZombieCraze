using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnControl : UIElement
{
	[SerializeField]
	GameEvent OnPlayerRespawn;
	
	// Update is called once per frame
	public override void Tick () 
	{
		if(Input.GetKeyUp(KeyCode.R))
		{
			this.OnPlayerRespawn.Raise();
		}
	}
}
