using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBase : MonoBehaviour 
{
	[SerializeField]
	string ItemId;

	[SerializeField]
	Vector3 itemLocalPosition;

	[SerializeField]
	float SpawnCooldown;

	[SerializeField]
	Image progress;

	ItemTemplate itemController;

	float SpawnCurrCooldown;
	

	// Use this for initialization
	void Start () 
	{
		this.CreateItem();
		this.progress.fillAmount = 1;
	}

	void Update()
	{
		if(this.progress.fillAmount >= 1)
		{
			return;
		}

		this.SpawnCurrCooldown -= Time.deltaTime;
		
		this.progress.fillAmount = 1f - this.SpawnCurrCooldown / this.SpawnCooldown;

		if(this.progress.fillAmount == 1f)
		{
			this.CreateItem();
		}
	}

	public void Picked()
	{
		this.progress.fillAmount = 0;

		this.SpawnCurrCooldown = this.SpawnCooldown;
	}

	public void CreateItem()
	{
		GameObject item = ObjectManager.instance.Instantiate(ItemId, transform.position, Quaternion.identity);

		ItemTemplate controller = item.GetComponent<ItemTemplate>();

		if(controller != null)
		{
			this.itemController = controller;
			item.transform.parent = transform;
			item.transform.localPosition = itemLocalPosition;

			this.itemController.Init(this);
		}
	}
}
