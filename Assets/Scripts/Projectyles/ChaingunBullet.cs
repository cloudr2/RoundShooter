using UnityEngine;
using System.Collections;

public class ChaingunBullet : Bullet
{
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Player") 
		{
			col.gameObject.SendMessage ("ReceiveDamage", damage);
			DestroyBullet ();
		}

		if (col.gameObject.tag == "boundary") 
		{
			DestroyBullet ();
		}
	}
}

