using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour 
{
	public static Game instance = null;

	public int stageNumber;
	public GameObject winScreen;
	public GameObject loseScreen;
	public Text scoreLabel;
	public Text enemyLabel;
	public Text infoText;

	private int enemiesRemaining;

	void Start () 
	{
		if (instance == null) 
		{
			instance = this;
			Initialize ();
		} 
		else 
		{
			Destroy (gameObject);
		}
	}

	private void Initialize()
	{
		GameManager.instance.currentLevel = stageNumber;
		enemiesRemaining = SpawnManager.instance.maxAmountOfEnemies;
		enemyLabel.text = "Enemies Remaining: " + enemiesRemaining.ToString ();
		scoreLabel.text = "Score: " + GameManager.instance.GetScore ().ToString ();
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.R)) {
			GameManager.instance.RestartLevel ();
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			ShowResult ("WIN");
		}
	}

	public void CheckEnemiesAlive()
	{
		if (SpawnManager.instance.maxAmountOfEnemies > 0) {
			enemiesRemaining--;
			enemyLabel.text = "Enemies Remaining: " + enemiesRemaining.ToString ();
			if (enemiesRemaining <= 0) {
				ShowResult ("WIN");
			}
		}
	}

	public void UpdateScoreLabel() {
		scoreLabel.text = "Score: " + GameManager.instance.GetScore ().ToString();
	}

	public void ShowResult(string gameResult)
	{
		StartCoroutine (LevelTransition(gameResult));
    }

	private IEnumerator LevelTransition (string result) {
		infoText.enabled = true;

		if (result == "LOSE") {
			for (int i = 0; i < 3; i++) {
				infoText.text = "Restarting the level in\n" + (3 - i).ToString();
				yield return new WaitForSeconds (1f);
			}
			infoText.enabled = false;
			GameManager.instance.RestartLevel ();

		} else if (result == "WIN") { 
			for (int i = 0; i < 3; i++) {
				infoText.text = "Entering next level in\n" + (3 - i).ToString();
				yield return new WaitForSeconds (1f);
			}
			infoText.enabled = false;
			GameManager.instance.LoadNextLevel ();

		} else if (result == "CLEAR") {
			for (int i = 0; i < 3; i++) {
				infoText.text = "GAME CLEARED!";
				yield return new WaitForSeconds (1f);
			}
			infoText.enabled = false;
			GameManager.instance.LoadLevel ("YOUWIN");
		}
	}
}
