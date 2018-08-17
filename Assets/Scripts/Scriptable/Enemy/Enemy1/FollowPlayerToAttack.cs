using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="AI/Transition/FollowPlayer to Attack")]
public class FollowPlayerToAttack : TransitionTemplate
{

	[SerializeField]
	float minDistance;

	CharacterTemplate target;

	// Use this for initialization
	public override void Init (StateTemplate _parent) 
	{
		base.Init(_parent);

		this.target = this.parent.GetParent().GetTarget();
	}

	public override bool Check()
	{
		if(Vector3.Distance(this.target.transform.position, this.parent.GetParent().transform.position) <= this.minDistance)
		{
			return true;
		}

		return false;
	}

	public override void CloneData(TransitionTemplate _reference)
	{
		FollowPlayerToAttack controller = (_reference) as FollowPlayerToAttack;

		this.minDistance = controller.GetMinDistante();
	}

	public float GetMinDistante()
	{
		return this.minDistance;
	}
}
