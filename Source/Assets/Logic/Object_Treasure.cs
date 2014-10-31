using UnityEngine;
using System.Collections;

public class Object_Treasure : MonoBehaviour 
{
	// При столкновении
	void OnTriggerEnter(Collider Trigger)
	{
		// Если сундука коснулся воин - загрузить экран проигрыша
		if (Trigger.collider.tag == "Enemy_Warrior") 
		{
			Application.LoadLevel("Game_Over");
		}
	}
}
