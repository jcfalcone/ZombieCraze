using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour 
{	
	[SerializeField]
	PlayerData data;

	Camera mainCamera;

	public void Init () 
	{
		this.mainCamera = Camera.main;
	}
	// Update is called once per frame
	public void LateTick () 
	{
		Vector3 movementInput = this.data.AxisMovement;

		movementInput.x = Input.GetAxis("Horizontal");
		movementInput.z = Input.GetAxis("Vertical");

		this.data.AxisMovement = movementInput;
		this.data.fire = Input.GetButton("Fire2");
		this.data.reload = Input.GetKey(KeyCode.R);

		this.data.mousePosition = this.mainCamera.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * this.mainCamera.transform.position.y);
		this.data.mousePosition.y = transform.position.y;
	}

	public void GetMouseRotation()
	{

	}
}
