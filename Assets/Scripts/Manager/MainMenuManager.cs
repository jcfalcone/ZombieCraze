using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour 
{
	[SerializeField]
	GameEvent OnGameStart;

	bool isGameStarting = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(this.isGameStarting)
		{
			return;
		}

		if(Input.anyKey)
		{
			this.isGameStarting = true;
			this.OnGameStart.Raise();
		}	
	}

	public void LoadLevel(string _levelName)
	{
		SceneManager.LoadScene(_levelName);
	}
}
