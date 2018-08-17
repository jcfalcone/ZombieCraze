using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTemplate : MonoBehaviour 
{
	[SerializeField]
	[Range(0,100)]
	float ItemScore;

	[SerializeField]
	protected FloatScriptable playerScore;

	[SerializeField]
	protected GameEvent OnScoreChange;

	protected ItemBase parent;
	// Use this for initialization
	public virtual void Init (ItemBase _parent) 
	{
		parent = _parent;
	}

	public virtual void Picked(CharacterTemplate _char)
	{
		this.playerScore.value += this.ItemScore;
		this.OnScoreChange.Raise();

		ObjectManager.instance.Destroy(gameObject);
		parent.Picked();
	}
}
