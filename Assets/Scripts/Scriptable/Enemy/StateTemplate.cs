using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTemplate : ScriptableObject
{
	[SerializeField]
	string ID;

	[SerializeField]
	List<TransitionTemplate> transitions;

	protected float timeInState = 0;

	protected EnemyTemplate parent;
	// Use this for initialization
	public virtual void Init (EnemyTemplate _parent) 
	{
		this.parent = _parent;

		for(int count = 0; count < this.transitions.Count; count++)
		{
			this.transitions[count].Init(this);
		}
	}
	
	// Update is called once per frame
	public virtual void Tick () 
	{
		this.timeInState += Time.deltaTime;	
	}

	public virtual void OnEnter()
	{
		this.timeInState = 0;
	}

	public virtual void OnExit()
	{
		
	}

	public virtual StateTemplate CheckTransitions()
	{
		for(int count = 0; count < this.transitions.Count; count++)
		{
			if(this.transitions[count].Check())
			{
				return this.transitions[count].NextState();
			}
		}

		return this;
	}

	public virtual void CloneData(StateTemplate _reference)
	{

	}

	public float GetTimeInState()
	{
		return this.timeInState;
	}

	public EnemyTemplate GetParent()
	{
		return this.parent;
	}

	public List<TransitionTemplate> GetTransitions()
	{
		return this.transitions;
	}

	public void SetTransitions(List<TransitionTemplate> _transitions)
	{
		this.transitions = _transitions;
	}

	public string GetID()
	{
		return this.ID;
	}
}
