using UnityEngine;
using System.Collections;

public class Object_Trap : MonoBehaviour 
{
	bool Caught_In_Trap = false;


	IEnumerator  WAINTING (){

	
		if (Caught_In_Trap == true) {
			yield return new  WaitForSeconds(5);	

			Application.LoadLevel("Game_Winner");
		
		}
	}





	// При столкновении
	void OnTriggerEnter(Collider Trigger)

		 

	{
		// Если в ловушку попал воин - загрузить победный экран
		if (Trigger.collider.tag == "Enemy_Warrior") 
		{
			Caught_In_Trap = true;

			StartCoroutine (WAINTING ());
			//yield return new WaitForSeconds(5);
		 
			//Application.LoadLevel("Game_Winner");
		 

			}
	}
}
