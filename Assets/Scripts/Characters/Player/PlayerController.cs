using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterTemplate 
{
	[Header("Player")]
	[SerializeField]
	IntScriptable PlayerPrevHealth;

	[SerializeField]
	IntScriptable PlayerHealth;

	[SerializeField]
	IntScriptable PlayerTotalHealth;

	[SerializeField]
	FloatScriptable PlayerScore;

	[SerializeField]
	PlayerData data;

	[Header("Data")]
	[SerializeField]
	[Range(0f, 5f)]
	float SpeedReturnRatio;

	[Header("References")]
	[SerializeField]
	WeaponTemplate currWeapon;

	[SerializeField]
	WeaponTemplate defaultWeapon;

	[SerializeField]
	PlayerInput input;

	[SerializeField]
	Material playerMaterial;

	[Header("Events")]
	[SerializeField]
	GameEvent OnHealthChange;

	[SerializeField]
	GameEvent OnPlayerDie;

	[SerializeField]
	GameEvent OnPlayerSpawn;

	Vector3 lastFramePosition;

	bool isReloading;
	// Use this for initialization
	public override void Start () 
	{
		base.Start();

		this.OnHealthChange.Raise();

		if(this.input)
		{
			this.input.Init();
		}

		this.Init();
	}

	public void Init()
	{
		this.lastFramePosition = transform.position;

		this.SetWeapon(this.defaultWeapon);

		if(this.currWeapon)
		{
			this.currWeapon.Init();
		}

		this.PlayerHealth.value = this.Health;
		this.PlayerTotalHealth.value = this.Health;
		this.PlayerPrevHealth.value = this.Health;

		this.isDead = false;
		this.collider.enabled = true;
		this.rb.isKinematic = false;

		this.PlayerScore.value = 0;

		this.anim.CrossFade("Empty", 0.2f);
		this.anim.Play("Attack");

		this.OnPlayerSpawn.Raise();
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		if(this.isDead)
		{
			return;
		}
		
		this.CheckMove();
        transform.LookAt(this.data.mousePosition);

		if(this.Speed != this.InitialSpeed)
		{
			this.Speed = Mathf.Lerp(this.Speed, this.InitialSpeed, Time.deltaTime * this.SpeedReturnRatio);
		}

		if(this.currWeapon)
		{
			this.currWeapon.Tick();
		}

		if(this.input)
		{
			this.input.Init();
		}

		if(this.data.fire && !this.isReloading)
		{
			this.currWeapon.Shoot();
		}
		else
		{
			this.currWeapon.StopBullets();
		}

		if(this.data.reload && !this.isReloading)
		{
			this.StartReload();
		}
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		if(this.isDead)
		{
			return;
		}

		if(this.input)
		{
			this.input.LateTick();
		}
	}

	public override void ApplyDamage(int _damage)
	{
		this.PlayerPrevHealth.value = this.PlayerHealth.value;
		this.PlayerHealth.value -= _damage;

		if(this.PlayerHealth.value <= 0)
		{
			this.PlayerHealth.value = 0;
			this.Die();
		}
		else
		{
			this.anim.CrossFade("Hit"+(Random.Range(0,100) % 2).ToString(), 0.1f);
		}

		this.OnHealthChange.Raise();
	}

	public void StartReload()
	{
		if(!this.currWeapon.HasBullets() && this.currWeapon.GetID() != this.defaultWeapon.GetID())
		{
			this.defaultWeapon.gameObject.SetActive(true);
			this.SetWeapon(this.defaultWeapon);
			return;
		}
		
		this.anim.CrossFade("Reload", 0.2f);
		this.isReloading = true;
	}

	public void EndReload()
	{
		this.currWeapon.Reload();
		this.isReloading = false;
	}

	IEnumerator SimulateReload()
	{
		yield return new WaitForSeconds(1.3f);
	}

	IEnumerator SimulateDeath()
	{
		yield return new WaitForSeconds(3f);
		this.Die();
	}

	public override void Die()
	{
		this.collider.enabled = false;
		this.rb.isKinematic = true;

		this.isDead = true;
		this.anim.Play("Death");
		this.anim.Play("StopArm");

		this.OnPlayerDie.Raise();
	}

	public void CheckMove()
	{
		Vector3 forward = Vector3.forward * this.data.AxisMovement.z;
		Vector3 right = Vector3.right * this.data.AxisMovement.x;
		this.MoveCharacter(forward + right);

		Vector3 Normalize = (transform.position - this.lastFramePosition).normalized;

		this.anim.SetFloat("Horizontal", Normalize.x * this.data.AxisMovement.x);
		this.anim.SetFloat("Vertical", Normalize.z * this.data.AxisMovement.z);
	}

	public override void AddHealth(int _amount)
	{
		this.PlayerHealth.value = Mathf.Clamp(this.PlayerHealth.value + _amount, 0, this.PlayerTotalHealth.value);
	}

	public override bool IsFullHealth()
	{
		return this.PlayerHealth.value == this.PlayerTotalHealth.value;
	}

	public override bool IsDead()
	{
		return this.isDead || this.PlayerHealth.value <= 0;
	}

	void OnTriggerEnter(Collider _item)
	{
		if(_item.CompareTag("Item"))
		{
			ItemTemplate controller = _item.GetComponent<ItemTemplate>();

			if(controller != null)
			{
				controller.Picked(this);
			}
		}
		else if(_item.CompareTag("Pickable"))
		{
			PickableTemplate controller = _item.GetComponent<PickableTemplate>();

			if(controller != null)
			{
				controller.Picked(this);
			}
		}
	}

	public override WeaponTemplate GetWeapon()
	{
		return this.currWeapon;
	}

	public override void SetWeapon(WeaponTemplate _weapon)
	{
		if(this.currWeapon != _weapon)
		{
			if(this.currWeapon == this.defaultWeapon)
			{
				this.currWeapon.gameObject.SetActive(false);
			}
			else
			{
				ObjectManager.instance.Destroy(this.currWeapon.gameObject);
			}
		}

		this.currWeapon = _weapon;
		this.currWeapon.transform.parent = transform;

		this.currWeapon.Init();
		

		this.playerMaterial.SetColor("_ArmColor", this.currWeapon.GetColor());
	}

	public override void AddWeapon(string _weaponID, WeaponTemplate _weaponPrefab)
	{
		if(this.currWeapon.GetID() == _weaponID)
		{
			this.currWeapon.AddBullet(_weaponPrefab.GetMagazineSize());
		}
		else
		{
			WeaponTemplate controller = null;

			if(this.defaultWeapon.GetID() != _weaponID)
			{
				GameObject instance = ObjectManager.instance.Instantiate(_weaponID, transform.position, Quaternion.identity);
		
				controller = instance.GetComponent<WeaponTemplate>();
			}
			else
			{
				controller = this.defaultWeapon;
				controller.gameObject.SetActive(true);
			}

			if(controller != null)
			{
				this.SetWeapon(controller);
			}
		}
	}
}
