using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public int Maxhealth;
	public int currentHealth;
	public float minSpeed;
	public float maxSpeed;
	public float fireRate;

	public GameObject bulletPrefab;
	public GameObject pointLabel;
	public Transform bulletSpawner;

	public Color colorFullHP;
	public Color colorHalfHP;
	public Color colorLowHP;

	private Color myColor;
	private float lerpSpeed = 1f;
	private float speed;
	private bool ableToFire = false;
	private bool destroyedByPlayer = false;
	private Rigidbody2D rb;
	private Player player;
	private SpriteRenderer myRenderer;

	//make trigger so the enemies start shooting only when the player is in sight.

	void Start()
	{
		currentHealth = Random.Range(1,Maxhealth + 1);
		speed = Random.Range (minSpeed, maxSpeed);
		myColor = colorFullHP;
		rb = GetComponent<Rigidbody2D> ();
		player = FindObjectOfType<Player> ();
		myRenderer = GetComponentInChildren<SpriteRenderer> ();
	}

	void FixedUpdate()
	{
		AnimateColors ();
		MoveTowardsPlayer ();
		Shoot ();
	}

	private void MoveTowardsPlayer ()
	{
		if (player)
		{
			Vector3 direction = player.transform.position - this.transform.position;
			this.transform.right = direction;
			rb.velocity = this.transform.right * speed;
		}
	}

	private void OnTriggerEnter2D(Collider2D trigger)
	{
		if (trigger.gameObject.tag == "Player")
		{
			ableToFire = true;
		}
	}

	private void OnTriggerExit2D(Collider2D trigger)
	{
		if (trigger.gameObject.tag == "Player")
		{
			ableToFire = false;
			StopAllCoroutines ();
		}
	}

	private void AnimateColors()
	{
		Color myAnimatedColor = Color.Lerp(Color.magenta, myColor, Mathf.PingPong(Time.time,lerpSpeed));
		myRenderer.color = myAnimatedColor;
	}
		
	private void Shoot()
	{
		if (ableToFire)
		{
			StartCoroutine (AbleToShoot());
			EnemyBullet newBullet = GameObject.Instantiate (bulletPrefab).GetComponent<EnemyBullet>();
			newBullet.transform.right = this.transform.right;
			newBullet.transform.position = bulletSpawner.position;
		}
	}

	private IEnumerator AbleToShoot()
	{
		ableToFire = false;
		yield return new WaitForSeconds (fireRate);
		ableToFire = true;
	}

	private void ReceiveDamage(int damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			Destroy (this.gameObject);
			destroyedByPlayer = true;
		}
		else if (currentHealth / Maxhealth <= 0.25f)
		{
			myColor = colorLowHP;
			lerpSpeed = 0.25f;
		}	
		else if (currentHealth / Maxhealth <= 0.5f)
		{
			myColor = colorHalfHP;
			lerpSpeed = 0.5f;
		}	
	}

	void OnDestroy()
	{
		StopAllCoroutines ();
		if (destroyedByPlayer) 
		{
			SpawnManager.instance.ReduceRemainingEnemies ();
			GameObject.Instantiate (pointLabel,new Vector3 (this.transform.position.x, this.transform.position.y + 1, 0),Quaternion.Euler(Vector3.zero));
			GameManager.instance.AddScore (SpawnManager.instance.GetEnemyScorePerKill());
			GameManager.instance.CheckEnemiesAlive ();
		}
	}
}
