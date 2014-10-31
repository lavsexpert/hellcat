using UnityEngine;
using System.Collections;

public class Enemy_Warrior_Model_Animation : MonoBehaviour {

	public Transform Treasure;			// Сундук	
	public Transform Player;			// Игровой персонаж (кошка)
	public Transform Warrior;			// Неигровой персонаж (воин)
	public float Warrior_Scope = 3f;	// Область видимости воина
	public Transform Trap; 
	public Transform Swamp;
	public GameObject Warrior_Game_Object;











	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		float Warrior_Atack_Success_Factor;


				const int ANIMATION_RAZGON = 1;
				const int ANIMATION_STOP = 2;
				const int ANIMATION_ATACK = 3;
				const int ANIMATION_GO = 4;
				const int ANIMATION_FALLING = 5;
				const int ANIMATION_IN_TRAP = 6;
				const int ANIMATION_IN_SWAMP = 7;
				const int ANIMATION_TORMOZIT = 8;

				int Current_Animation_Play = ANIMATION_GO;
				int Previous_Animation_Play = ANIMATION_GO;


				var Treasure_Direction_Distance = Treasure.position - Warrior.position;
				float Treasure_Distance = Treasure_Direction_Distance.x * Treasure_Direction_Distance.x + Treasure_Direction_Distance.y * Treasure_Direction_Distance.y + Treasure_Direction_Distance.z * Treasure_Direction_Distance.z;
				

		var Trap_Direction_Distance = Trap.position -  Warrior.position;
		Trap_Direction_Distance.y = 0;
		float Trap_Distance = Trap_Direction_Distance.x * Trap_Direction_Distance.x + Trap_Direction_Distance.y * Trap_Direction_Distance.y + Trap_Direction_Distance.z * Trap_Direction_Distance.z;

		var Swamp_Direction_Distance = Swamp.position -  Warrior.position;
		Swamp_Direction_Distance.y = 0;
		float Swamp_Distance = Swamp_Direction_Distance.x * Swamp_Direction_Distance.x + Swamp_Direction_Distance.y * Swamp_Direction_Distance.y + Swamp_Direction_Distance.z * Swamp_Direction_Distance.z;



		// Рассчёт расстояния между кошкой и воином
				var Look_Dir = Player.position - Warrior.position; 
		
				Look_Dir.y = 0;
				float Distance = Look_Dir.x * Look_Dir.x + Look_Dir.y * Look_Dir.y + Look_Dir.z * Look_Dir.z;
				Distance = Mathf.Sqrt (Distance);
				Treasure_Distance = Mathf.Sqrt (Treasure_Distance);
		Trap_Distance = Mathf.Sqrt (Trap_Distance);
		Swamp_Distance = Mathf.Sqrt (Swamp_Distance);

	

						if (animation.isPlaying == false) {
								Previous_Animation_Play = Current_Animation_Play;


								if (((Previous_Animation_Play == ANIMATION_RAZGON) || 
										(Previous_Animation_Play == ANIMATION_GO)) &&
										(Distance >= 0.05f)) {

										//Previous_Animation_Play = Current_Animation_Play;
										Current_Animation_Play = ANIMATION_GO;

										animation.CrossFade ("Take_004_Go");

								}
							




		
								if ((Previous_Animation_Play == ANIMATION_GO) && ((Distance < (0.05f)) && (Distance > (0.0f)))) {
										//Previous_Animation_Play = Current_Animation_Play;
										Current_Animation_Play = ANIMATION_STOP;
			
										animation.CrossFade ("Take_008_Tormozit");

										animation.PlayQueued ("Take_002_Stop");


								}


		
				
				
						}

						

			

						if (((Previous_Animation_Play == ANIMATION_TORMOZIT) || (Previous_Animation_Play == ANIMATION_ATACK)) && (Distance == 0.0f)) {
								Previous_Animation_Play = Current_Animation_Play;
								Current_Animation_Play = ANIMATION_STOP;
			
								animation.CrossFade ("Take_002_Stop");
			
			
						}



	
						if ((Previous_Animation_Play == ANIMATION_STOP) && (Distance > 0.0f)) {
								Previous_Animation_Play = Current_Animation_Play;
								Current_Animation_Play = ANIMATION_RAZGON;
			
								animation.CrossFade ("Take_001_Razgon");
			
			
						}




						//if ((Previous_Animation_Play == ANIMATION_STOP) && (Distance == 0.0f)) {
						/*if (Distance == 0.0f) {
								//Previous_Animation_Play = Current_Animation_Play;
								Current_Animation_Play = ANIMATION_ATACK;
			
								animation.CrossFade ("Take_003_Atack");
			
			
						 
						}*/
		if (animation.isPlaying == false) {
						if (Distance == 0.0f) {
								//Previous_Animation_Play = Current_Animation_Play;
								Current_Animation_Play = ANIMATION_ATACK;
			
								animation.CrossFade ("Take_003_Atack");


								Warrior_Atack_Success_Factor = Random.Range (0.0f, 1.0f);
								if (Warrior_Atack_Success_Factor >= 0.5f) {
										HellCat_LifeBar_Script.HellCat_LifeBar_Value = HellCat_LifeBar_Script.HellCat_LifeBar_Value - 1;
								}
						}
				}


					
		if (Trap_Distance < 0.5f) {
			//Previous_Animation_Play = Current_Animation_Play;
			Current_Animation_Play = ANIMATION_IN_SWAMP;
			
			animation.CrossFade ("Take_007_In_Swamp");
			Destroy(gameObject, 0.8f);

			
			
		}



		if (Swamp_Distance < 0.5f) {
			//Previous_Animation_Play = Current_Animation_Play;
			Current_Animation_Play = ANIMATION_IN_TRAP;
			
			animation.CrossFade ("Take_006_In_Trap");
			Destroy(gameObject, 0.8f);
		
			
			
			
		}



		if ( (Distance < 2.0f) && (Input.GetKeyDown (KeyCode.LeftControl) ))
		{
			//Previous_Animation_Play = Current_Animation_Play;
			Current_Animation_Play = ANIMATION_FALLING;
			animation.CrossFade ("Take_005_Falling");

		
				Destroy(gameObject, 0.8f);


		
			
		}


	

		}

}








