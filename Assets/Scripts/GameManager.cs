using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private int Score;
	private int timeScore;
	public static GameManager instance = null;

	void Start ()
	{
		if (instance == null)
		{
			GameObject.DontDestroyOnLoad (this.gameObject);
			instance = this;
		}
		else
		{
			Destroy (this.gameObject);
		}
	}

	public void AddScore(int score)
	{
		Score += score;
	}

	public int GetScore()
	{
		return Score;
	}

	public int GetFinalScore()
	{
		return Score * TimeManager.instance.GetTime ();
	}

	public void StartGame()
	{
		SceneManager.LoadScene ("Game");
		Score = 0;
	}
		
	public void LoadLevel(string levelName)
	{
		SceneManager.LoadScene (levelName);
	}
		
	public void CheckEnemiesAlive()
	{
		Enemy[] enemies = FindObjectsOfType<Enemy>();
		if (enemies.Length <= 0)
		{
			this.EndGame ("WIN");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	public void EndGame(string gameResult)
	{
		if (gameResult == "WIN") 
		{
			print ("GANASTE! :)");
			PlayerPrefs.SetInt ("HighScore", GetFinalScore ());
		}
		else if (gameResult == "LOSE")
		{
			print ("Perdiste... D:");
		}

		this.LoadLevel ("MainMenu");
	}
}
