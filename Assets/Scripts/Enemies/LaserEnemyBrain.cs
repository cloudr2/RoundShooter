using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]

public class LaserEnemyBrain : MonoBehaviour {

	private Ship owner;
	private PlayerBrain player;
	private bool isTargetInRange = false;
	private float scalationAmount;
	private float scalationInterval;
	private LineRenderer lr;
	private ParticleSystem laserHitEffect;
	private Light laserLight;
	private Animator anim;
	private bool teleporting = false;

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
		MoveTowardsPlayer ();
		ShowLaserTrail ();
		if (owner.CurrentHealth < owner.Maxhealth && !teleporting) {
			teleporting = true;
			StartCoroutine (TeleportNearPlayer());
		}
	}

	private void MoveTowardsPlayer () {
		if (player) {
			Vector3 direction = player.transform.position - transform.position;
			direction = direction.normalized;
			transform.right = direction;
			owner.Move (direction);
		}
	}

	private IEnumerator TeleportNearPlayer () {
		while (player) {
			Vector3 targetPos = player.transform.position;
			float teleportTime = Random.Range (3f,6f);
			float xOffset = Random.Range (3f, 15f);
			float yOffset = Random.Range (3f, 6f);
			float xPos = targetPos.x;
			float yPos = targetPos.y;

			if (targetPos.x > 0) {
				//teleportleft
				xPos -= xOffset;
			} 
			else {
				//teleportright
				xPos += xOffset;
			}

			if (targetPos.y > 0) {
				//teleportdown
				yPos -= yOffset;
			} 
			else {
				//teleportup
				yPos += yOffset;
			}

			Vector3 teleportPos = new Vector3 (xPos,yPos,transform.position.z);
			transform.position = teleportPos;
			yield return new WaitForSeconds (teleportTime);
		}
	}

	private IEnumerator ShootLaser() {
		float laserDamage = owner.damage;
		PlayLaserEffect ();
		while (isTargetInRange) {
			laserDamage *= scalationAmount;
			player.SendMessage ("ReceiveDamage", laserDamage );
			yield return new WaitForSeconds (scalationInterval);
		}
		StopLaserEffect ();
	}

	private void ShowLaserTrail () {
		if (lr.enabled && player) {
			lr.SetPosition (0,owner.aim.position);
			lr.SetPosition (1,player.transform.position);
			Vector3 direction = player.transform.position - transform.position;
			laserHitEffect.transform.position = player.transform.position - direction.normalized;
			laserHitEffect.transform.rotation = Quaternion.LookRotation (-direction);
		}
	}

	private void PlayLaserEffect() {
		laserLight.enabled = true;
		laserHitEffect.Play ();
	}

	private void StopLaserEffect() {
		laserLight.enabled = false;
		laserHitEffect.Stop ();
	}

	private void OnTriggerEnter2D(Collider2D trigger) {
		if (trigger.gameObject.tag == "Player") {
			isTargetInRange = true;
			StartCoroutine (ShootLaser());
			lr.enabled = true;
		}
	}

	private void OnTriggerExit2D(Collider2D trigger) {
		if (trigger.gameObject.tag == "Player") {
			isTargetInRange = false;
			StopCoroutine (ShootLaser());
			lr.enabled = false;
		}
	}
}

