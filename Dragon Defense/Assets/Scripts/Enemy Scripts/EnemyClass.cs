﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyClass : MonoBehaviour {

    public delegate void DestroyEvent(float points);
    public static event DestroyEvent OnDestroyEnemy;

	[Header("Health Bar")]
    [SerializeField] protected float health;
	protected float startHealth;
	public Slider healthBar;
	public Gradient healthColorGradient;
	public Image healthBarColor;
	private float healthPercent;
	[Header("")]
    public float damage;
    public float moveSpeed;
	protected float startMoveSpeed;
	public Vector2 velocity;
    public float points;
    public bool isMelee;

    protected bool paused;
    protected bool wasPaused;
	protected Vector2 startSpawn;

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


    public void OnPause() 
    {
        paused = !paused;
        wasPaused = true;
    }

	public void OnDamage(float damageDealt, GameObject col, int weaponNumber, Vector3 shotPosition) //Used for taking damage
    {
		if(col == gameObject){
			health -= damageDealt;
			SpawnDamageText(damageDealt, col, weaponNumber, shotPosition);
			UpdateHealthBar();
		}
		if(health <= 0) {
            if(OnDestroyEnemy != null)
                OnDestroyEnemy(points);
			SpawnPointText(points);
			DespawnThis();
		}
    }

	void OnTriggerEnter2D(Collider2D col){
		if(col.name.Contains("Despawn")){
			TestSpawner.pop--;
			DespawnThis();
		}
	}

	private void DespawnThis(){
		ResetValues();
		UpdateHealthBar();
		Pooling.Despawn(gameObject);
	}

	public void ResetValues(){
		health = startHealth;
		transform.position = startSpawn;
	}

	protected void UpdateHealthBar(){
		healthBar.value = health;
		healthPercent = health/healthBar.maxValue;
		healthBarColor.color = healthColorGradient.Evaluate(healthPercent);
	}

	private void SpawnPointText(float pointValue){
		Vector2 pointTextSpawn = transform.position;
		pointTextSpawn.y += GetComponent<SpriteRenderer>().sprite.bounds.size.y/2;
		pointTextSpawn = Camera.main.WorldToScreenPoint(pointTextSpawn);
		GameObject clone = (GameObject)Instantiate(Resources.Load("Action Texts/PointText"), pointTextSpawn, Quaternion.identity);
		clone.transform.SetParent(GameObject.FindGameObjectWithTag("ScreenSpaceCanvas").transform);
		clone.transform.SetAsFirstSibling();
		clone.GetComponent<PointTextMove>().pointDisplay = pointValue;
	}

	private void SpawnDamageText(float damageDealt, GameObject col, int weaponNumber, Vector3 shotPosition){
		Vector2 damageTextSpawn = shotPosition;
		damageTextSpawn = Camera.main.WorldToScreenPoint(damageTextSpawn);
		GameObject clone = (GameObject)Instantiate(Resources.Load("Action Texts/DamageText"), damageTextSpawn, Quaternion.identity);
		clone.transform.SetParent(GameObject.FindGameObjectWithTag("ScreenSpaceCanvas").transform); 
		clone.transform.SetAsFirstSibling();
		clone.GetComponent<DamageTextMove>().colorNumber = weaponNumber;
		clone.GetComponent<DamageTextMove>().damageDealt = damageDealt;
	}

}
