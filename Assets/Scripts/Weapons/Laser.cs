using UnityEngine;
using System.Collections;

public class Laser : Weapon
{
	private EnemyLaser myOwner;

	void Awake()
	{
		myOwner = GetComponentInParent<EnemyLaser>();;
	}

	public override void GenerateBullet ()
	{
		LaserBullet newBullet 		= Instantiate (bulletPrefab).GetComponent<LaserBullet>();
		newBullet.gameObject.layer 	= this.myLayer;
		newBullet.myOwner 			= myOwner;
	}

	public override void Shoot ()
	{
		if (isAbleToShoot)
		{
			this.GenerateBullet ();
			isAbleToShoot = false;
		}
	}
}

