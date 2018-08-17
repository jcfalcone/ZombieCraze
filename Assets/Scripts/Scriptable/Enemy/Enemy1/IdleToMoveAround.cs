using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="AI/Transition/Idle To MoveAround")]
public class IdleToMoveAround : TransitionTemplate 
{
	[SerializeField]
	float TimeInIdle;

	public override bool Check()
	{
		if(this.parent.GetTimeInState() > this.TimeInIdle)
		{
			return true;
		}

		return false;
	}

	public override void CloneData(TransitionTemplate _reference)
	{
		IdleToMoveAround controller = (_reference) as IdleToMoveAround;

		this.TimeInIdle = controller.GetTimeInIdle ();
	}

	public float GetTimeInIdle ()
	{
		return this.TimeInIdle;
	}
}
