using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BulletControl : UIElement
{
	[SerializeField]
	IntScriptable BulletsCurrWeapon;

	[SerializeField]
	IntScriptable TotalBulletsCurrWeapon;

	[SerializeField]
	TextMeshProUGUI bulletsLabel;

	[SerializeField]
	TextMeshProUGUI totalBulletsLabel;

	// Use this for initialization
	public override void Init () 
	{
		this.UpdateUI();
	}

	public void UpdateUI()
	{
		this.bulletsLabel.text = this.BulletsCurrWeapon.value.ToString();
		this.totalBulletsLabel.text = this.TotalBulletsCurrWeapon.value.ToString();
	}

}
