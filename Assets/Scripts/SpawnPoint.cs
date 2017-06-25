using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	private SpriteRenderer myRenderer;

	void Start () {
		myRenderer = GetComponent<SpriteRenderer> ();
		myRenderer.color = Color.clear;
	}
}
