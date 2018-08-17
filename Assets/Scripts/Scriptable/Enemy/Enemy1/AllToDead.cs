using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="AI/Transition/All To Dead")]
public class AllToDead : TransitionTemplate
{

	public override bool Check()
	{
		if(this.parent.GetParent().IsDead())
		{
			return true;
		}

		return false;
	}
}
