using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour 
{
	[SerializeField]
	[Range(0f, 2f)]
	float SpawnInterval;
	
	[SerializeField]
	[Range(0, 100)]
	int maxEnemys;
	
	[SerializeField]
	[Range(0, 100)]
	int maxWaves;

	[SerializeField]
	AnimationCurve spawnCurve;

	[SerializeField]
	Vector3 minSpawnPosition;

	[SerializeField]
	Vector3 maxSpawnPosition;

	[SerializeField]
	IntScriptable enemyCount;
	
	[SerializeField]
	GameEvent OnEnemyCountChange;
	
	[SerializeField]
	GameEvent OnWaveStart;
	
	[SerializeField]
	GameEvent OnWaveEnd;

	bool IsSpawning = false;

	WaitForSeconds spawnWait;

	Vector3 vectorZero = Vector3.zero;

	int currWave;

	public void Start()
	{
		this.spawnWait = new WaitForSeconds(this.SpawnInterval);
		this.enemyCount.value = 0;
		this.currWave = 0;
		this.OnWaveEnd.Raise();	
	}

	// Use this for initialization
	void StartWave () 
	{
		this.OnWaveStart.Raise();	
	}
	
	public void EnemyCreated()
	{
		this.enemyCount.value++;
		this.OnEnemyCountChange.Raise();
	}
	
	public void EnemyDied()
	{
		this.enemyCount.value--;

		this.enemyCount.value = Mathf.Clamp(this.enemyCount.value, 0, this.maxEnemys);

		this.OnEnemyCountChange.Raise();

		if(this.enemyCount.value == 0 && !this.IsSpawning)
		{
			this.OnWaveEnd.Raise();	
		}
	}

	public void StartSpawn()
	{
		this.IsSpawning = true;

		StartCoroutine(this.SpawnEnemys());
	}

	IEnumerator SpawnEnemys()
	{
		int amountToSpawn = Mathf.RoundToInt((float)this.maxEnemys * this.spawnCurve.Evaluate(Mathf.Clamp01((float)this.currWave / (float)this.maxWaves)));

		Debug.Log(this.spawnCurve.Evaluate(Mathf.Clamp01(((float)this.currWave / (float)this.maxWaves))));

		for(int count = 0; count < amountToSpawn; count++)
		{
			Vector3 spawnPoint = vectorZero;

			spawnPoint.x = Random.Range(this.minSpawnPosition.x, this.maxSpawnPosition.x);
			spawnPoint.z = Random.Range(this.minSpawnPosition.z, this.maxSpawnPosition.z);

			GameObject instance = ObjectManager.instance.Instantiate("Enemy1", spawnPoint, Quaternion.identity);

			instance.transform.parent = transform;

			EnemyTemplate controller = instance.GetComponent<EnemyTemplate>();

			if(controller != null)
			{
				controller.Init();
			}

			yield return this.spawnWait;
		}

		this.IsSpawning = false;
		this.currWave++;
	}
}
