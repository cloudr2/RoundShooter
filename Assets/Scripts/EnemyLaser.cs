using UnityEngine;
using System.Collections;

public class EnemyLaser : Enemy
{
	public Vector3 targetPosition { get { return player.transform.position; } }

	protected void OnTriggerEnter2D(Collider2D trigger)
	{
		if (trigger.gameObject.tag == "Player")
		{
			isTargetInRange = true;
		}
	}

	protected void OnTriggerExit2D(Collider2D trigger)
	{
		if (trigger.gameObject.tag == "Player")
		{
			isTargetInRange = false;
			myWeapon.isAbleToShoot = true;
		}
	}
}

