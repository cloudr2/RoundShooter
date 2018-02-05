using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class ChaingunBullet : MonoBehaviour {
	public float speed;
	public float lifeSpan;
	public float damage { get; set;}

	private Rigidbody2D rb;
	private Transform bulletHolder;

	private void Start () {
		rb = GetComponent<Rigidbody2D>();
		bulletHolder = GameObject.FindGameObjectWithTag("BulletHolder").transform;
		transform.parent = bulletHolder;
		Destroy (this.gameObject, lifeSpan);
	}

	private void Update () {
		rb.velocity = transform.right * speed;
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Enemy" && gameObject.tag == "PlayerBullet" || col.gameObject.tag == "Player" && gameObject.tag == "EnemyBullet") {
			col.gameObject.SendMessage ("ReceiveDamage",damage);
			Destroy (gameObject);
		}

		if (col.gameObject.tag == "boundary") {
			Destroy (gameObject);
		}
	}
}
