using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerControl : UIElement
{
	[SerializeField]
	IntScriptable currTime;

	[SerializeField]
	FloatScriptable playerScore;

	[SerializeField]
	GameEvent OnScoreChange;

	[SerializeField]
	int ScoreOverTime;

	[SerializeField]
	TextMeshProUGUI timer;

	float currWait = 1;

	// Use this for initialization
	public override void Init () 
	{
		this.timer.text = "00:00";
		this.currWait = 1;
		this.currTime.value = 0;
	}
	
	// Update is called once per frame
	public override void Tick () 
	{
		this.currWait -= Time.deltaTime;

		if(this.currWait <= 0)
		{
			this.currTime.value += 1;
			string minutes = Mathf.Floor(this.currTime.value / 60).ToString("00");
 			string seconds = (this.currTime.value  % 60).ToString("00");

			 this.timer.text = minutes+":"+seconds;

			 this.currWait = 1;
		}

		this.playerScore.value += this.ScoreOverTime * Time.deltaTime;
		this.OnScoreChange.Raise();
	}
}
