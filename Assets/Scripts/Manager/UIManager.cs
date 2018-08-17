using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour 
{
	[SerializeField]
	List<UIElement> elements;

	// Use this for initialization
	void Start () 
	{
		for(int count = 0; count < this.elements.Count; count++)
		{
			this.elements[count].Init();
		}	
	}
	
	// Update is called once per frame
	void Update () 
	{
		for(int count = 0; count < this.elements.Count; count++)
		{
			if(this.elements[count].isActiveAndEnabled)
			{
				this.elements[count].Tick();
			}
		}
	}
}
