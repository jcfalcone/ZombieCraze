using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
	[SerializeField]
	float speed;

	[SerializeField]
	Transform target;

	[SerializeField]
	Vector3 minBorder = Vector3.zero;

	[SerializeField]
	Vector3 maxBorder = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 targetPosition = this.target.position;

		targetPosition.y = transform.position.y;

		transform.position = Vector3.Lerp(transform.position, targetPosition, this.speed * Time.deltaTime);

		Vector3 position = transform.position;

		position.x = Mathf.Clamp(position.x, this.minBorder.x, this.maxBorder.x);
		position.z = Mathf.Clamp(position.z, this.minBorder.z, this.maxBorder.z);

		transform.position = position;
	}
}
