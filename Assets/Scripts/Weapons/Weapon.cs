﻿using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
	[SerializeField]
	protected GameObject bulletPrefab;
	[SerializeField]
	protected float coolDown = 0.3f;
	protected bool isAbleToShoot = true;
	protected int myLayer {get {return this.gameObject.layer;}}

	public virtual void Shoot ()
	{
		if (isAbleToShoot)
		{
			isAbleToShoot = false;
			StartCoroutine (WeaponCooldown ());
			GenerateBullet ();
		}
	}

	public abstract void GenerateBullet ();

	protected IEnumerator WeaponCooldown()
	{
		yield return new WaitForSeconds (coolDown);
		isAbleToShoot = true;
	}
}