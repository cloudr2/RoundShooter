using UnityEngine;
using System.Collections;

public class Chaingun : Weapon
{
	public override void GenerateBullet ()
	{
		ChaingunBullet newBullet = Instantiate (bulletPrefab).GetComponent<ChaingunBullet>();
		newBullet.gameObject.layer = myLayer;
		newBullet.transform.right = this.transform.right;
		newBullet.transform.position = this.transform.position;
		newBullet.transform.position += newBullet.transform.right;
	}
}

