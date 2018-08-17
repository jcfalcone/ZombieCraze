using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyControl : UIElement
{
	[SerializeField]
	IntScriptable EnemyCount;

	[SerializeField]
	TextMeshProUGUI enemyCountLabel;


	// Use this for initialization
	public override void Init () 
	{
		this.UpdateUI();
	}

	public void UpdateUI()
	{
		this.enemyCountLabel.text = this.EnemyCount.value.ToString("00");
	}
}
