using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExtraShot : MonoBehaviour
{
	public TextMesh label;

	private SpriteRenderer sr;
	private bool isConsumed = false;

	void Start() {
		sr = GetComponent<SpriteRenderer> ();
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Player" && !isConsumed) {
			col.SendMessage ("AddExtraShots");
			sr.enabled = false;
			isConsumed = true;
			label.text = "EXTRA SHOT!";
			Destroy (gameObject, 2f);
		}
	}
}

