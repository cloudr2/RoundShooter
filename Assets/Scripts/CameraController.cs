using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public Player player;

	private Camera myCamera;
	private Vector3 offset;

	void Start () 
	{
		myCamera = GetComponent<Camera> ();
		offset = transform.position - player.transform.position;
	}
		
	void LateUpdate () 
	{
		if (player)
		{
			transform.position = player.transform.position + offset;
		}
	}
}

