using UnityEngine;
using System.Collections;

public class ShootDisabler : MonoBehaviour
{
	public TextMesh label;

	private SpriteRenderer sr;
	private bool isConsumed = false;

	void Start() {
		sr = GetComponent<SpriteRenderer> ();
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Player" && !isConsumed) {
			col.SendMessage ("DisableShoot");
			sr.enabled = false;
			isConsumed = true;
			label.text = "SHOOT DISABLED!";
			Destroy (gameObject, 2f);
		}
	}
}

