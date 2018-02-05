using UnityEngine;
using System.Collections;

public class PlayerBrain : MonoBehaviour
{
	private Ship owner;
	private bool isAbleToShoot = true;
	private bool hasExtraShoot = false;
	private bool isShootDisabled = false;
	private int numerOfShots;
	private float originalSpeed;

	public SpriteRenderer shieldSprite;
	public SpriteRenderer shootDisabledSprite;
	public SpriteRenderer slowTimeSprite;

	void Start ()
	{
		owner = GetComponent<Ship> ();
		numerOfShots = 1;
		originalSpeed = owner.speed;
	}
	
	void Update ()
	{
		PlayerMove ();
		RotateCannon ();
		Shoot ();
	}

	private void PlayerMove ()
	{
		float horizontalMovement = Input.GetAxisRaw ("Horizontal");
		float VerticalMovement = Input.GetAxisRaw ("Vertical");
		Vector3 direction = new Vector3(horizontalMovement,VerticalMovement,0f);
		owner.Move (direction);
	}

	private void RotateCannon()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);
		mousePos.z = transform.position.z;
		this.transform.right = mousePos - transform.position;
	}

	private void Shoot()
	{
		if ((Input.GetKey (KeyCode.Space) || (Input.GetMouseButton(0))) && isAbleToShoot && !isShootDisabled) {
			GenerateScatterShot (numerOfShots);
		}
	}

	private void GenerateScatterShot(int shootAmount) {
		float offset = 1f / shootAmount;
		float angle = offset * (shootAmount - 1) / 2;
		for (int i = 0; i < shootAmount; i++) {
			ChaingunBullet newBullet = ((GameObject)Instantiate (owner.bulletPrefab)).GetComponent<ChaingunBullet> ();
			newBullet.gameObject.layer = 9;
			newBullet.gameObject.tag = gameObject.tag + "Bullet";
			newBullet.transform.right = new Vector3 (transform.right.x * Mathf.Cos (angle) - transform.right.y * Mathf.Sin (angle), transform.right.x * Mathf.Sin (angle) + transform.right.y * Mathf.Cos (angle), transform.right.z);
			newBullet.transform.position = owner.aim.position;
			newBullet.transform.position += newBullet.transform.right;
			newBullet.damage = owner.damage;
			angle -= offset;
			isAbleToShoot = false;
			StartCoroutine (ShootCooldown ());
		}
	}

	private IEnumerator ShootCooldown()
	{
		yield return new WaitForSeconds (owner.fireRate);
		isAbleToShoot = true;
	}

	private void ModifySpeed (float percent) {
		owner.speed *= percent;
	}

	private void RestoreSpeed () {
		owner.speed = originalSpeed;
		slowTimeSprite.enabled = false;
	}

	private void AddExtraShots(){
		if (!hasExtraShoot) {
			hasExtraShoot = true;
		} else {
			CancelInvoke("DeactivateExtraShoot");
		}
		numerOfShots++;
		Invoke ("DeactivateExtraShoot", 5f);
	}

	private void DeactivateExtraShoot() {
		hasExtraShoot = false;
		numerOfShots = 1;
	}

	private void ActivateShield() {
		if (owner.canBeHit) {
			owner.canBeHit = false;
			shieldSprite.enabled = true;
		} else {
			CancelInvoke("DeactivateShield");
		}
		Invoke ("DeactivateShield", 5f);
	}

	private void DeactivateShield() {
		owner.canBeHit = true;
		shieldSprite.enabled = false;
	}

	private void DisableShoot() {
		if (!isShootDisabled) {
			isShootDisabled = true;
			shootDisabledSprite.enabled = true;
		} else {
			CancelInvoke("EnableShoot");
		}
		Invoke ("EnableShoot", 5f);
	}

	private void EnableShoot() {
		isShootDisabled = false;
		shootDisabledSprite.enabled = false;
	}

	private void ReduceSpeed (float rate) {
		if (!slowTimeSprite.enabled) {
			slowTimeSprite.enabled = true;
			ModifySpeed (rate);
		} else {
			CancelInvoke ("RestoreSpeed");
			ModifySpeed (rate);
		}
		Invoke ("RestoreSpeed", 5f);
	}
}

