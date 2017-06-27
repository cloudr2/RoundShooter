using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour 
{
	public Transform bulletHolder;
	public static Game instance = null;
	public Text scoreLabel;
	public Text highscoreLabel;
	public Text EnemyCountLabel;
	public Image winImage;
	public Image loseImage;
	public Image restartLabel;

	void Start () 
	{
		if (instance == null) 
		{
			instance = this;
			highscoreLabel.text = "HighScore: " + GameManager.instance.GetHighScore().ToString();
			winImage.gameObject.SetActive (false);
			loseImage.gameObject.SetActive (false);
			restartLabel.gameObject.SetActive (false);
		} 
		else 
		{
			Destroy (this.gameObject);
		}
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.R) && restartLabel.IsActive()) {
			GameManager.instance.StartGame ();
		}
	}

	public void ShowResult(string gameResult)
	{
		if (gameResult == "WIN") 
		{
			winImage.gameObject.SetActive (true);
		}
		else if (gameResult == "LOSE")
		{
			loseImage.gameObject.SetActive (true);
		}
		restartLabel.gameObject.SetActive (true);
	}
}
