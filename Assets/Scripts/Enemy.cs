using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]

public abstract class Enemy : MonoBehaviour
{
	[Header ("Stats")]

	[SerializeField]
	protected int Maxhealth;
	[SerializeField]
	protected int currentHealth;
	[SerializeField]
	protected float speed;

	[Header ("Weapons")]

	[SerializeField]
	protected Weapon myWeapon;

	[Header ("Misc")]
	protected bool isDestroyedByPlayer = false;
	public bool isTargetInRange = false;

	protected Rigidbody2D rb;
	protected Player player;

	public GameObject pointLabel;

	protected virtual void Start()
	{
		currentHealth 	= Maxhealth;
		rb 				= GetComponent<Rigidbody2D> ();
		player 			= FindObjectOfType<Player> ();
	}

	protected virtual void FixedUpdate()
	{
		MoveTowardsPlayer ();

		if (isTargetInRange)
		{
			myWeapon.Shoot ();
		}
	}

	protected virtual void MoveTowardsPlayer ()
	{
		if (player)
		{
			Vector3 direction		= player.transform.position - this.transform.position;
			this.transform.right 	= direction;
			rb.velocity 			= this.transform.right * speed;
		}
	}

	protected virtual void ReceiveDamage(int damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			Destroy (this.gameObject);
			isDestroyedByPlayer = true;
		}
		else if (currentHealth / Maxhealth <= 0.25f)
		{
			//change color low health
		}	
		else if (currentHealth / Maxhealth <= 0.5f)
		{
			//change color critical health
		}	
	}

	void OnDestroy()
	{
		DestroyEnemy ();
	}

	protected virtual void DestroyEnemy()
	{
		StopAllCoroutines ();
		if (isDestroyedByPlayer) 
		{
			SpawnManager.instance.UpdateRemainingEnemies ();
			GameObject.Instantiate (pointLabel,new Vector3 (this.transform.position.x, this.transform.position.y + 1, 0),Quaternion.Euler(Vector3.zero));
			GameManager.instance.AddScore (SpawnManager.instance.GetEnemyScorePerKill());
			GameManager.instance.CheckEnemiesAlive ();
		}
	}
}
