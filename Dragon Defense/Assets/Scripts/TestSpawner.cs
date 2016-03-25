﻿using UnityEngine;
using System.Collections;

public class TestSpawner : MonoBehaviour
{
    public float spawnFreq; //used for time between spawns
    public GameObject knight;
    public GameObject ogre;
    public Vector2 spawnPos;
    public int rareEnemyChance;
    private bool pause;
	private bool playerDead;
    private float timer;

    #region Event Subscriptions
    void OnEnable()
    {
        GameStateManager.OnPause += OnPause;
		PlayerController.OnDestroyPlayer += OnDestroyPlayer;
    }
    void OnDisable()
    {
        GameStateManager.OnPause -= OnPause;
		PlayerController.OnDestroyPlayer -= OnDestroyPlayer;
    }
    void OnDestroy()
    {
        GameStateManager.OnPause -= OnPause;
		PlayerController.OnDestroyPlayer -= OnDestroyPlayer;
    }
    #endregion

    // Use this for initialization
    void Start()
    {
        pause = false;
		playerDead = false;
        timer = 0;
        Spawn(knight);
    }

    void Update()
    {
		if (!pause && !playerDead)
        {
            timer += Time.deltaTime;
            if(Random.Range(0, rareEnemyChance) == rareEnemyChance - 1) {
                Spawn(ogre);
            } else {
                Spawn(knight);
            }
        }
    }

    private void Spawn(GameObject enemy)
    {
        if (timer > spawnFreq)
        {
            Instantiate(enemy, spawnPos, Quaternion.identity);
            timer = 0;
        }
    }

	public void OnDestroyPlayer(){
		playerDead = true;
	}

    public void OnPause()
    {
        pause = !pause;
    }
}
