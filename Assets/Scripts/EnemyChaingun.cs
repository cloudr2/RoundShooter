using UnityEngine;
using System.Collections;

public class EnemyChaingun : Enemy
{
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
		}
	}
}

