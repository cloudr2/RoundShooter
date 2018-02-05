using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTime : MonoBehaviour {

	public TextMesh label;
	public float rate;

	private SpriteRenderer sr;
	private bool isConsumed = false;

	void Start() {
		sr = GetComponent<SpriteRenderer> ();
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Player" && !isConsumed) {
			col.SendMessage ("ReduceSpeed",rate);
			sr.enabled = false;
			isConsumed = true;
			label.text = "TIME SLOWED!";
			Destroy (gameObject, 2f);
		}
	}
}
