using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public SpriteRenderer bg;	

	[SerializeField]
	private GameObject player;
	private Vector2 dim;
	private float height;
	private float width;
	private float xMax;
	private float xMin;
	private float yMax;
	private float yMin;
	private float fixX;
	private float fixY;

	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player");
		//prevent camera from going beyond background size
		height = 2f * Camera.main.orthographicSize;
		width = height * Camera.main.aspect;
		dim = bg.size;
		xMax = bg.transform.position.x + (dim.x / 2 - width / 2);
		xMin = bg.transform.position.x - (dim.x / 2 - width / 2);
		yMax = bg.transform.position.y + (dim.y / 2 - height / 2);
		yMin = bg.transform.position.y - (dim.y / 2 - height / 2);
		fixX = (dim.x / 2 - xMax) / 2 ;
		fixY = (dim.y / 2) - (yMax * 2) - yMax;
		xMin = -fixX;
		xMax = fixX;
		yMin += fixY;
		yMax -= fixY;
	}

	void LateUpdate () 
	{
		if (player)
		{
			transform.position = new Vector3 (Mathf.Clamp(player.transform.position.x,xMin,xMax),Mathf.Clamp(player.transform.position.y,yMin,yMax),-10);
		}
	}
}

