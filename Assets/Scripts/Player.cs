using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour 
{
	public int Maxhealth;
	public int currentHealth;
	public float speed;
	public float fireRate;
	public GameObject bulletPrefab;
	public TextMesh healthLabel;
	public Transform bulletSpawner;

	public Color32 colorFullHP;
	public Color32 colorHalfHP;
	public Color32 colorLowHP;

	public bool isMoving = false;

	private Color32 myColor;
	private float lerpSpeed = 1f;
	private Rigidbody2D rb;
	private SpriteRenderer myRenderer;
	private bool ableToFire = true;

	void Start () 
	{
		myColor = colorFullHP;
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
		isMoving = (rb.velocity != Vector2.zero);
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
			newBullet.transform.position += newBullet.transform.right * 1.5f;
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
		Color myAnimatedColor = Color.Lerp(Color.cyan, myColor, Mathf.PingPong(Time.time, lerpSpeed));
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

		healthLabel.text = currentHealth.ToString ();
		healthLabel.gameObject.SetActive (true);

		if (IsInvoking("HideHPLabel"))
		{
			CancelInvoke ();
		}
		Invoke ("HideHPLabel", 2f);
	}

	private void HideHPLabel()
	{
		healthLabel.gameObject.SetActive (false);
	}
}
