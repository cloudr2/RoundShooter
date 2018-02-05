using UnityEngine;
using System.Collections;

public class ShotgunEnemyBrain : MonoBehaviour
{
	private Ship owner;
	private PlayerBrain player;
	private bool isTargetInRange = false;
	private bool isAbleToShoot = true;

	void Start () {
		owner = GetComponent<Ship> ();
		player = FindObjectOfType<PlayerBrain> ();
	}

	void Update () {
		MoveTowardsPlayer ();
		Shoot ();
	}

	private void MoveTowardsPlayer () {
		if (player) {
			Vector3 direction = player.transform.position - transform.position;
			direction = direction.normalized;
			transform.right = direction;
			owner.Move (direction);
		}
	}

	private void Shoot() {
		if (isTargetInRange && isAbleToShoot) {
			if (owner.CurrentHealth * 3 <= owner.Maxhealth) {
				GenerateScatterShot (5);
				owner.fireRate = 0.3f;
			} else if (owner.CurrentHealth * 1.5 <= owner.Maxhealth) {
				GenerateScatterShot (4);
				owner.fireRate = 0.4f;
			} else {
				GenerateScatterShot (3);
			}
			isAbleToShoot = false;
			StartCoroutine (ShootCooldown());
		}
	}

	private void GenerateScatterShot(int shootAmount) {
		float offset = 1f / shootAmount;
		float angle = offset * (shootAmount - 1) / 2;
		for (int i = 0; i < shootAmount; i++) {
			ChaingunBullet newBullet = ((GameObject)Instantiate (owner.bulletPrefab)).GetComponent<ChaingunBullet> ();
			newBullet.gameObject.layer = 11;
			newBullet.gameObject.tag = gameObject.tag + "Bullet";
			newBullet.transform.right = new Vector3 (transform.right.x * Mathf.Cos (angle) - transform.right.y * Mathf.Sin (angle), transform.right.x * Mathf.Sin (angle) + transform.right.y * Mathf.Cos (angle), transform.right.z);
			newBullet.transform.position = owner.aim.position;
			newBullet.transform.position += newBullet.transform.right;
			newBullet.damage = owner.damage;
			angle -= offset;
		}
	}
		
	private void OnTriggerEnter2D(Collider2D trigger) {
		if (trigger.gameObject.tag == "Player") {
			isTargetInRange = true;
		}
	}

	private void OnTriggerExit2D(Collider2D trigger) {
		if (trigger.gameObject.tag == "Player") {
			isTargetInRange = false;
		}
	}

	private IEnumerator ShootCooldown() {
		yield return new WaitForSeconds (owner.fireRate);
		isAbleToShoot = true;
	}
}

