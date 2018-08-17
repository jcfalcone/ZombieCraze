using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneEnemy : CloneTemplate
{
	[SerializeField]
	EnemyTemplate parent;

	public override void CloneData()
	{

		List<StateTemplate> states = this.parent.GetStates();
		List<StateTemplate> clonedStates = new List<StateTemplate>();

		//this.parent.Start();

		for(int count = 0; count < states.Count; count++)
		{
			clonedStates.Add(Instantiate(states[count]));
			clonedStates[count].name = this.parent.gameObject.name+"_"+clonedStates[count].name;
		}	

		for(int count = 0; count < clonedStates.Count; count++)
		{
			List<TransitionTemplate> transition = clonedStates[count].GetTransitions();
			List<TransitionTemplate> clonedTransition = new List<TransitionTemplate>();
			

			for(int countT = 0; countT < transition.Count; countT++)
			{
				clonedTransition.Add(Instantiate(transition[countT]));
				clonedTransition[countT].name = this.parent.gameObject.name+"_"+clonedTransition[countT].name;

				string targetId = clonedTransition[countT].GetTarget().GetID();

				StateTemplate newTarget = clonedStates.Find(x => x.GetID() == targetId);

				clonedTransition[countT].SetTarget(newTarget);
				clonedTransition[countT].CloneData(transition[countT]);
			}

			clonedStates[count].SetTransitions(clonedTransition);

			clonedStates[count].Init(this.parent);
			clonedStates[count].CloneData(states[count]);
		}

		this.parent.SetStates(clonedStates);
	}
}
