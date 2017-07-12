using UnityEngine;
using System.Collections;

public class LaserBullet : Bullet
{
	[SerializeField]
	private float damageScalation = 1.2f;
	private float newScalation = 0f;
	private float scalationRate = 0.5f;
	private LineRenderer lr;

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Player")
		{
			ResetScalation ();
			InvokeRepeating ("IncreaseScalation", 0f, scalationRate);
		}
	}

	void OnCollisionExit2D(Collision2D col)
	{
		if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Player")
		{
			CancelInvoke ("IncreaseScalation");
		}
	}

	void OnCollisionStay2D(Collision2D col)
	{
		if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Player") 
		{
			col.gameObject.SendMessage ("ReceiveDamage", LaserDamage());
			DestroyBullet ();
		}
	}

	private void IncreaseScalation ()
	{
		newScalation += damageScalation;
	}

	private void ResetScalation ()
	{
		newScalation = 0f;
	}

	private int LaserDamage()
	{
		print (damage * newScalation);
		int myDamage = Mathf.FloorToInt(damage * newScalation);
		print (myDamage);
		return myDamage;
	}
}
