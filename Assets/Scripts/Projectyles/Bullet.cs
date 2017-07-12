using System.Collections;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
	[SerializeField]
	protected float speed = 10f;
	[SerializeField]
	protected float lifeSpan = 2f;
	[SerializeField]
	protected int damage = 10;

	protected Rigidbody2D rb;

	public int Damage {get { return damage; } }

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
}
