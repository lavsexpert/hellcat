using UnityEngine;
using System.Collections;

public class Enemy_Warrior_Model_Animation : MonoBehaviour {

	public Transform Treasure;			// Сундук	
	public Transform Player;			// Игровой персонаж (кошка)
	public Transform Warrior;			// Неигровой персонаж (воин)
	public float Warrior_Scope = 3f;	// Область видимости воина



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		const int ANIMATION_RAZGON  = 1;
		const int ANIMATION_STOP =2;
		const int ANIMATION_ATACK = 3;
		const int ANIMATION_GO =4;
		const int ANIMATION_FALLING =5;
		const int ANIMATION_IN_TRAP =6;
		const int ANIMATION_IN_SWAMP =7;
		const int ANIMATION_TORMOZIT =8;



		int Current_Animation_Play = ANIMATION_STOP;
		int Previous_Animation_Play = ANIMATION_STOP;


		var Treasure_Direction_Distance = Treasure.position - Warrior.position;
		float Treasure_Distance = Treasure_Direction_Distance.x * Treasure_Direction_Distance.x + Treasure_Direction_Distance.y * Treasure_Direction_Distance.y + Treasure_Direction_Distance.z * Treasure_Direction_Distance.z;
		// Рассчёт расстояния между кошкой и воином
		var Look_Dir = Player.position - Warrior.position; 
		
		Look_Dir.y = 0;
		float Distance = Look_Dir.x*Look_Dir.x + Look_Dir.y*Look_Dir.y + Look_Dir.z*Look_Dir.z;
		Distance = Mathf.Sqrt(Distance);
		Treasure_Distance = Mathf.Sqrt(Treasure_Distance);


	                    if (animation.isPlaying == false)
							{
						if (((Previous_Animation_Play == ANIMATION_RAZGON) || 
								(Previous_Animation_Play == ANIMATION_GO)) &&
								(Distance > 1.0f)) {

								Previous_Animation_Play = Current_Animation_Play;
								Current_Animation_Play = ANIMATION_GO;

								animation.CrossFade ("Take_004_Go");

						}
							}




		if (animation.isPlaying == false) {
						if ((Previous_Animation_Play == ANIMATION_GO) && (Distance < (Warrior_Scope ))) {
								Previous_Animation_Play = Current_Animation_Play;
								Current_Animation_Play = ANIMATION_TORMOZIT;
			
								animation.CrossFade ("Take_008_Tormozit");


						}
				}
 

		if (animation.isPlaying == false) {

						if (((Previous_Animation_Play == ANIMATION_TORMOZIT) || (Previous_Animation_Play == ANIMATION_ATACK)) && (Distance < (1.0f))) {
								Previous_Animation_Play = Current_Animation_Play;
								Current_Animation_Play = ANIMATION_STOP;
			
								animation.CrossFade ("Take_002_Stop");
			
			
						}
				}


		if (animation.isPlaying == false) {
			if ((Previous_Animation_Play == ANIMATION_STOP) && (Distance >= 1.0f)) {
								Previous_Animation_Play = Current_Animation_Play;
								Current_Animation_Play = ANIMATION_RAZGON;
			
								animation.CrossFade ("Take_001_Razgon");
			
			
						}
				}


		if (animation.isPlaying == false) {
						if ((Previous_Animation_Play == ANIMATION_STOP) && (Distance < Warrior_Scope)) {
								Previous_Animation_Play = Current_Animation_Play;
								Current_Animation_Play = ANIMATION_ATACK;
			
								animation.CrossFade ("Take_003_Atack");
			
			
						}
				}

				



}
}