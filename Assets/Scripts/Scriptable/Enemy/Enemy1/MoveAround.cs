using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="AI/MoveAround")]
public class MoveAround : StateTemplate
{
	[SerializeField]
	float rotationSpeed;

	[SerializeField]
	float minDistance;

	Vector3 StartPosition;
	Vector3 NextPosition;
	Vector3 NextDirection;
	public override void OnEnter()
	{
		base.OnEnter();

		this.StartPosition = this.parent.GetSpawnPosition();

		this.NextDirection = Vector3.zero;
		this.NextDirection.x = Random.Range(-5f, 5f);
		this.NextDirection.z = Random.Range(-5f, 5f);

		if(this.NextDirection.x < 1f && this.NextDirection.x > -1)
		{
			this.NextDirection.x += (Random.Range(0, 10)%2 == 0)? 1f : -1f;
		}

		if(this.NextDirection.z < 1f && this.NextDirection.z > -1)
		{
			this.NextDirection.z += (Random.Range(0, 10)%2 == 0)? 1f : -1f;
		}

		this.NextPosition = this.StartPosition + this.NextDirection;
	}

	public override void OnExit()
	{
		this.parent.GetAnimator().SetFloat("Speed", 0f);
	}
	
	// Update is called once per frame
	public override void Tick () 
	{
		this.LookAt();
		
		if(this.isLookingAt())
		{
			if(Vector3.Distance(this.parent.transform.position, this.NextPosition) > this.minDistance)
			{
				this.parent.GetAnimator().SetFloat("Speed", 0.11f);
				this.parent.MoveCharacterTo(this.NextPosition);
			}
		}
	}

	public override void CloneData(StateTemplate _reference)
	{
		MoveAround controller = _reference as MoveAround;
		this.rotationSpeed    	= controller.GetRotationSpeed();
		this.minDistance  		= controller.GetMinDistance();
	}
	
	public bool InDistance()
	{
		return Vector3.Distance(this.parent.transform.position, this.NextPosition) <= this.minDistance;
	}

	void LookAt()
	{
		this.parent.transform.rotation = Quaternion.Lerp(this.parent.transform.rotation, Quaternion.LookRotation(this.NextDirection), this.rotationSpeed * Time.deltaTime);
	}

	bool isLookingAt()
	{
		float dotProd = Vector3.Dot(this.NextDirection.normalized, this.parent.transform.forward);

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
}
