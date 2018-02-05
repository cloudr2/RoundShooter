using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
	public TextMesh label;

	private SpriteRenderer sr;
	private bool isConsumed = false;

	void Start() {
		sr = GetComponent<SpriteRenderer> ();
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Player" && !isConsumed) {
			col.SendMessage ("ActivateShield");
			sr.enabled = false;
			isConsumed = true;
			label.text = "SHIELD!";
			Destroy (gameObject, 2f);
		}
	}
}

