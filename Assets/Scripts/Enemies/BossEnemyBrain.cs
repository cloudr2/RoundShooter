using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]

public class BossEnemyBrain : MonoBehaviour
{
	private Ship owner;
	private PlayerBrain player;
	private bool isTargetInRange = false;
	private bool isAbleToShoot = true;
	private bool isMoving = false;
	private bool isShootingLaser = false;
	private float scalationAmount;
	private float scalationInterval;
	private LineRenderer lr;
	private ParticleSystem laserHitEffect;
	private Light laserLight;
	private Animator anim;

	void Start () {
		owner = GetComponent<Ship> ();
		player = FindObjectOfType<PlayerBrain> ();
		lr = GetComponent<LineRenderer> ();
		laserHitEffect = GetComponentInChildren<ParticleSystem> ();
		laserLight = GetComponentInChildren<Light> ();
		lr.enabled = false;
		StopLaserEffect ();
		scalationAmount = 1.25f;
		scalationInterval = 0.25f;
	}

	void Update () {
		if (!isMoving) {
			StartCoroutine (MoveBoss ());
			isMoving = true;
		}
		LookTowardsPlayer ();
		ShowLaserTrail ();
		Shoot ();
	}

	private void LookTowardsPlayer () {
		if (player) {
			Vector3 direction = player.transform.position - transform.position;
			direction = direction.normalized;
			transform.right = direction;
		}
	}

	private IEnumerator MoveBoss() {
		while (player) {
			Vector3 direction = new Vector3 (Random.Range (-1f, 1f), Random.Range (-1f, 1f), 0f);
			owner.Move (direction);
			yield return new WaitForSeconds (Random.Range (0f, 2f));
		}
	}

	private void ShowLaserTrail () {
		if (lr.enabled && player && isShootingLaser) {
			lr.SetPosition (0,owner.aim.position);
			lr.SetPosition (1,player.transform.position);
			Vector3 direction = player.transform.position - transform.position;
			laserHitEffect.transform.position = player.transform.position - direction.normalized;
			laserHitEffect.transform.rotation = Quaternion.LookRotation (-direction);
		}
	}

	private IEnumerator ShootLaser() {
		float laserDamage = owner.damage;
		PlayLaserEffect ();
		isShootingLaser = true;
		while (isTargetInRange) {
			laserDamage *= scalationAmount;
			player.SendMessage ("ReceiveDamage", laserDamage );
			owner.RegainHealth (laserDamage * 2f);
			yield return new WaitForSeconds (scalationInterval);
		}
		isShootingLaser = false;
		StopLaserEffect ();
	}

	private void PlayLaserEffect() {
		laserLight.enabled = true;
		laserHitEffect.Play ();
	}

	private void StopLaserEffect() {
		laserLight.enabled = false;
		laserHitEffect.Stop ();
	}

	private void Shoot() {
		if (isAbleToShoot) {
			if (owner.CurrentHealth * 3 <= owner.Maxhealth) {
				GenerateScatterShot (5);
			} else if (owner.CurrentHealth * 1.5 <= owner.Maxhealth) {
				GenerateScatterShot (4);
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
			if (Random.Range (0, 10) > 4) {
				StartCoroutine (ShootLaser ());
				lr.enabled = true;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D trigger) {
		if (trigger.gameObject.tag == "Player") {
			isTargetInRange = false;
			StopCoroutine (ShootLaser());
			lr.enabled = false;
		}
	}

	private IEnumerator ShootCooldown() {
		yield return new WaitForSeconds (owner.fireRate);
		isAbleToShoot = true;
	}

	//TODO: FIJARSE PORQUE CRASHEA ESTA PORONGA
	void OnDestroy() {
		if (owner.CurrentHealth <= 0f) {
			Game.instance.ShowResult ("CLEAR");
		}
	}
}