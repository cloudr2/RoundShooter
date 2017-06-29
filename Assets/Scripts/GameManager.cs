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
		Game.instance.scoreLabel.text = "Score: " + GetScore ().ToString();
	}

	public int GetScore()
	{
		return Score;
	}

	public int GetFinalScore()
	{
		return Score * Mathf.RoundToInt(TimeManager.instance.GetTime ());
	}

	public int GetHighScore()
	{
		return PlayerPrefs.GetInt ("HighScore");
	}

	public void StartGame()
	{
		Score = 0;
		SceneManager.LoadScene ("Game");
		Time.timeScale = 1f;
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
			EndGame ("WIN");
		}
	}

	public void EndGame(string gameResult)
	{
		Time.timeScale = 0f;
		Game.instance.ShowResult (gameResult);
		if (gameResult == "WIN") 
		{
			PlayerPrefs.SetInt ("HighScore", GetFinalScore ());
			Game.instance.scoreLabel.text = "Score: " + GetFinalScore ().ToString();
		}
	}
}
