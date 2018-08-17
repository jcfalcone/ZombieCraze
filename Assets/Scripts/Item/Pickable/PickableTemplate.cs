using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableTemplate : MonoBehaviour 
{
	[SerializeField]
	[Range(0f,20f)]
	float LifeTime;

	float currTime;

	protected virtual void Update()
	{
		if(this.currTime > this.LifeTime)
		{
			ObjectManager.instance.Destroy(gameObject);
		}

		this.currTime += Time.deltaTime;
	}

	public virtual void Picked(CharacterTemplate _controller)
	{

	}
}
