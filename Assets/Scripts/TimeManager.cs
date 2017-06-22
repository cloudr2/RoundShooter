using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	public static TimeManager instance = null;

	private enum stage { stage_1, stage_2, stage_3 };
	private stage currentStage;
	private float currentTime;

	void Start ()
	{
		if (instance == null)
		{
			instance = this;
			currentTime = 0f;
			currentStage = stage.stage_1;
		} 
		else
		{
			Destroy (this.gameObject);
		}
	}
	
	void Update ()
	{
		currentTime += Time.deltaTime;
		print (currentTime);

		if (currentTime >= 25f && currentStage == stage.stage_1)
		{
			SpawnManager.instance.AccelerateSpawnTime (1.5f);
			SpawnManager.instance.IncrementEnemyScorePerKill (1.5f);
			currentStage = stage.stage_2;
		}	
		else if (currentTime >= 45f && currentStage == stage.stage_2)
		{
			SpawnManager.instance.AccelerateSpawnTime ();
			SpawnManager.instance.IncrementEnemyScorePerKill (2.5f);
			currentStage = stage.stage_3;
		}
	}

	public float GetTime()
	{
		return currentTime;
	}
}

