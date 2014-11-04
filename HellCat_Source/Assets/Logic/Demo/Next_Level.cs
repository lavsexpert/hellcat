using UnityEngine;
using System.Collections;

public class Next_Level : MonoBehaviour 
{
	// При столкновении
	void OnTriggerEnter (Collider Trigger)
	{
		// Если игрок коснулся зоны перехода - загружается демо 2 (карта)
		if (Trigger.collider.tag == "Player") 
		{
			Application.LoadLevel("Demo_02");
		}
	}
}
