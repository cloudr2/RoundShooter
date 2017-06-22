using System.Collections;
using UnityEngine;

public class EnemyBullet : Bullet {

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			col.gameObject.SendMessage ("ReceiveDamage", 1);
			Destroy (this.gameObject);
		}

		if (col.gameObject.tag == "boundary") 
		{
			Destroy (this.gameObject);
		}
	}
}
