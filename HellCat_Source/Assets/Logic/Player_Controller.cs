using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour 
{
	public MeshFilter HellCat_Mesh_Filter;  //В эту перменную загружается текущая модель кошки
	public Mesh HellCat_Mesh_Mode_One;      //Модель кошки - Модель кошки над землей
	public Mesh HellCat_Mesh_Mode_Two;      //Модель кошки - Модель кошки под землей
 
	public float Speed;              		// Скорость игрока
	private float MoveVertical;				// Перемещение по вертикали
	private float MoveHorizontal;			// Перемещение по горизонтали
//	public Rigidbody rigitbody;

	//режим кошки, false = кошка видна целиком true = кошка ушла под землю
	public static bool Hellcat_Mode = false;

	//Карта и её координаты
	private GameObject Land;
	private double LeftX;
	private double RightX;
	private double UpperZ;
	private double LowerZ;
	
	// При запуске
	void Start()
	{
		// Сохранение краёв карты в переменные
		Land = GameObject.Find("Land");
		LeftX = Land.renderer.bounds.min.x;
		RightX = Land.renderer.bounds.max.x;
		UpperZ = Land.renderer.bounds.min.z;
		LowerZ = Land.renderer.bounds.max.z;
	}

	// Перед обновлением сцены	
	void FixedUpdate()
	{		
		//Считываем со стрелок клавиатуры движения по горизонтали (-1  = влево и 1 = вправо) и вертикали (вверх = 1 и вниз = -1)
		MoveHorizontal = Input.GetAxis("Horizontal");
		MoveVertical = Input.GetAxis("Vertical");
		
		//формируем вектор перемещения и отрабатываем пермещение
		Vector3 Move = new Vector3(MoveHorizontal, 0.0f, MoveVertical);
		Go(Move);
		
		//клавишей ПРОБЕЛ меняем режим кошки
		if (Input.GetKeyDown (KeyCode.Space)) 
		{			
			Mode();
		}		
	}
	
	// При обновлении сцены	
	void Update ()
	{
		//animation.CrossFade ("HellCat_Take_004_Go");
	}

	// Проверка, находится ли кошка в пределах карты
	bool Level_Borders_Check(Vector3 Position)
	{
		bool Inside_Level_Indicator = false;
		
		if (((Position.x > LeftX) && (Position.x < RightX))	&& ((Position.z > UpperZ) && (Position.z < LowerZ))) 
		{
			Inside_Level_Indicator = true;
		} 
		else
		{
			Inside_Level_Indicator = false;
		}
		
		return Inside_Level_Indicator;
	}

	// Изменение положения кошки (режим кошки)
	public void Mode()
	{
		//Кошка под землей
		if (Hellcat_Mode == false) 
		{
			Hellcat_Mode = true;
			rigidbody.isKinematic = true;
			HellCat_Mesh_Filter.mesh = HellCat_Mesh_Mode_Two;
		} 
		else 
		{
			//Кошка над землей
			Hellcat_Mode = false;
			rigidbody.isKinematic = false;
			HellCat_Mesh_Filter.mesh = HellCat_Mesh_Mode_One;
		}
	}
	
	// Перемещение кошки
	public void Go(Vector3 Move)
	{
		float current_angle = rigidbody.rotation.eulerAngles.y;
		float Angle_Rotation_Speed = 5.0f;
		float Rotation_Variable;

		//------------------------------------------------------------------
		//-------NOT USED CODE . ATTEMPT OF AUTOMATIZATION
		//------------------------------------------------------------------
//		float Left_Angle = 180f;
//		float Right_Angle = 0f;
//		float Down_Angle = 90f;
//		float Up_Angle = 270f;
//
//		float Up_Right_Angle = 315f;
		float Down_Right_Angle = 45f;
//		float Down_Left_Angle = 135f;
//		float Up_Left_Angle = 225f;

		float Rotation_Angle_Steps = 0;
		float [] Angle_Steps = new float[361];
		float [] Angle_Steps_Difference_with_Angle_Left_Side = new float[361];
		float [] Angle_Steps_Difference_with_Angle_Right_Side = new float[361];
		float  Angle_Steps_Difference_with_Angle_Left_Side_MIN = 0;
		float  Angle_Steps_Difference_with_Angle_Right_Side_MIN =0;

		for( int Counter = 0; Counter < 361 ; Counter++ )
		{
			Angle_Steps [ Counter ] = Rotation_Angle_Steps + Angle_Rotation_Speed;
			Angle_Steps_Difference_with_Angle_Left_Side [Counter] = Down_Right_Angle - Angle_Steps [ Counter ]; 
			Angle_Steps_Difference_with_Angle_Right_Side  [Counter] = Angle_Steps [ Counter ] - Down_Right_Angle; 
		}

		Angle_Steps_Difference_with_Angle_Left_Side_MIN = Angle_Steps_Difference_with_Angle_Left_Side [0];
		Angle_Steps_Difference_with_Angle_Right_Side_MIN = Angle_Steps_Difference_with_Angle_Right_Side [0];
		for( int Counter = 0; Counter < 361 ; Counter++ )
		{

			if  ( Angle_Steps_Difference_with_Angle_Left_Side [Counter] < Angle_Steps_Difference_with_Angle_Left_Side_MIN )
			{
				Angle_Steps_Difference_with_Angle_Left_Side_MIN = Angle_Steps_Difference_with_Angle_Left_Side[Counter];

			}

			if (	Angle_Steps_Difference_with_Angle_Right_Side  [Counter]  < Angle_Steps_Difference_with_Angle_Right_Side_MIN )
			{

				Angle_Steps_Difference_with_Angle_Right_Side_MIN  = Angle_Steps_Difference_with_Angle_Right_Side  [Counter] ;
			}
		}
		//------------------------------------------------------------------
		//-------NOT USED CODE . ATTEMPT OF AUTOMATIZATION
		//------------------------------------------------------------------


		Rotation_Variable = rigidbody.rotation.eulerAngles.y;



		//-----------------------------------------------------------------
		// ------------------------- Go DOWN  -----------------------------
		//-----------------------------------------------------------------
		if ((MoveVertical < 0) && (MoveHorizontal == 0)  )
		{
			//rigidbody.rotation = Quaternion.Euler (0.0f, 90.0f, 0.0f);



		
			if ((current_angle < 270.0f) && (current_angle > 90.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y - Angle_Rotation_Speed;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
				
			}
			
		

			
			 
			if ((current_angle >= 270.0f) && (current_angle < 360.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y + Angle_Rotation_Speed;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			
		 
			}


	
			if (current_angle == 360.0f)
			{
				Rotation_Variable = 0.0f;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			
				
			}
			

			
		
			if ((current_angle >= 0.0f) && (current_angle < 90.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y + Angle_Rotation_Speed;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
				
			}
			

			

	
			if ((current_angle >= 90.0f -Angle_Rotation_Speed ) && (current_angle <= 90.0f +Angle_Rotation_Speed ))
			{
				Rotation_Variable = 90.0f;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);

			
			}
		
			//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
		}
		//-----------------------------------------------------------------
		// ------------------------- Go DOWN  -----------------------------
		//-----------------------------------------------------------------









		//-----------------------------------------------------------------
		// ------------------------- Go UP  -----------------------------
		//-----------------------------------------------------------------
		if ((MoveVertical > 0) && (MoveHorizontal == 0)) 
		{
			//	rigidbody.rotation = Quaternion.Euler (0.0f, 270.0f, 0.0f);
			if ((current_angle >= 90.0f) && (current_angle < 270.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y + Angle_Rotation_Speed;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}
			
			
			
			
			
			if ((current_angle < 90.0f) && (current_angle > 0.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y - Angle_Rotation_Speed;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}
			
			if (current_angle <= 00.0f)
			{
				Rotation_Variable = 360;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
				
			}
			
			
			if ((current_angle <= 360.0f) && (current_angle > 270.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y - Angle_Rotation_Speed;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}


			if ((current_angle <= 270.0f + Angle_Rotation_Speed ) && (current_angle >= 270.0f - Angle_Rotation_Speed))
			{
				Rotation_Variable =  270.0f;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}

			//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
		}
		//-----------------------------------------------------------------
		// ------------------------- Go UP  -----------------------------
		//-----------------------------------------------------------------




		//-----------------------------------------------------------------
		// ------------------------- Go LEFT  -----------------------------
		//-----------------------------------------------------------------
		if ((MoveVertical == 0) && (MoveHorizontal < 0)) 
		{
			//rigidbody.rotation = Quaternion.Euler (0.0f, 180.0f, 0.0f);
			
			if ((current_angle <= 360.0f) && (current_angle > 180.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y - Angle_Rotation_Speed;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}


			
			if ((current_angle >=0.0f) && (current_angle < 180.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y + Angle_Rotation_Speed;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}

			
			if ((current_angle >=180.0f - Angle_Rotation_Speed) && (current_angle <= 180.0f + Angle_Rotation_Speed))
			{
				Rotation_Variable = 180.0f;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}
			//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			
			
		}
		//-----------------------------------------------------------------
		// ------------------------- Go LEFT  -----------------------------
		//-----------------------------------------------------------------







		//-----------------------------------------------------------------
		// ------------------------- Go RIGHT  -----------------------------
		//-----------------------------------------------------------------
		if ((MoveVertical == 0) && (MoveHorizontal > 0)) 
		{
			//rigidbody.rotation = Quaternion.Euler (0.0f, 360.0f, 0.0f);
			if ((current_angle >= 180.0f) && (current_angle < 360.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y + Angle_Rotation_Speed;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}
			
			
			if ((current_angle < 180.0f) && (current_angle > 0.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y - Angle_Rotation_Speed;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}

			if ((current_angle >= 360.0f - Angle_Rotation_Speed) && (current_angle <= 360.0f + Angle_Rotation_Speed))
			{
				Rotation_Variable = 0.0f;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}
	
			if ((current_angle >= 0.0f - Angle_Rotation_Speed) && (current_angle <= 0.0f + Angle_Rotation_Speed))
			{
				Rotation_Variable = 0.0f;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}

			//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
		}
		//-----------------------------------------------------------------
		// ------------------------- Go RIGHT  -----------------------------
		//-----------------------------------------------------------------
		






		//-----------------------------------------------------------------
		// ------------------------- Go LEFT-DOWM  -----------------------------
		//-----------------------------------------------------------------
		if ((MoveVertical < 0) && (MoveHorizontal < 0)) 
		{
			//rigidbody.rotation = Quaternion.Euler (0.0f, 135.0f, 0.0f);
			
			if ((current_angle <= 315.0f) && (current_angle > 135.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y - Angle_Rotation_Speed;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}
			
			if ((current_angle > 315.0f) && (current_angle <= 360.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y + Angle_Rotation_Speed;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}
			
			if (current_angle >= 360.0f)
			{
				current_angle = 0.0f;
				
			}
			
			if ((current_angle >= 0.0f) && (current_angle < 135.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y + Angle_Rotation_Speed;
			//	rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}



			if ((current_angle >= 135.0f - Angle_Rotation_Speed) && (current_angle <= 135.0f + Angle_Rotation_Speed ))
			{
				Rotation_Variable = 135.0f;
				//	rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}

		//	rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f)
		}
		//-----------------------------------------------------------------
		// ------------------------- Go LEFT-DOWM  -----------------------------
		//-----------------------------------------------------------------


		//-----------------------------------------------------------------
		// ------------------------- Go RIGHT-UP  -----------------------------
		//-----------------------------------------------------------------
		if ((MoveVertical > 0) && (MoveHorizontal > 0)) 
		{
			//rigidbody.rotation = Quaternion.Euler (0.0f, 315.0f, 0.0f);
			if ((current_angle >= 135.0f) && (current_angle < 315.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y + Angle_Rotation_Speed;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}
			
			
			if ((current_angle < 135.0f) && (current_angle > 0.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y - Angle_Rotation_Speed;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}
			
			
			if (current_angle == 0.0f)
			{
				current_angle = 360.0f;
				
			}
			
			if ((current_angle <= 360.0f) && (current_angle > 315.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y - Angle_Rotation_Speed;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}

			if ((current_angle >= 315.0f - Angle_Rotation_Speed ) && (current_angle <= 315.0f + Angle_Rotation_Speed ))
			{
				Rotation_Variable = 315.0f;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}
			//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			
		}
		//-----------------------------------------------------------------
		// ------------------------- Go RIGHT-UP  -----------------------------
		//-----------------------------------------------------------------





		//-----------------------------------------------------------------
		// ------------------------- Go LEFT-UP  -----------------------------
		//-----------------------------------------------------------------

		if ((MoveVertical > 0) && (MoveHorizontal < 0)) 
		{
			//rigidbody.rotation = Quaternion.Euler (0.0f, 225.0f, 0.0f);
			if ((current_angle <= 360.0f) && (current_angle > 225.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y - Angle_Rotation_Speed;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}
			
			if ((current_angle <= 45.0f) && (current_angle > 0.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y - Angle_Rotation_Speed;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}
			
			if (current_angle == 0.0f)
			{
				
				current_angle = 360.0f;
			}
			
			
			if ((current_angle <  225.0f) && (current_angle > 45.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y + Angle_Rotation_Speed;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}

			if ((current_angle >=  225.0f - Angle_Rotation_Speed) && (current_angle <= 225.0f + Angle_Rotation_Speed ))
			{
				Rotation_Variable = 225.0f;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}

		//	rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
		}
		//-----------------------------------------------------------------
		// ------------------------- Go LEFT-UP  -----------------------------
		//-----------------------------------------------------------------







		//-----------------------------------------------------------------
		// ------------------------- Go RIGHT-DOWN  -----------------------------
		//-----------------------------------------------------------------
		if ((MoveVertical < 0) && (MoveHorizontal > 0)) 
		{
			//rigidbody.rotation = Quaternion.Euler (0.0f, 45.0f, 0.0f);
			if ((current_angle >=  225.0f) && (current_angle < 360.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y + Angle_Rotation_Speed;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}
			
			
			if (current_angle ==  360.0f) 
			{
				current_angle = 0.0f;
				
			}
			
			if ((current_angle >=  0.0f) && (current_angle < 45.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y + Angle_Rotation_Speed;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}
			
			if ((current_angle <  225.0f) && (current_angle > 45.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y - Angle_Rotation_Speed;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}

			if ((current_angle >= 45.0f - Angle_Rotation_Speed) && (current_angle <= 45.0f + Angle_Rotation_Speed))
			{
				Rotation_Variable = 45.0f;
				//rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}

		//	rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
		}	
		//-----------------------------------------------------------------
		// ------------------------- Go RIGHT-DOWN  -----------------------------
		//-----------------------------------------------------------------


		rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable , 0.0f);

		// Формирование движения кошки
		if (Level_Borders_Check(rigidbody.position + Move / Speed) == true) 
			{
			if (Hellcat_Mode == false) 
				{			
					//кошка над землей
					rigidbody.position = rigidbody.position + Move / Speed;			
				} 
				else 
				{		
					//кошка под землей (быстрее двигается чем над землей)
					rigidbody.position = rigidbody.position + Move / (Speed / 2);
				}
			}
		}
	}


