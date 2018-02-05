using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public static SpawnManager instance = null;
	public GameObject[] enemy;
	public int maxAmountOfEnemies;

	private int enemyCount;
	private int enemiesRemaining;
	private float spawnTime;
	private List<SpawnPoint> spawnPointsList = new List<SpawnPoint>();
	private bool changePhase2 = false;
	private bool changePhase3 = false;

	void Start()
	{
		if (instance == null)
		{
			instance = this;
			enemyCount = 0;
			spawnTime = 3f;

			SpawnPoint[] points = FindObjectsOfType<SpawnPoint> ();
			foreach (var item in points)
			{
				if (enemyCount >= maxAmountOfEnemies) return;
				spawnPointsList.Add (item);
				SpawnEnemyAtSpawnPoint (spawnPointsList.IndexOf(item));
			}
			StartCoroutine("SpawnEnemiesOverTime");
		}
		else
		{
			Destroy (gameObject);
		}
	}

	void Update() {
		if (Time.time >= 20f && !changePhase2) {
			spawnTime = 2f;
			changePhase2 = true;
		}
		else if (Time.time >= 45f && !changePhase3) {
			spawnTime = 1f;
			changePhase3 = true;
		}
	}

	public void SpawnEnemyAtSpawnPoint(int spawnPointIndex) {
		Transform mySpawnTransform = spawnPointsList[spawnPointIndex].transform;
		Instantiate (enemy[Random.Range(0,enemy.Length)], mySpawnTransform);
		enemyCount++;
	}

	private IEnumerator SpawnEnemiesOverTime()
	{
		while (enemyCount < maxAmountOfEnemies)
		{
			yield return new WaitForSeconds (spawnTime);
			SpawnEnemyAtSpawnPoint (Random.Range (0, spawnPointsList.Count));
		}
	}
}
