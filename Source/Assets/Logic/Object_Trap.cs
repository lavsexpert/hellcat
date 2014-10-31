using UnityEngine;
using System.Collections;

public class Object_Trap : MonoBehaviour 
{
	// При столкновении
	void OnTriggerEnter(Collider Trigger)
	{
		// Если в ловушку попал воин - загрузить победный экран
		if (Trigger.collider.tag == "Enemy_Warrior") 
		{
			Application.LoadLevel("Game_Winner");
		}
	}
}
