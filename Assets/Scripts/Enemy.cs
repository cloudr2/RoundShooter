using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[Header ("Stats")]
	public int Maxhealth;
	public int currentHealth;
	public float speed;

	[Header ("Colors")]
	public Color colorFullHP;
	public Color colorHalfHP;
	public Color colorLowHP;

	[Header ("Weapons")]
	public Weapon myWeapon;

	[Header ("Misc")]
	public GameObject pointLabel;

	private float lerpSpeed = 1f;
	private bool isDestroyedByPlayer = false;
	private bool isTargetInRange = false;

	private Color myColor;
	private Rigidbody2D rb;
	private Player player;
	private SpriteRenderer myRenderer;

	void Start()
	{
		currentHealth 	= Random.Range(1,Maxhealth + 1) * 10;
		myColor 		= colorFullHP;
		rb 				= GetComponent<Rigidbody2D> ();
		player 			= FindObjectOfType<Player> ();
		myRenderer 		= GetComponentInChildren<SpriteRenderer> ();
	}

	void FixedUpdate()
	{
		AnimateColors ();
		MoveTowardsPlayer ();

		if (isTargetInRange)
		{
			myWeapon.Shoot ();
		}
	}

	private void MoveTowardsPlayer ()
	{
		if (player)
		{
			Vector3 direction		= player.transform.position - this.transform.position;
			this.transform.right 	= direction;
			rb.velocity 			= this.transform.right * speed;
		}
	}

	private void OnTriggerEnter2D(Collider2D trigger)
	{
		if (trigger.gameObject.tag == "Player")
		{
			isTargetInRange = true;
		}
	}

	private void OnTriggerExit2D(Collider2D trigger)
	{
		if (trigger.gameObject.tag == "Player")
		{
			isTargetInRange = false;
		}
	}

	private void AnimateColors()
	{
		Color myAnimatedColor 	= Color.Lerp(Color.magenta, myColor, Mathf.PingPong(Time.time,lerpSpeed));
		myRenderer.color 		= myAnimatedColor;
	}

	private void ReceiveDamage(int damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			Destroy (this.gameObject);
			isDestroyedByPlayer = true;
		}
		else if (currentHealth / Maxhealth <= 0.25f)
		{
			myColor 	= colorLowHP;
			lerpSpeed 	= 0.25f;
		}	
		else if (currentHealth / Maxhealth <= 0.5f)
		{
			myColor 	= colorHalfHP;
			lerpSpeed 	= 0.5f;
		}	
	}

	void OnDestroy()
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
