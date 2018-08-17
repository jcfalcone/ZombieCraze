using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTemplate : ScriptableObject 
{
	[SerializeField]
	StateTemplate targetState;

	protected StateTemplate parent;
	

	// Use this for initialization
	public virtual void Init (StateTemplate _parent) 
	{
		this.parent = _parent;
	}

	public virtual void CloneData(TransitionTemplate _reference)
	{

	}

	public virtual bool Check()
	{
		return false;
	}

	public virtual StateTemplate NextState()
	{
		return this.targetState;
	}

	public virtual StateTemplate GetTarget()
	{
		return this.targetState;
	}

	public virtual void SetTarget(StateTemplate _target)
	{
		this.targetState = _target;
	}
}
