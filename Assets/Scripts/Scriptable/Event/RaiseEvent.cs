using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseEvent : MonoBehaviour {
	[SerializeField]
	GameEvent gameEvent;
	
	public void Raise () 
	{
		this.gameEvent.Raise();
	}
}
