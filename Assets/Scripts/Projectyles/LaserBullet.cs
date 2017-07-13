using UnityEngine;
using System.Collections;

public class LaserBullet : Bullet
{
	private LineRenderer lr;
	public EnemyLaser myOwner { get; set;}

	protected override void Start () 
	{
		base.Start ();
		lr = GetComponent<LineRenderer> ();
	}

	protected override void FixedUpdate () 
	{
		lr.SetPosition (0,myOwner.transform.position);
		lr.SetPosition (1,myOwner.targetPosition);

		if (myOwner.isTargetInRange == false)
		{
			DestroyBullet ();	
		}
	}

//	[SerializeField]
//	private float damageScalation = 1.2f;
//	private float newScalation = 0f;
//	private float scalationRate = 0.5f;
//	private LineRenderer lr;
//
//	void OnCollisionEnter2D(Collision2D col)
//	{
//		if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Player")
//		{
//			ResetScalation ();
//			InvokeRepeating ("IncreaseScalation", 0f, scalationRate);
//		}
//	}
//
//	void OnCollisionExit2D(Collision2D col)
//	{
//		if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Player")
//		{
//			CancelInvoke ("IncreaseScalation");
//		}
//	}
//
//	void OnCollisionStay2D(Collision2D col)
//	{
//		if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Player") 
//		{
//			col.gameObject.SendMessage ("ReceiveDamage", 10);
//			DestroyBullet ();
//		}
//	}
//
//	private void IncreaseScalation ()
//	{
//		newScalation += damageScalation;
//	}
//
//	private void ResetScalation ()
//	{
//		newScalation = 0f;
//	}
//
//	private int LaserDamage()
//	{
//		print (bulletDamage * newScalation);
//		int myDamage = Mathf.FloorToInt(bulletDamage * newScalation);
//		print (myDamage);
//		return myDamage;
//	}
}
