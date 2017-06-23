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
			print ("======= " + currentStage + " ========");
		} 
		else
		{
			Destroy (this.gameObject);
		}
	}
	
	void Update ()
	{
		currentTime += Time.deltaTime;

		if (currentTime >= 25f && currentStage == stage.stage_1)
		{
			SpawnManager.instance.SetSpawnTime (2f);
			SpawnManager.instance.IncrementEnemyScorePerKill (1.5f);
			ChangeGameStage ();
			print (currentTime);
		}	
		else if (currentTime >= 45f && currentStage == stage.stage_2)
		{
			SpawnManager.instance.SetSpawnTime (1f);
			SpawnManager.instance.IncrementEnemyScorePerKill (2.5f);
			ChangeGameStage ();
			print (currentTime);
		}
	}

	private void ChangeGameStage()
	{
		switch (currentStage)
		{
		case stage.stage_1:
			{
				currentStage = stage.stage_2;
				break;
			}

		case stage.stage_2:
			{
				currentStage = stage.stage_3;
				break;
			}
		default:
			{
				currentStage = stage.stage_1;
				break;
			}
		}
		print ("======= " + currentStage + " ========");
	}



	public float GetTime()
	{
		return currentTime;
	}
}

