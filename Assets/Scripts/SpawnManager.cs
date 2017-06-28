using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public static SpawnManager instance = null;
	public GameObject enemy;
	public int maxAmountOfEnemies;

	private int enemyCount;
	private int enemiesRemaining;
	private float spawnTime;
	private int scorePerKill;
	private List<SpawnPoint> spawnPointsList = new List<SpawnPoint>();

	void Start()
	{
		if (instance == null)
		{
			instance = this;
			enemyCount = 0;
			spawnTime = 3f;
			scorePerKill = 100;
			enemiesRemaining = maxAmountOfEnemies;
			SpawnPoint[] points = FindObjectsOfType<SpawnPoint> ();
			foreach (var item in points)
			{
				spawnPointsList.Add (item);
				SpawnEnemyAtSpawnPoint (spawnPointsList.IndexOf(item));
			}
			StartCoroutine("SpawnEnemiesOverTime");
		}
		else
		{
			Destroy (this.gameObject);
		}
	}

	public int GetEnemyScorePerKill()
	{
		return scorePerKill;
	}

	public void IncrementEnemyScorePerKill(float increment)
	{
		scorePerKill = Mathf.FloorToInt(scorePerKill * increment);
	}

	public float GetSpawnTime()
	{
		return spawnTime;
	}

	public void SetSpawnTime(float interval)
	{
		spawnTime = interval;
	}

	private void SpawnEnemyAtSpawnPoint(int spawnPointIndex)
	{
		Transform mySpawnTransform = spawnPointsList[spawnPointIndex].transform;
		Enemy newEnemy = GameObject.Instantiate (enemy, mySpawnTransform).GetComponent<Enemy> ();
		newEnemy.transform.position = mySpawnTransform.position;
		enemyCount++;
	}

	private IEnumerator SpawnEnemiesOverTime()
	{
		while (enemyCount < maxAmountOfEnemies)
		{
			yield return new WaitForSeconds (spawnTime);
			int rand = Random.Range (0, spawnPointsList.Count);
			SpawnEnemyAtSpawnPoint (rand);
		}
	}

	public void UpdateRemainingEnemies()
	{
		enemiesRemaining--;
		Game.instance.EnemyCountLabel.text = "Quedan " +  enemiesRemaining.ToString() + " Enemigos";
	}
}
