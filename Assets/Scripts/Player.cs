using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour 
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
	private Rigidbody2D rb;
	private SpriteRenderer myRenderer;
	private bool ableToFire = true;

	void Start () 
	{
		currentHealth = Maxhealth;
		rb = GetComponent<Rigidbody2D>();
		myRenderer = GetComponentInChildren<SpriteRenderer> ();
	}

	void FixedUpdate ()
	{
		AnimateColors ();
		Move ();
		RotateCannon ();
		Shoot ();
	}

	private void Move()
	{
		float horizontalMovement = Input.GetAxis ("Horizontal");
		float VerticalMovement = Input.GetAxis ("Vertical");
		rb.velocity = new Vector2(horizontalMovement,VerticalMovement) * speed;
	}

	private void RotateCannon()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);
		mousePos.z = this.transform.position.z;
		this.transform.right = mousePos - this.transform.position;
	}

	private void Shoot()
	{
		if (Input.GetKey (KeyCode.LeftShift) && ableToFire)
		{
			StartCoroutine (AbleToShoot());
			PlayerBullet newBullet = GameObject.Instantiate (bulletPrefab).GetComponent<PlayerBullet>();
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

	private void AnimateColors()
	{
		Color myAnimatedColor = Color.Lerp(Color.yellow, myColor, Mathf.PingPong(Time.time, 0.5f));
		myRenderer.color = myAnimatedColor;
	}

	private void ReceiveDamage(int damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			StopAllCoroutines ();
			GameManager.instance.EndGame ("LOSE");
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
}
