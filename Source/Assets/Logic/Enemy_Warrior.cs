using UnityEngine;
using System.Collections;


public class Enemy_Warrior : MonoBehaviour 
{


	public Transform Treasure;			// Сундук	
	public Transform Player;			// Игровой персонаж (кошка)
	public Transform Warrior;			// Неигровой персонаж (воин)
	public float Warrior_Scope = 3f;	// Область видимости воина



	private NavMeshAgent Agent;					// Агент навигации по сетке
	public Transform Random_Direction;			// Случайное направление
	private bool Random_Point_Generated = false;// Генерация случайных навигационных точек выключена
	private int Random_Point_Life_Time = 0;		// Время жизни случайных навигационных точек


	public static Vector3 Distance_Enemy_Player; 


	// При запуске
	void Start() 
	{

			
	 

		//Warrior_RigidBody.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		// Создание агента навигации по сетке
		Agent = GetComponent<NavMeshAgent>();
		//animation.AddClip (Animation_Warrior,"Take_001_Razgon" );
		 
	}

	// При обновлении сцены
	void Update()
	{

	


		var Treasure_Direction_Distance = Treasure.position - Warrior.position;
		float Treasure_Distance = Treasure_Direction_Distance.x * Treasure_Direction_Distance.x + Treasure_Direction_Distance.y * Treasure_Direction_Distance.y + Treasure_Direction_Distance.z * Treasure_Direction_Distance.z;
		// Рассчёт расстояния между кошкой и воином
		var Look_Dir = Player.position - Warrior.position; 

		Look_Dir.y = 0;
		float Distance = Look_Dir.x*Look_Dir.x + Look_Dir.y*Look_Dir.y + Look_Dir.z*Look_Dir.z;
		Distance = Mathf.Sqrt(Distance);
		Treasure_Distance = Mathf.Sqrt(Treasure_Distance);

		 



		//animation.Play("Take_001_Razgon");
		//Warrior_Animation.CrossFade ("Take_001_Razgon");
		//Warrior_Animation.Play;

		// Если кошка попала в область видимости воина, т.е. воин видит кошку - то он идёт к ней
		if ( (Distance < Warrior_Scope) && Player_Controller.Hellcat_Mode == false)
		{

			Agent.SetDestination(Player.position); 
		}

		// Если же воин никого не видит, то случайным образом выбирается вариант движения:
		// 1 - воин двигается к сундуку
		// 2 - воин двигается в произвольном направлении
		else
		{
			int Random_Value = Random.Range(1, 3);

			if (Random_Value == 1) 
			{
				Agent.SetDestination(Treasure.position); 

			}
			else
			{
				// Если выключена генерация случайных навигационных точек,
				// то генерируется случайная точка, находящаяся на границе области видимости,
				// указывается направление к ней и эта точка "включается"
				if (Random_Point_Generated == false)
				{
					// Получение координат случайной точки на окружности (границе области видимости)
					float dx = Random.Range(-Warrior_Scope, Warrior_Scope);
					float dz = Random.Range(-Warrior_Scope, Warrior_Scope);
					float d = dx*dx + dz*dz;
					d = Mathf.Sqrt(d);
					dx = Warrior_Scope * dx / d;
					dz = Warrior_Scope * dz / d;

					// Установка случайного вектора от воина к сгенерированным случайным координатам
					Random_Direction.position = new Vector3(Warrior.position.x + dx, 0, Warrior.position.z + dz);
					Random_Point_Generated = true;
					Random_Point_Life_Time = 30;
				}

				// Воин движится в направлении случайной точки, истекает её срок действия и она исчезает
				Agent.SetDestination(Random_Direction.position);  
				Random_Point_Life_Time--;
				if (Random_Point_Life_Time <= 0 )
				{
					Random_Point_Generated = false;
				}
			}

		}



		//if ( Treasure_Distance == 0.0f)
		//{
		//	Application.LoadLevel("Game_Over");
		//}


		Distance_Enemy_Player = Look_Dir;
	}
}
