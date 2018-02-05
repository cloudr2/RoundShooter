using UnityEngine;
using System.Collections;

public class ChaingunEnemyBrain : MonoBehaviour {
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
			if (!isAbleToShoot) {
				direction = Vector3.zero;
			}
			owner.Move (direction);
		}
	}

	private void Shoot() {
		if (isTargetInRange && isAbleToShoot) {
			ChaingunBullet newBullet = ((GameObject)Instantiate (owner.bulletPrefab)).GetComponent<ChaingunBullet>();
			newBullet.gameObject.layer = 11;
			newBullet.gameObject.tag = gameObject.tag + "Bullet";
			newBullet.transform.right = transform.right;
			newBullet.transform.position = owner.aim.position;
			newBullet.transform.position += newBullet.transform.right;
			newBullet.damage = owner.damage;
			isAbleToShoot = false;
			StartCoroutine (ShootCooldown());
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

