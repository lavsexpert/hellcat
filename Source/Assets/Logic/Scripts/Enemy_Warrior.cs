using UnityEngine;
using System.Collections;

public class Enemy_Warrior : MonoBehaviour {

	public Transform Treasure; 
	public Transform Player;

	public Transform Enemy;
	private NavMeshAgent agent;
	public Transform Random_Direction;
	bool Navigation_Random_Point_Generation_Flag =false;
	int Navigation_Random_Point_Life_Time = 0;

	void Start() {
		agent = GetComponent<NavMeshAgent>();
	}






	// Update is luanching every fps.
	void Update ()
	{

//		RaycastHit hit;
		int Random_Value_1 = 0;
	
		var Look_Dir = Player.position - Enemy.position; 
		Look_Dir.y = 0;
		//Enemy.rotation = Quaternion.Slerp (Enemy.rotation, Quaternion.LookRotation (Look_Dir), Rotation_Speed*Time.deltaTime);
		//Enemy.position += Enemy.forward * Move_Speed * Time.deltaTime;  

		Look_Dir = Player.position - Enemy.position;
		Look_Dir.y = 0;
		float R= Look_Dir.x*Look_Dir.x+Look_Dir.y*Look_Dir.y+Look_Dir.z*Look_Dir.z;
		R = Mathf.Sqrt (R);

		//Go for PLAYER if you Enemy See him
		if (  R<3 )
		{
//			_target.position = new Vector3 ( Player.position.x , Player.position.y , Player.position.z);

			agent.SetDestination(Player.position); 

			//Enemy.rotation = Quaternion.Slerp (Enemy.rotation, Quaternion.LookRotation (Look_Dir), Rotation_Speed*Time.deltaTime);
			//Enemy.position += Enemy.forward * Move_Speed * Time.deltaTime;  

		}

		//Go for PLAYER if you Enemy See him
		else
		//Enemy go to Treasure
		{

			Random_Value_1 = Random.Range (1, 3);

			//Look_Dir = Treasure.position - Enemy.position; 
			//Look_Dir.y = 0;

		if (Random_Value_1 == 1) 
			{

						
					//	Enemy.rotation = Quaternion.Slerp (Enemy.rotation, Quaternion.LookRotation (Look_Dir), Rotation_Speed * Time.deltaTime);
					//	Enemy.position += Enemy.forward * Move_Speed * Time.deltaTime;  
			//	_target.position =  new Vector3 ( Treasure.position.x ,Treasure.position.y , Treasure.position.z) ;

				agent.SetDestination(Treasure.position); 
				}

			//Enemy go to Treasure
			else 
		{
		
				if (Navigation_Random_Point_Generation_Flag == false)
				{
				Random_Direction.position = new Vector3 (Enemy.position.x + Random.Range (-20,20), 0 , Enemy.position.z + Random.Range (-20, 20));
					Navigation_Random_Point_Generation_Flag =true;
					Navigation_Random_Point_Life_Time  = 30;
				}
				agent.SetDestination(Random_Direction.position);  
				Navigation_Random_Point_Life_Time--;
				if (Navigation_Random_Point_Life_Time <= 0 )
				{

					Navigation_Random_Point_Generation_Flag = false;
				}


			//Enemy.rotation = Quaternion.Slerp (Enemy.rotation, Quaternion.LookRotation (Random_Rotation), Rotation_Speed * Time.deltaTime);
			//Enemy.position += Enemy.forward * Move_Speed * Time.deltaTime; 
			} 
				}

	}
	}
