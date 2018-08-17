using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PrefabsData
{
	public string Id;
	public GameObject Prefab;
	public int maxAmount;
	public int currAmount;

	[HideInInspector]
	public List<GameObject> instances;

	public bool PreWarm;
}
public class ObjectManager : Singleton<ObjectManager> 
{
	[SerializeField]
	List<PrefabsData> prefabs;

	Dictionary<int, int> prefabsIndex;
	Vector3 startPos = Vector3.one * -1000;


	// Use this for initialization
	void Awake () 
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}	
		else
		{
			Destroy(gameObject);
		}

		this.PreWarm();
	}

	public void PreWarm()
	{
		this.prefabsIndex = new Dictionary<int, int>();

		for(int count = 0; count < this.prefabs.Count; count++)
		{
			if(this.prefabs[count].PreWarm)
			{
				for(int countPrefab = 0; countPrefab < this.prefabs[count].maxAmount; countPrefab++)
				{
					GameObject instance = this.CreateGameObject(this.prefabs[count].Prefab, this.startPos, Quaternion.identity);
					instance.name = this.prefabs[count].Id+"_"+countPrefab;

					this.prefabs[count].instances.Add(instance);

					CloneTemplate cloner = instance.GetComponent<CloneTemplate>();

					if(cloner != null)
					{
						cloner.CloneData();
					}
				}
			}

			this.prefabsIndex[this.prefabs[count].Id.GetHashCode()] = count;
		}
	}
	
	public GameObject Instantiate(string _id, Vector3 _position, Quaternion _rotation)
	{
		int hash = _id.GetHashCode();

		if(!this.prefabsIndex.ContainsKey(hash))
		{
			return null;
		}

		PrefabsData data = this.prefabs[this.prefabsIndex[hash]];
		GameObject instance;

		if(data.instances.Count <= data.currAmount)
		{
			instance = this.CreateGameObject(data.Prefab, _position, _rotation);
			data.instances.Add(instance);
		}
		else
		{
			instance = data.instances[data.currAmount];
			instance.transform.position = _position;
			instance.transform.rotation = _rotation;
		}
		
		instance.transform.parent = null;
		instance.SetActive(true);

		data.currAmount++;

		if(data.currAmount > data.maxAmount)
		{
			data.currAmount = 0;
		}

		this.prefabs[this.prefabsIndex[hash]] = data;

		return instance;
	}

	GameObject CreateGameObject(GameObject prefab, Vector3 _position, Quaternion _rotation)
	{
		GameObject instance = Instantiate(prefab, _position, _rotation);
		instance.transform.parent = transform;
		instance.SetActive(false);

		return instance;
	}

	public void Destroy(GameObject _obj)
	{
		_obj.transform.position = this.startPos;
		_obj.transform.rotation = Quaternion.identity;
		_obj.SetActive(false);

		_obj.transform.parent = transform;
	}
}
