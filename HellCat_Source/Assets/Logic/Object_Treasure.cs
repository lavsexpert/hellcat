using UnityEngine;
using System.Collections;

public class Object_Treasure : MonoBehaviour 
{
	public int TimeToWaitOnTreasureFound = 1;

	private GameObject camera;
	private float TimeWaitStarted = 0;
	private bool TreasureTriggered = false;
	private Vector3 cameraStartPosition;

	// При запуске
	void Start() 
	{
		// Поиск объектов на сцене
		camera = GameObject.FindWithTag ("MainCamera");
	}
	void Update () 
	{
		if (TreasureTriggered == true) 
		{
			camera.transform.LookAt(transform.position);
			if (TimeWaitStarted == 0)
			{
				cameraStartPosition = camera.transform.position;
				camera.BroadcastMessage("SetFollowPlayer", false);
				TimeWaitStarted = Time.time;
				audio.Play();
				return;
			}
			float fracPassed = (Time.time - TimeWaitStarted)/TimeToWaitOnTreasureFound;
			camera.transform.position = Vector3.Lerp(cameraStartPosition, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), fracPassed);
			if (((Time.time - TimeWaitStarted) > TimeToWaitOnTreasureFound)&&(audio.isPlaying == false))
			{
				TreasureTriggered = false;
				Application.LoadLevel("Game_Over");
			}
		}
	}

	// При столкновении
	void OnTriggerEnter(Collider Trigger)
	{
		// Если сундука коснулся воин - загрузить экран проигрыша
		if (Trigger.collider.tag == "Enemy_Warrior") 
		{
			TreasureTriggered = true;
		}
	}
}
