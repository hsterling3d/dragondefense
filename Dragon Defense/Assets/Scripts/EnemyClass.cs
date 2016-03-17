﻿using UnityEngine;
using System.Collections;

public class EnemyClass : MonoBehaviour {

    public float health;
    public float damage; //Currently unused
	public Vector2 velocity;

    protected bool paused;
    protected bool wasPaused;

	#region Event Subscriptions
	void OnEnable(){
		GameStateManager.OnPause += OnPause;
		ShotClass.OnDamage += OnDamage;
	}
	void OnDisable(){
		GameStateManager.OnPause -= OnPause;
		ShotClass.OnDamage -= OnDamage;
	}
	void OnDestroy(){
		GameStateManager.OnPause -= OnPause;
		ShotClass.OnDamage -= OnDamage;
	}
	#endregion



    public void DeleteObject() //Used for deleting the object upon death
    {
        Destroy(this.gameObject);
    }

    public void OnPause() //Currently nonfunctional for enemies
    {
        paused = !paused;
        wasPaused = true;
    }

	public void OnDamage(float damage, GameObject col) //Used for taking damage
    {
		if(col == this.gameObject){
			health -= damage;
		}
    }

}