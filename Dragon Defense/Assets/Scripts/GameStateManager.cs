﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour {

	public delegate void PauseEvent();
	public static event PauseEvent OnPause;

	public GameObject pausePanel;

	#region Event Subscriptions
	void OnEnable()
	{
		PlayerController.OnDestroyPlayer += OnDestroyPlayer;
	}

	void OnDisable()
	{
		PlayerController.OnDestroyPlayer -= OnDestroyPlayer;
	}

	void OnDestroy()
	{
		PlayerController.OnDestroyPlayer -= OnDestroyPlayer;
	}
	#endregion

	void Start(){
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"));
	}

    public void PauseGame(){
		if(OnPause != null){
			OnPause();
			pausePanel.SetActive(!pausePanel.activeSelf); 
		}
	}

	public void OnDestroyPlayer(){
		//Save highscore, save exp (if we have that), save achievements etc etc
	}
}
