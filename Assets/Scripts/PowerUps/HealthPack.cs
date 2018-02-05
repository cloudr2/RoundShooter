using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthPack : MonoBehaviour
{
	public float healAmount = 30f;
	public TextMesh label;

	private SpriteRenderer sr;
	private bool isConsumed = false;

	void Start() {
		sr = GetComponent<SpriteRenderer> ();
	}
		
	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Player" && !isConsumed) {
			col.SendMessage ("RegainHealth", healAmount);
			sr.enabled = false;
			isConsumed = true;
			label.text = "+" + healAmount.ToString ();
			Destroy (gameObject, 2f);
		}
	}
}

