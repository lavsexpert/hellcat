using UnityEngine;
using System.Collections;

public class Object_Swamp : MonoBehaviour 
{
	bool Caught_In_Trap = false;

	public Transform Swamp;
	public Transform Warrior;	
	
 

	IEnumerator  WAINTING (){
		
		
 


		yield return new  WaitForSeconds(5);
			Application.LoadLevel("Game_Winner");
		 
		}

	


	void Update ()
	{
		var Swamp_Direction_Distance = Swamp.position -  Warrior.position;
		Swamp_Direction_Distance.y = 0;
		float Swamp_Distance = Swamp_Direction_Distance.x * Swamp_Direction_Distance.x + Swamp_Direction_Distance.y * Swamp_Direction_Distance.y + Swamp_Direction_Distance.z * Swamp_Direction_Distance.z;
		Swamp_Distance = Mathf.Sqrt (Swamp_Distance);
	
	 if (Swamp_Distance < 0.5f)
		{
			
			StartCoroutine (WAINTING ());
	}
	 
	
	}
	
	/*
	// При столкновении
	void OnTriggerEnter (Collider Trigger)		
	{
		// Если в ловушку попал воин - загрузить победный экран
		if (Trigger.collider.tag == "Enemy_Warrior") {
						//StartCoroutine (WAINTING2 ());
		
						Caught_In_Trap = true;

						
				}

	
	}



	void OnTriggerExit (Collider Trigger)		
	{
		// Если в ловушку попал воин - загрузить победный экран
		if (Trigger.collider.tag == "Enemy_Warrior") {
			//StartCoroutine (WAINTING2 ());
			
			Caught_In_Trap = false;
		
			
		}



		
	}*/
}

 

