using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuArmColor : MonoBehaviour 
{
	
	[SerializeField]
	float LerpDuration;

	[SerializeField]
	Material playerMaterial;

	[SerializeField]
	[ColorUsageAttribute(true,true)]
	List<Color> Colors;

	float currColorDuration = 0;

	int currColor;

	int nextColor;


	// Use this for initialization
	void Start () 
	{
		this.currColor = 0;
		this.nextColor = 1;

		if(this.nextColor >= this.Colors.Count)
		{
			this.nextColor = 0;
		}	
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.currColorDuration += Time.deltaTime;

		float perc = this.currColorDuration / this.LerpDuration;

		this.playerMaterial.SetColor("_ArmColor", Color.Lerp(this.Colors[this.currColor], this.Colors[this.nextColor], perc));

		if(perc >= 1)
		{
			this.currColorDuration = 0;
			this.currColor = this.nextColor;
			this.nextColor++;

			if(this.nextColor >= this.Colors.Count)
			{
				this.nextColor = 0;
			}	
		}
	}
}
