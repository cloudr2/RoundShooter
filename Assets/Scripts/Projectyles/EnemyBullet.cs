using System.Collections;
using UnityEngine;

public class EnemyBullet : Bullet {

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			print (col.gameObject.name + " " + col.gameObject.tag);
			col.gameObject.SendMessage ("ReceiveDamage", 1);
			Destroy (this.gameObject);
		}

		if (col.gameObject.tag == "boundary") 
		{
			Destroy (this.gameObject);
		}
	}
}
