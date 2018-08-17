using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTemplate : MonoBehaviour 
{
	[Header("Data")]
	[SerializeField]
	string ID;

	[SerializeField]
	string Name;

	[SerializeField]
	[Range(0, 300)]
	int maxBullet;

	[SerializeField]
	[Range(0, 100)]
	int magazineBullet;

	[SerializeField]
	[ColorUsageAttribute(true,true)]
	Color WeaponColor;

	[SerializeField]
	IntScriptable currTotalBullets;

	[SerializeField]
	IntScriptable currWeaponBullets;

	[SerializeField]
	StringScriptable currWeaponName;


	int totalBullet;
	int currentBullets;

	[SerializeField]
	[Range(0, 10)]
	int damage;

	[SerializeField]
	[Range(0f, 2f)]
	float cooldown;

	[Header("Model")]
	[SerializeField]
	Vector3 localPosition;

	[SerializeField]
	Vector3 localRotation;

	[Header("References")]
	[SerializeField]
	Animator anim;

	[SerializeField]
	List<ParticleSystem> particles;

	[Header("Events")]
	[SerializeField]
	GameEvent OnEmptyMagazine;

	[SerializeField]
	GameEvent OnUpdateBullet;

	[SerializeField]
	GameEvent OnWeaponChange;

	bool inCooldown = false;
	bool isReloading = false;

	// Use this for initialization
	public virtual void Init () 
	{
		transform.localPosition = this.localPosition;
		transform.localRotation = Quaternion.Euler(this.localRotation);
		this.totalBullet = this.maxBullet;
		this.currentBullets = this.magazineBullet;
		this.currTotalBullets.value = this.totalBullet;
		this.currWeaponBullets.value = this.currentBullets;

		this.currWeaponName.value = this.Name;

		this.OnUpdateBullet.Raise();
		this.OnWeaponChange.Raise();
	}
	
	// Update is called once per frame
	public virtual void Tick () 
	{
		
	}

	public virtual void Shoot()
	{
		if(this.inCooldown || this.isReloading)
		{
			return;
		}

		if(this.currentBullets <= 0)
		{
			this.OnEmptyMagazine.Raise();
			return;
		}

		if(this.anim)
		{
			this.anim.Play("Shoot");
		}

		for(int count = 0; count < this.particles.Count; count++)
		{
			this.particles[count].Play(true);
		}

		this.currentBullets--;
		this.inCooldown = true;

		this.currWeaponBullets.value = this.currentBullets;

		this.OnUpdateBullet.Raise();

		StartCoroutine(this.Cooldown());
	}

	public virtual void Reload()
	{
		if(this.totalBullet <= 0 || this.currentBullets == this.magazineBullet)
		{
			return;
		}

		if(this.totalBullet > this.magazineBullet)
		{
			this.currentBullets = this.magazineBullet;
			this.totalBullet 	   -= this.magazineBullet;
		}
		else
		{
			this.currentBullets = this.totalBullet;
			this.totalBullet = 0;
		}

		this.currTotalBullets.value = this.totalBullet;
		this.currWeaponBullets.value = this.currentBullets;
		this.OnUpdateBullet.Raise();
	}

	public virtual void StopBullets()
	{
		for(int count = 0; count < this.particles.Count; count++)
		{
			this.particles[count].Stop(false, ParticleSystemStopBehavior.StopEmitting);
		}
	}

	public int GetDamage()
	{
		return this.damage;
	}

	IEnumerator Cooldown()
	{
		yield return new WaitForSeconds(this.cooldown);
		inCooldown = false;
	}

	public virtual void AddBullet(int _amount)
	{
		this.totalBullet = Mathf.Clamp(this.currTotalBullets.value + _amount, 0, 999);
		this.currTotalBullets.value = this.totalBullet;
		this.OnUpdateBullet.Raise();

		if(this.currentBullets <= 0)
		{
			this.OnEmptyMagazine.Raise();
			return;
		}
	}

	public virtual Color GetColor()
	{
		return this.WeaponColor;
	}

	public virtual string GetID()
	{
		return this.ID;
	}

	public virtual int GetMagazineSize()
	{
		return this.magazineBullet;
	}

	public virtual bool HasBullets()
	{
		return this.totalBullet > 0 || this.currentBullets > 0;
	}
}
