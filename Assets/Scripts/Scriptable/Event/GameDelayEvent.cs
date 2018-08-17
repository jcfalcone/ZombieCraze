using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDelayEvent : MonoBehaviour 
{
	[SerializeField]
	GameEvent listener;

	public void DelayRaise(float _time)
	{
		StartCoroutine(this.LateRaise(_time));
	}

	IEnumerator LateRaise(float _time)
	{
		yield return new WaitForSeconds(_time);
		this.listener.Raise();
	}
}
