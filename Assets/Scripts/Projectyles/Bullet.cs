using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public abstract class Bullet : MonoBehaviour
{
	[SerializeField]
	protected float speed = 10f;
	[SerializeField]
	protected float lifeSpan = 2f;

	protected int bulletDamage;

	protected Rigidbody2D rb;

	protected virtual void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
		this.transform.parent = Game.instance.bulletHolder.transform;
	}

	protected virtual void FixedUpdate ()
	{
		MoveBullet ();
		DestroyBullet (lifeSpan);
	}

	protected virtual void MoveBullet()
	{
		rb.velocity = this.transform.right * speed;
	}

	protected virtual void DestroyBullet(float time = 0f)
	{
		Destroy (this.gameObject, time);
	}		

	public void SetDamage(int damage)
	{
		bulletDamage = damage;
	}
}
