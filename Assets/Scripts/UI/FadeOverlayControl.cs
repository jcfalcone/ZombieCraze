using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOverlayControl : UIElement 
{

	[SerializeField]
	GameEvent OnFadeEnd;

	[SerializeField]
	CanvasGroup overlay;

	bool OnFadeIn = false;
	
	// Update is called once per frame
	public override void Tick () 
	{
		if(!this.OnFadeIn)
		{
			return;
		}

		this.overlay.alpha += Time.deltaTime;

		if(this.overlay.alpha >= 1)
		{
			this.OnFadeIn = false;
			this.OnFadeEnd.Raise();
		}
	}

	public void UpdateUI()
	{
		this.overlay.alpha = 0f;
		this.OnFadeIn = true;
	}
}
