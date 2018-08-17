using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTemplate : CharacterTemplate 
{
	[SerializeField]
	[Range(0, 20)]
	int Damage;

	[SerializeField]
	[Range(0, 20)]
	float ScoreOnHit;

	[SerializeField]
	CharacterTemplate target;

	[SerializeField]
	List<StateTemplate> states;

	[SerializeField]
	StateTemplate currState;

	[SerializeField]
	FloatScriptable playerScore;

	
	[SerializeField]
	PickableItems pickables;

	[SerializeField]
	GameEvent OnScoreChange;

	
	[SerializeField]
	GameEvent OnEnemyCreated;

	
	[SerializeField]
	GameEvent OnEnemyDied;

	Vector3 SpawnPosition;

	int StartHealth = -1;

	// Use this for initialization
	public override void Start () 
	{
		if(this.target == null)
		{
			GameObject target = GameObject.FindGameObjectWithTag("Player");

			if(target != null)
			{
				this.target = target.GetComponent<CharacterTemplate>();
			}
		}

		this.SpawnPosition = transform.position;

		for(int count = 0; count < this.states.Count; count++)
		{
			this.states[count].Init(this);
		}
	}

	public virtual void Init () 
	{
		if(this.StartHealth == -1)
		{
			this.StartHealth = this.Health;
		}

		this.Health = this.StartHealth;
		
		this.currState = this.states[0];

		this.currState.OnEnter();

		this.OnEnemyCreated.Raise();
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		this.currState.Tick();

		StateTemplate state = this.currState.CheckTransitions();

		if(this.currState != state)
		{
			this.currState.OnExit();
			this.currState = state;
			this.currState.OnEnter();
		}
	}

	public Animator GetAnimator()
	{
		return this.anim;
	}

	public Vector3 GetSpawnPosition()
	{
		return this.SpawnPosition;
	}

	public override void Die()
	{
		if(this.isDead)
		{
			return;
		}

		string pickableID = this.pickables.GetRandom();

		if(pickableID != null)
		{
			ObjectManager.instance.Instantiate(pickableID, transform.position, transform.rotation);
		}

		this.collider.enabled = false;
		this.rb.isKinematic = true;

		this.isDead = true;
		this.anim.Play("Death");

		Destroy(gameObject, 5f);

		this.OnEnemyDied.Raise();
	}

	public virtual void OnParticleCollision(GameObject _other)
	{
		WeaponTemplate weapon = _other.GetComponentInParent<WeaponTemplate>();
		
		if(weapon)
		{
			this.playerScore.value += this.ScoreOnHit;
			this.OnScoreChange.Raise();
			
			this.ApplyDamage(weapon.GetDamage());
		}
	}

	public virtual CharacterTemplate GetTarget()
	{
		return this.target;
	}

	public virtual int GetDamage()
	{
		return this.Damage;
	}

	public virtual List<StateTemplate> GetStates()
	{
		return this.states;
	}

	public virtual void SetStates(List<StateTemplate> _states)
	{
		this.states = _states;
	}
}
