using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextWaveTimer : UIElement
{
	[SerializeField]
	[Range(0, 10)]
	int cooldownWave;

	[SerializeField]
	string DefaultMSG = "Next Wave in [TIME] Seconds";

	[SerializeField]
	GameEvent OnNextWaveTimerEnd;

	[SerializeField]
	TextMeshProUGUI label;

	float currWait = 1;
	float currCoolDown = 1;

	bool OnTimer = false;

	// Use this for initialization
	public override void Init () 
	{
		this.label.text = this.DefaultMSG.Replace("[TIME]", cooldownWave.ToString());
		this.currWait = 1;

		this.currCoolDown = this.cooldownWave;

		this.OnTimer = true;
	}
	
	// Update is called once per frame
	public override void Tick () 
	{
		if(!this.OnTimer)
		{
			return;
		}

		this.currWait -= Time.deltaTime;

		if(this.currWait <= 0)
		{
			this.currCoolDown -= 1;
			this.label.text = this.DefaultMSG.Replace("[TIME]", this.currCoolDown.ToString());

			if(this.currCoolDown >= 0)
			{
				this.currWait = 1;
			}
			else
			{
				this.OnTimer = false;
				this.OnNextWaveTimerEnd.Raise();
			}
		}
	}
}
