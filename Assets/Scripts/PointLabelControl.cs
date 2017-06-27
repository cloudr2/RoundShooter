using System.Collections;
using UnityEngine;

public class PointLabelControl : MonoBehaviour
{
	private TextMesh text;

	void Start ()
	{
		text = GetComponent<TextMesh> ();
		GameObject.Destroy (this.gameObject, 2f);
		text.text = SpawnManager.instance.GetEnemyScorePerKill ().ToString () + "!";
	}
	
	// Update is called once per frame
	void Update () {
		Color myColor = Color.Lerp(Color.white,Color.green,Mathf.PingPong(Time.time,0.125f));
		text.color = myColor;
	}
}
