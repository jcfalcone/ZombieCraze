using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEnter : MonoBehaviour 
{
	public GameEvent gameEvent;

	public void OnCollisionEnter(Collision collision)
	{
		this.gameEvent.Raise();
	}
}
