using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct PickableData
{
	public string ID;
	public int Ratio;
}

[CreateAssetMenu(menuName="Item/Pickable List")]
public class PickableItems : ScriptableObject
{
	[SerializeField]
	List<PickableData> Pickable;

	public string GetRandom()
	{
		int randomPerc = Random.Range(0, 1000) % 100;
		List<PickableData> listPick = new List<PickableData>();

		for(int count = 0; count < this.Pickable.Count; count++)
		{
			if(this.Pickable[count].Ratio > randomPerc)
			{
				listPick.Add(this.Pickable[count]);
			}
		}

		if(listPick.Count <= 0)
		{
			return null;
		}

		return listPick[Random.Range(0, 100)%listPick.Count].ID;
	}
}
