using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour 
{
	public Transform bulletHolder;
	public static Game instance = null;

	void Start () 
	{
		if (instance == null) 
		{
			instance = this;
			print (PlayerPrefs.GetInt ("HighScore"));
		} 
		else 
		{
			Destroy (this.gameObject);
		}
	}
}
