using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;

	private int Score;
	public int currentLevel { get; set;}

	void Awake () {
		if (instance != null) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    public void AddScore(int score) {
		Score += score;
//		Game.instance.scoreLabel.text = "Score: " + GetScore ().ToString();
	}

	public int GetScore() {
		return Score;
	}

	public void StartGame() {
		Score = 0;
		LoadLevel ("STAGE1");
    }

	public void LoadNextLevel() {
		currentLevel++;
		LoadLevel(currentLevel);
	}

	public void RestartLevel() {
		LoadLevel (currentLevel);
	}

    public void LoadLevel(string levelName) {
        SceneManager.LoadScene (levelName);
    }

	public void LoadLevel(int index) {
		SceneManager.LoadScene (index);
	}
}
