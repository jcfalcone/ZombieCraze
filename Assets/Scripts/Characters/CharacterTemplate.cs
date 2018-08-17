using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTemplate : MonoBehaviour 
{
	[Header("Char")]
	[SerializeField]
	[Range(0, 100)]
	protected int Health;

	[SerializeField]
	[Range(0f, 1000f)]
	protected float Speed;

	protected float InitialSpeed;

	[Header("References")]
	[SerializeField]
	protected Rigidbody rb;

	[SerializeField]
	protected Collider collider;

	[SerializeField]
	protected Animator anim;

	protected bool isDead;

	// Use this for initialization
	public virtual void Start () 
	{
		this.InitialSpeed = this.Speed;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}

	public virtual void ApplyDamage(int _damage)
	{
		this.Health -= _damage;

		if(this.Health <= 0)
		{
			this.Health = 0;
			this.Die();
		}
	}

	public virtual void Die()
	{
		Destroy(gameObject);
	}

	public virtual void MoveCharacter(Vector3 _moveDirection)
	{
		this.rb.velocity = (_moveDirection * this.Speed * Time.deltaTime);
	}

	public virtual void MoveCharacterTo(Vector3 _TargetPosition)
	{
		this.transform.position = Vector3.Lerp(this.transform.position, _TargetPosition, Time.deltaTime * this.Speed);
	}

	public int GetHealth()
	{
		return this.Health;
	}

	public virtual bool IsFullHealth()
	{
		return false;
	}

	public virtual bool IsDead()
	{
		return this.isDead || this.Health <= 0;
	}

	public void SetSpeed(float _speed)
	{
		this.Speed = _speed;
	}

	public void AddSpeed(float _speed)
	{
		this.Speed += _speed;
	}

	public virtual void AddHealth(int _amount)
	{
		this.Health += _amount;
	}

	public virtual WeaponTemplate GetWeapon()
	{
		return null;
	}

	public virtual void SetWeapon(WeaponTemplate _weapon)
	{
		
	}

	public virtual void AddWeapon(string _weaponID, WeaponTemplate _weaponPrefab)
	{

	}
}
