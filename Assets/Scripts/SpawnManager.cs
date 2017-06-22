using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public static SpawnManager instance = null;
	public GameObject enemy;
	public int maxAmountOfEnemies;

	private float spawnTime;
	private int scorePerKill;
	private List<SpawnPoint> spawnPointsList = new List<SpawnPoint>();

	void Start()
	{
		if (instance == null)
		{
			spawnTime = 3f;
			scorePerKill = 100;
			SpawnPoint[] points = FindObjectsOfType<SpawnPoint> ();
			foreach (var item in points)
			{
				spawnPointsList.Add (item);
				SpawnOneEnemy (item);
			}
			StartCoroutine("SpawnEnemyOverTime");
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
		scorePerKill *= increment;
	}

	public int GetSpawnTime()
	{
		return spawnTime;
	}

	public void AccelerateSpawnTime(float increment)
	{
		spawnTime /= (increment);
	}

	private void SpawnOneEnemy(SpawnPoint spawnPoint)
	{
		Transform mySpawnTransform = spawnPointsList [spawnPoint].transform;
		Enemy newEnemy = GameObject.Instantiate (enemy, mySpawnTransform).GetComponent<Enemy> ();
		newEnemy.transform.position = mySpawnTransform.position;
	}

	private IEnumerator SpawnEnemyOverTime()
	{
		int enemiesCount = 0;
		while (enemiesCount <= maxAmountOfEnemies)
		{
			int rand = Random.Range (0, spawnPointsList.Count);
			SpawnOneEnemy (rand);
			enemiesCount++;
			yield return new WaitForSeconds (spawnTime);
		}
	}
}
