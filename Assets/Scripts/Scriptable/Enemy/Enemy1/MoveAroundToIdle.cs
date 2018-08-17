using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="AI/Transition/MoveAround to Idle")]
public class MoveAroundToIdle : TransitionTemplate 
{
	MoveAround state;

	// Use this for initialization
	public override void Init (StateTemplate _parent) 
	{
		base.Init(_parent);
		this.state = (MoveAround)this.parent;

	}

	public override bool Check()
	{
		if(this.state.InDistance())
		{
			return true;
		}

		return false;
	}
}
