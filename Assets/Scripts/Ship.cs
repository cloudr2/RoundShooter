using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]

public class Ship : MonoBehaviour
{
	[Header ("Stats")]
	public float Maxhealth;
	public float damage;
	public float speed;
	public float fireRate;
	public int score;
	public float CurrentHealth {get {return currentHealth; }}
	public GameObject healthBar;
	public GameObject bulletPrefab;
	public Transform aim;
	public bool canBeHit = true;
	public GameObject[] powerups;

	private float currentHealth;
	private Rigidbody2D rb;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		currentHealth = Maxhealth;
		healthBar.transform.localScale = new Vector3 (Mathf.Clamp(CalculateHealth (),0f,1f), 1f, 1f);
	}

	private float CalculateHealth() {
		return currentHealth / Maxhealth;
	}

	public void Move(Vector3 direction) {
		rb.velocity = direction * speed;
	}

	public void ReceiveDamage(float damage) {
		if (canBeHit) {
			currentHealth -= damage;
			healthBar.transform.localScale = new Vector3 (Mathf.Clamp(CalculateHealth (),0f,1f), 1f, 1f);
			if (currentHealth <= 0) {
				Death ();
			}
		}
	}

	public void RegainHealth (float heal) {
		if (currentHealth + heal < Maxhealth) {
			currentHealth += heal;
		} else if (currentHealth + heal >= Maxhealth) {
			currentHealth = Maxhealth;
		}
		healthBar.transform.localScale = new Vector3 (Mathf.Clamp(CalculateHealth (),0f,1f), 1f, 1f);
	}
		
	private void Death()
	{
		if (gameObject.tag == "Player") {
			Game.instance.ShowResult ("LOSE");
		}
		if (gameObject.tag == "Enemy") {
			GameManager.instance.AddScore (score);
			Game.instance.UpdateScoreLabel ();
			Game.instance.CheckEnemiesAlive();
			SpawnPowerUp ();
		}
		Destroy (gameObject);
	}
		
	private void SpawnPowerUp() {
		float chance = Random.Range (0,10);
		if (chance > 6 && powerups.Length > 0) {
			int rand = Random.Range (0,powerups.Length);
			GameObject newPow = Instantiate (powerups[rand],transform.position,Quaternion.identity);
			newPow.transform.parent = Game.instance.transform;
		}
	}
}