using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthControl : UIElement
{
	[SerializeField]
	float Speed;

	[SerializeField]
	IntScriptable PrevHealthPlayer;

	[SerializeField]
	IntScriptable HealthPlayer;

	[SerializeField]
	IntScriptable TotalHealthPlayer;

	[SerializeField]
	TextMeshProUGUI healthLabel;

	[SerializeField]
	TextMeshProUGUI totalHealthLabel;
	
	[SerializeField]
	Image healthBar;

	[SerializeField]
	Color FullHealth;

	[SerializeField]
	Color EmptyHealth;

	bool updateUI = false;

	// Use this for initialization
	public override void Init () 
	{
		this.updateUI = false;
	}
	
	// Update is called once per frame
	public override void Tick () 
	{
		if(!this.updateUI)
		{
			return;
		}

		float percHealth = (float)this.HealthPlayer.value / (float)this.TotalHealthPlayer.value;

		this.healthLabel.text = this.HealthPlayer.value.ToString();
		this.healthBar.fillAmount = Mathf.Lerp(this.healthBar.fillAmount, percHealth, Time.deltaTime * this.Speed);
		this.healthBar.color = Color.Lerp(this.EmptyHealth, this.FullHealth, this.healthBar.fillAmount);

		if(this.healthBar.fillAmount == percHealth)
		{
			this.updateUI = false;
		}
	}

	public void UpdateUI()
	{
		this.totalHealthLabel.text = this.TotalHealthPlayer.value.ToString();
		this.updateUI = true;
	}
}
