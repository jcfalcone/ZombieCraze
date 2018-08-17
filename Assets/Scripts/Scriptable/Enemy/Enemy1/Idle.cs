using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="AI/Idle")]
public class Idle : StateTemplate 
{
	public override void OnEnter()
	{
		base.OnEnter();
		
		this.parent.GetAnimator().CrossFade("Idle", 0.2f);
	}
}
