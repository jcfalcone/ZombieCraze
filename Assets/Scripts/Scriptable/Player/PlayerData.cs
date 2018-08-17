using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Player/Data")]
public class PlayerData : ScriptableObject
{
	public Vector3 AxisMovement;
	public Vector3 mousePosition;
	public bool fire;
	public bool reload;
}
