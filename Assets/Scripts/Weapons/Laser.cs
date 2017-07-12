using UnityEngine;
using System.Collections;

public class Laser : Weapon
{
	public override void GenerateBullet ()
	{
		if (isAbleToShoot)
		{
			LaserBullet newBullet = Instantiate (bulletPrefab).GetComponent<LaserBullet>();
			newBullet.transform.right = this.transform.right;
			newBullet.transform.position += newBullet.transform.right * 1.5f;
		}
	}
}

