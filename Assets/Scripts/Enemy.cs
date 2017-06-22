using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float Maxhealth;
	public float speed;
	public float fireRate;

	public GameObject bulletPrefab;
	public Transform bulletSpawner;

	public Color32 colorFullHP;
	public Color32 colorHalfHP;
	public Color32 colorLowHP;

	private Color32 myColor;
	private float currentHealth;
	private bool ableToFire = true;
	private Rigidbody2D rb;
	private Player player;
	private SpriteRenderer myRenderer;

	//make trigger so the enemies start shooting only when the player is in sight.

	void Start()
	{
		currentHealth = Maxhealth;
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

	private void AnimateColors()
	{
		Color myAnimatedColor = Color.Lerp(Color.yellow, myColor, Mathf.PingPong(Time.time, 0.5f));
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
		}
		else if (currentHealth / Maxhealth < 0.35)
		{
			myColor = colorLowHP;
		}	
		else if (currentHealth / Maxhealth < 0.5)
		{
			myColor = colorHalfHP;
		}	
	}

	void OnDestroy()
	{
		StopAllCoroutines ();
		if (player) 
		{
			GameManager.instance.AddScore (SpawnManager.instance.GetEnemyScorePerKill());
			GameManager.instance.CheckEnemiesAlive ();
		}
	}
}
