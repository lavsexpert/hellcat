using UnityEngine;
using System.Collections;

public class Object_Trap : MonoBehaviour 
{
	bool Caught_In_Trap = false;
	int Caught_number = 0 ;
	

	IEnumerator  WAINTING (){

	
		if (Caught_In_Trap == true) {
			yield return new  WaitForSeconds(2);	

			Application.LoadLevel("Game_Winner");
		
		}
	}





	// При столкновении
	void OnTriggerEnter(Collider Trigger)

		 

	{
		// Если в ловушку попал воин - загрузить победный экран
		if (Trigger.collider.tag == "Enemy_Warrior") 
		{
			Caught_number++;
			Caught_In_Trap = true;

			if (  Caught_number == 2){ 
			StartCoroutine (WAINTING ());
			//yield return new WaitForSeconds(5);
		 
			//Application.LoadLevel("Game_Winner");
			}

			}







		if (Trigger.collider.tag == "Enemy_Wolf") 
		{
			Caught_number++;
			Caught_In_Trap = true;
			
			if (  Caught_number == 2){ 
				StartCoroutine (WAINTING ());
				//yield return new WaitForSeconds(5);
				
				//Application.LoadLevel("Game_Winner");
			}
			
		}
	}
}
