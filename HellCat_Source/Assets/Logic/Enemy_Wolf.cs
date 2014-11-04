using UnityEngine;
using System.Collections;


public class Enemy_Wolf : MonoBehaviour 
{

	//public GameObject Wolf_Game_Object; 
	public Transform Treasure;			// Сундук	
	public Transform Player;			// Игровой персонаж (кошка)
	public Transform Wolf;			// Неигровой персонаж (воин)
	public float Wolf_Scope = 3f;	// Область видимости воина
	public Transform Warrior;
	public Transform Trap ; 
	//public GameObject Wolf_Game_Object;
	private NavMeshAgent Agent;					// Агент навигации по сетке
	public Transform Random_Direction;			// Случайное направление
	private bool Random_Point_Generated = false;// Генерация случайных навигационных точек выключена
	private int Random_Point_Life_Time = 0;		// Время жизни случайных навигационных точек


	public static Vector3 Distance_Enemy_Player2; 
//	public static bool Wolf_Destroyed = false;

	// При запуске
	void Start() 
	{

			
	 

		//Wolf_RigidBody.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		// Создание агента навигации по сетке
		Agent = GetComponent<NavMeshAgent>();
		//animation.AddClip (Animation_Wolf,"Take_001_Razgon" );
		 
	}

	// При обновлении сцены
	void Update()
	{

				//if (Wolf_Destroyed == false) {



		var Trap_Direction_Distance = Trap.position -  Warrior.position;
		Trap_Direction_Distance.y = 0;
		float Trap_Distance = Trap_Direction_Distance.x * Trap_Direction_Distance.x + Trap_Direction_Distance.y * Trap_Direction_Distance.y + Trap_Direction_Distance.z * Trap_Direction_Distance.z;


				var Treasure_Direction_Distance = Treasure.position - Wolf.position;
				float Treasure_Distance = Treasure_Direction_Distance.x * Treasure_Direction_Distance.x + Treasure_Direction_Distance.y * Treasure_Direction_Distance.y + Treasure_Direction_Distance.z * Treasure_Direction_Distance.z;
				// Рассчёт расстояния между кошкой и воином
				var Look_Dir = Player.position - Wolf.position; 

				Look_Dir.y = 0;
				float Distance = Look_Dir.x * Look_Dir.x + Look_Dir.y * Look_Dir.y + Look_Dir.z * Look_Dir.z;
				Distance = Mathf.Sqrt (Distance);
				Treasure_Distance = Mathf.Sqrt (Treasure_Distance);
	
		 





		var  Warrior_Wolf_Distance_Direction = Warrior.position - Wolf.position;
		float Warrior_Wolf_Distance = Warrior_Wolf_Distance_Direction.x * Warrior_Wolf_Distance_Direction.x + Warrior_Wolf_Distance_Direction.y * Warrior_Wolf_Distance_Direction.y + Warrior_Wolf_Distance_Direction.z * Warrior_Wolf_Distance_Direction.z;

		Warrior_Wolf_Distance = Mathf.Sqrt (Warrior_Wolf_Distance);

				if (Trap_Distance < 0.4) {
			Destroy(gameObject, 0.8f);
		}


				//animation.Play("Take_001_Razgon");
				//Wolf_Animation.CrossFade ("Take_001_Razgon");
				//Wolf_Animation.Play;

				// Если кошка попала в область видимости воина, т.е. воин видит кошку - то он идёт к ней
				if ((Distance < Wolf_Scope) && Player_Controller.Hellcat_Mode == false) {

						if (Random_Point_Generated == false) {
								// Получение координат случайной точки на окружности (границе области видимости)
								float dx = Random.Range (-Wolf_Scope, Wolf_Scope);
								float dz = Random.Range (-Wolf_Scope, Wolf_Scope);
								float d = dx * dx + dz * dz;
								d = Mathf.Sqrt (d);
								dx = Wolf_Scope * dx / d;
								dz = Wolf_Scope * dz / d;
				
								// Установка случайного вектора от воина к сгенерированным случайным координатам
								Random_Direction.position = new Vector3 (Wolf.position.x + dx, 0, Wolf.position.z + dz);
								Random_Point_Generated = true;
								Random_Point_Life_Time = 190;
						}
			
						// Воин движится в направлении случайной точки, истекает её срок действия и она исчезает
						Agent.SetDestination (Random_Direction.position);  
						Random_Point_Life_Time--;
						if (Random_Point_Life_Time <= 0) {
								Random_Point_Generated = false;
						}
						Agent.SetDestination (Random_Direction.position); 
				}

		// Если же воин никого не видит, то случайным образом выбирается вариант движения:
		// 1 - воин двигается к сундуку
		// 2 - воин двигается в произвольном направлении
		else {

			if ( Warrior_Wolf_Distance < 3.0f) {
							

				Agent.SetDestination  (Warrior.position) ;

			
						
						} else {









								int Random_Value = Random.Range (1, 3);

								if (Random_Value == 1) {
										Agent.SetDestination (Treasure.position); 

								} else {
										// Если выключена генерация случайных навигационных точек,
										// то генерируется случайная точка, находящаяся на границе области видимости,
										// указывается направление к ней и эта точка "включается"
										if (Random_Point_Generated == false) {
												// Получение координат случайной точки на окружности (границе области видимости)
												float dx = Random.Range (-2 * Wolf_Scope, 2 * Wolf_Scope);
												float dz = Random.Range (-2 * Wolf_Scope, 2 * Wolf_Scope);
												float d = dx * dx + dz * dz;
												d = Mathf.Sqrt (d);
												dx = Wolf_Scope * dx / d;
												dz = Wolf_Scope * dz / d;

												// Установка случайного вектора от воина к сгенерированным случайным координатам
												Random_Direction.position = new Vector3 (Wolf.position.x + dx, 0, Wolf.position.z + dz);
												Random_Point_Generated = true;
												Random_Point_Life_Time = 190;
										}

										// Воин движится в направлении случайной точки, истекает её срок действия и она исчезает
										Agent.SetDestination (Random_Direction.position);  
										Random_Point_Life_Time--;
										if (Random_Point_Life_Time <= 0) {
												Random_Point_Generated = false;
										}
								}

						}



						//if ( Treasure_Distance == 0.0f)
						//{
						//	Application.LoadLevel("Game_Over");
						//}
		
						/*if ( (Distance < 2.0f) && (Input.GetKeyDown (KeyCode.LeftControl) ))
		{
			//Previous_Animation_Play = Current_Animation_Play;
			 Destroy(gameObject);
			
			
		}*/

		 
				


						Distance_Enemy_Player2 = Look_Dir;
				}
		}
		}
//}
