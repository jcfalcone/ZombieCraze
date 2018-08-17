using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponNameControl : UIElement
{
	[SerializeField]
	[Range(0f,5f)]
	float animationTime;

	[SerializeField]
	CanvasGroup group;

	[SerializeField]
	RectTransform labelTransform;

	[SerializeField]
	TextMeshProUGUI label;
	
	[SerializeField]
	StringScriptable weaponName;

	[SerializeField]
	AnimationCurve AlphaAnimation;

	[SerializeField]
	AnimationCurve PositionXAnimation;

	float currTime;

	// Use this for initialization
	public override void Init () 
	{
		this.currTime = this.animationTime;
	}
	
	// Update is called once per frame
	public override void Tick () 
	{
		if(this.currTime >= this.animationTime)
		{
			return;
		}

		this.currTime += Time.deltaTime;

		Vector2 anchorPos = this.labelTransform.anchoredPosition;
		anchorPos.x = this.PositionXAnimation.Evaluate(this.currTime / this.animationTime);
		
		this.labelTransform.anchoredPosition = anchorPos;
		this.group.alpha = this.AlphaAnimation.Evaluate(this.currTime / this.animationTime);
	}

	public void UpdateUI()
	{
		this.currTime = 0;
		this.label.text = this.weaponName.value;
	}
}
