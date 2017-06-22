using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float speed;
	private Rigidbody2D rb;

	void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
		this.transform.parent = Game.instance.bulletHolder.transform;
	}

	void FixedUpdate ()
	{
		MoveBullet ();
		Destroy (this.gameObject, 2);
	}

	private void MoveBullet()
	{
		rb.velocity = this.transform.right * speed;
	}
}
