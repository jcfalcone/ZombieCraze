using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreControl : UIElement 
{
	[SerializeField]
	FloatScriptable playerScore;

	[SerializeField]
	TextMeshProUGUI label;

	// Use this for initialization
	public override void Init () 
	{
		
	}

	public void UpdateUI()
	{
		this.label.text = Mathf.Round(this.playerScore.value).ToString("0000000000000000");
	}
}
