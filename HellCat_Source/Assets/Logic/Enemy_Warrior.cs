using UnityEngine;
using System.Collections;

public class Enemy_Warrior : MonoBehaviour 
{
	public GameObject Warrior_Game_Object;		// Объект воина
	public Transform Trap; 						// Ловушка
	public Transform Swamp;						// Болото
	public Transform Treasure;					// Сундук	
	public Transform Hellcat;					// Игровой персонаж (кошка)
	public Transform Warrior;					// Неигровой персонаж (воин)
	public float Warrior_Scope = 3f;			// Область видимости воина

	private NavMeshAgent Agent;					// Агент навигации по сетке
	public Transform Random_Direction;			// Случайное направление
	private bool Random_Point_Generated = false;// Генерация случайных навигационных точек выключена
	private int Random_Point_Life_Time = 0;		// Время жизни случайных навигационных точек

	public static bool Warrior_Destroyed = false;
	private string Current;
	private string Previous;

	// При запуске
	void Start() 
	{
		// Создание агента навигации по сетке
		Agent = GetComponent<NavMeshAgent>();
	}

	// При обновлении сцены
	void Update()
	{
		// Вычисление расстояний
//		float Treasure_Distance = Distance2D(Treasure, Warrior);
		float Trap_Distance 	= Distance2D(Trap, Warrior);
		float Swamp_Distance 	= Distance2D(Swamp, Warrior);
		float Distance			= Distance2D(Hellcat, Warrior);

		// Если кошка попала в область видимости воина, т.е. воин видит кошку - то он идёт к ней
		if ((Distance < Warrior_Scope) && Player_Controller.Hellcat_Mode == false) 
		{
			Agent.SetDestination (Hellcat.position); 
		}
		
		// Если же воин никого не видит, то случайным образом выбирается вариант движения:
		// 1 - воин двигается к сундуку
		// 2 - воин двигается в произвольном направлении
		else 
		{
			int Random_Value = Random.Range (1, 3);
			
			if (Random_Value == 1) 
			{
				Agent.SetDestination (Treasure.position); 
			} 
			else 
			{
				// Если выключена генерация случайных навигационных точек,
				// то генерируется случайная точка, находящаяся на границе области видимости,
				// указывается направление к ней и эта точка "включается"
				if (Random_Point_Generated == false) 
				{
					// Получение координат случайной точки на окружности (границе области видимости)
					float dx = Random.Range (-Warrior_Scope, Warrior_Scope);
					float dz = Random.Range (-Warrior_Scope, Warrior_Scope);
					float d = dx * dx + dz * dz;
					d = Mathf.Sqrt (d);
					dx = Warrior_Scope * dx / d;
					dz = Warrior_Scope * dz / d;
					
					// Установка случайного вектора от воина к сгенерированным случайным координатам
					Random_Direction.position = new Vector3 (Warrior.position.x + dx, 0, Warrior.position.z + dz);
					Random_Point_Generated = true;
					Random_Point_Life_Time = 30;
				}
				
				// Воин движится в направлении случайной точки, истекает её срок действия и она исчезает
				Agent.SetDestination (Random_Direction.position);  
				Random_Point_Life_Time--;
				if (Random_Point_Life_Time <= 0) 
				{
					Random_Point_Generated = false;
				}
			}
		}

		Current = "Warrior_04_Go";
		Previous = "Warrior_04_Go";

		// Если анимация не проигрывается - то проигрываем анимацию движения или анимацию стояния на месте
		if (animation.isPlaying == false) 
		{
			Previous = Current;
			
			// После начала ходьбы и ходьбы - ходьба
			if (((Previous == "Warrior_01_Stand") || (Previous == "Warrior_04_Go")) && (Distance >= 0.05f)) 
			{
				PlayAnimation("Warrior_04_Go");
			}
			
			// После хотьбы - остановка и стояние на месте
			if ((Previous == "Warrior_04_Go") && ((Distance < (0.05f)) && (Distance > (0.0f)))) 
			{
				PlayAnimation("Warrior_08_Stop");
				animation.PlayQueued ("Warrior_02_Stand");
			}
		}
		
		// После торможения и атаки - анимация стояния
		if (((Previous == "Warrior_08_Stop") || (Previous == "Warrior_03_Attack")) && (Distance == 0.0f)) 
		{
			PlayAnimation("Warrior_02_Stand", true);
		}
		
		// После стояния - разгон
		if ((Previous == "Warrior_02_Stand") && (Distance > 0.0f)) 
		{
			PlayAnimation("Warrior_01_Start", true);
		}
		
		//		// После стояния - атака
		//		if ((Previous == "Warrior_02_Stand") && (Distance == 0.0f)) 
		//		{
		//			PlayAnimation("Warrior_03_Attack");
		//		}
		
		if (animation.isPlaying == false) 
		{
			// Если подошёл в плотную - начинает атаковать
			if (Distance == 0.0f) 
			{
				animation["Warrior_03_Attack"].speed = 0.2f;
				animation["Warrior_02_Stand"].speed = 0.001f;
				PlayAnimation("Warrior_03_Attack");
				animation.CrossFadeQueued ("Warrior_02_Stand");
			} 
			
			// Если идёт атака, то вычисляется расстояние от воина до кошки, и если 0 - кошка теряет жизнь
			if (Current == "Warrior_03_Attack")
			{
				Distance = Distance2D(Hellcat, Warrior);
				if (Distance == 0.0f)
				{
					Player_LifeBar.Lifes = Player_LifeBar.Lifes - 1;
				}
			}
		}
		
		// Если до болота меньше 0.5 - то воин тонет и уничтожается
		if (Swamp_Distance < 0.5f) 
		{
			PlayAnimation("Warrior_07_In_Swamp");
			Destroy(gameObject, 1.8f);
			//Enemy_Warrior.Warrior_Destroyed = true;
		}
		
		// Если до ловушки меньше 0.4 - то воин начинает падать в ловушку и уничтожается
		if (Trap_Distance < 0.4f) 
		{
			PlayAnimation("Warrior_06_In_Trap");
			Destroy(gameObject, 0.8f);
			//Enemy_Warrior.Warrior_Destroyed = true;
		}
		
		// Если от воина до кошки меньше 2.0 и нажат левый Ctrl - то воин падает в ловушку и нуичтожается
		if ((Distance < 2.0f) && (Input.GetKeyDown (KeyCode.LeftControl)))
		{
			PlayAnimation("Warrior_05_Falling");
			Destroy(gameObject, 0.8f);
			//Enemy_Warrior.Warrior_Destroyed = true;
		}
	}

	// Вычисление расстояния между 2мя объектами в плоскости
	float Distance2D(Transform First_Object, Transform Second_Object)
	{
		var Direction = First_Object.position - Second_Object.position;
		Direction.y = 0;
		float Distance = Direction.x * Direction.x + Direction.y * Direction.y + Direction.z * Direction.z;
		Mathf.Sqrt(Distance);
		return Distance;
	}

	// Анимация и установка переменных Current и Previous
	void PlayAnimation(string AnimationName, bool SetPrevious = false)
	{
		if (SetPrevious) Previous = Current;
		Current = AnimationName;
		animation.CrossFade (Current);
	}

}

