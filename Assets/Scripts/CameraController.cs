using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public Player player;
		
	void LateUpdate () 
	{
		if (player)
		{
			if (player.transform.position.x < 55f && player.transform.position.x > -55f)
				transform.position = new Vector3 (player.transform.position.x,this.transform.position.y,-10);

			if (player.transform.position.y < 35f && player.transform.position.y > -35f)
				transform.position = new Vector3 (this.transform.position.x,player.transform.position.y,-10);
		}
	}
}

