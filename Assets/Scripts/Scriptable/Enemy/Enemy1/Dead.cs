using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="AI/Dead")]
public class Dead : StateTemplate
{
	public override void OnEnter()
	{
		base.OnEnter();
		
		this.parent.Die();
	}
}
