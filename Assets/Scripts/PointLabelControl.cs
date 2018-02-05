using System.Collections;
using UnityEngine;

public class PointLabelControl : MonoBehaviour
{
	private TextMesh text;

	void Start ()
	{
		text = GetComponent<TextMesh> ();
		GameObject.Destroy (this.gameObject, 2f);
		//text.text = SpawnManager.instance.GetEnemyScorePerKill ().ToString () + "!";
	}
	
	// Update is called once per frame
	void Update () {
		Color myColor = Color.Lerp(Color.green,Color.clear,Mathf.PingPong(Time.time,0.25f));
		text.color = myColor;
	}
}
