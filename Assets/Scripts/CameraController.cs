﻿using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public GameObject player;
	private Vector3 offset;

	void Start () 
	{
		if (player)
		{
			offset = transform.position - player.transform.position;
		}
	}

	void LateUpdate () 
	{
		if (player)
		{
			transform.position = player.transform.position + offset;
		}
	}
}
