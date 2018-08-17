using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour 
{
	[SerializeField]
	[Range(0f, 100f)]
	float speed;

	Vector3 vectorUp = Vector3.up;
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(this.vectorUp * this.speed * Time.deltaTime);
	}
}
