using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="AI/Follow Player")]
public class FollowPlayer : StateTemplate
{
	[SerializeField]
	float rotationSpeed;

	[SerializeField]
	float minDistance;

	[SerializeField]
	float parentSpeedOnEnter;

	[SerializeField]
	float parentSpeedOnExit;

	CharacterTemplate target;

	public override void OnEnter()
	{
		base.OnEnter();
		this.target = this.parent.GetTarget();
		this.parent.GetAnimator().SetFloat("Speed", 1f);

		this.parent.SetSpeed(this.parentSpeedOnEnter);
	}

	public override void OnExit()
	{
		this.parent.GetAnimator().SetFloat("Speed", 0f);
		this.parent.SetSpeed(this.parentSpeedOnExit);
	}
	
	// Update is called once per frame
	public override void Tick () 
	{
		Vector3 targetPos = this.target.transform.position;
		targetPos.y = this.parent.transform.position.y;

		this.LookAt(targetPos);
		
		if(Vector3.Distance(this.parent.transform.position, this.target.transform.position) > this.minDistance)
		{
			this.parent.GetAnimator().SetFloat("Speed", 1f);
			this.parent.MoveCharacterTo(targetPos);
		}
		else
		{
			this.parent.GetAnimator().SetFloat("Speed", 0f);
		}
	}

	public override void CloneData(StateTemplate _reference)
	{
		FollowPlayer controller = _reference as FollowPlayer;
		this.rotationSpeed    	= controller.GetRotationSpeed();
		this.minDistance  		= controller.GetMinDistance();
		this.parentSpeedOnEnter = controller.GetParentSpeedOnEnter();
		this.parentSpeedOnExit  = controller.GetParentSpeedOnExit();
	}
	
	public bool InDistance()
	{
		return Vector3.Distance(this.parent.transform.position, this.target.transform.position) <= this.minDistance;
	}

	void LookAt(Vector3 _targetPos)
	{
		Vector3 direction = _targetPos - this.parent.transform.position;
		this.parent.transform.rotation = Quaternion.Lerp(this.parent.transform.rotation, Quaternion.LookRotation(direction), this.rotationSpeed * Time.deltaTime);
	}

	bool isLookingAt(Vector3 _targetPos)
	{
		Vector3 direction = _targetPos - this.parent.transform.position;

		float dotProd = Vector3.Dot(direction.normalized, this.parent.transform.forward);

		if(dotProd > 0.99f)
		{
			return true;
		}

		return false;
	}

	public float GetRotationSpeed()
	{
		return this.rotationSpeed;
	}

	public float GetMinDistance()
	{
		return this.minDistance;
	}

	public float GetParentSpeedOnEnter()
	{
		return this.parentSpeedOnEnter;
	}

	public float GetParentSpeedOnExit()
	{
		return this.parentSpeedOnExit;
	}
}
