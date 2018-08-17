using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="AI/Attack")]
public class Attack : StateTemplate
{
	[SerializeField]
	float CoolDown;

	[SerializeField]
	float AttackTime;

	CharacterTemplate target;

	float currTime = 0;

	bool doAttack = false;

	public override void OnEnter()
	{
		base.OnEnter();
		this.target = this.parent.GetTarget();

		this.currTime = this.CoolDown;
	}
	
	// Update is called once per frame
	public override void Tick () 
	{
		this.currTime += Time.deltaTime;

		if(this.currTime > this.AttackTime && this.doAttack)
		{
			this.target.ApplyDamage(this.parent.GetDamage());
			this.doAttack = false;
		}

		if(this.currTime < this.CoolDown)
		{
			return;
		}

		this.parent.GetAnimator().CrossFade("Attack", 0f);
		this.currTime = 0;
		this.doAttack = true;
	}

	public override void CloneData(StateTemplate _reference)
	{
		Attack controller = _reference as Attack;
		this.CoolDown    = controller.GetCoolDown();
		this.AttackTime  = controller.GetAttackTime();
	}

	public float GetCoolDown()
	{
		return this.CoolDown;
	}

	public float GetAttackTime()
	{
		return this.AttackTime;
	}
}
