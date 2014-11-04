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

	//режим кошки, false = кошка видна целиком true = кошка ушла под землю
	public static bool Hellcat_Mode = false;  //режим кошки, false = кошка видна целиком true = кошка ушла под землю 

	//массив с номером уровня и координатами границ уровней.
	private  float [,] Level_Borders_Array = new float[99, 5];


	private int    Level_Number_Current; 
	private  float Level_Left_Border_value ;    //Левая граница по оси X уровня
	private  float Level_Right_Border_value ;    //Правая граница по оси X уровня
	private  float Level_Up_Border_value    ;	  //Верхняя граница по оси Z уровня
	private  float Level_Down_Border_value  ; 



	bool Level_Borders_Check ( Vector3 Position)
	{
		bool Inside_Level_Indicator = false;
		
		if (
			((Position.x > Level_Left_Border_value) && (Position.x < Level_Right_Border_value))
			&&
			((Position.z > Level_Up_Border_value) && (Position.z < Level_Down_Border_value))) {
			Inside_Level_Indicator = true;
		} 
		else
		{
			Inside_Level_Indicator = false;
			
		}
		
		return Inside_Level_Indicator;
	}















	void Start ()
	{

		//Границы 1 уровня 
		Level_Borders_Array[1,0] = 1;  //Номер уровня
		Level_Borders_Array[1,1] = -15.0f;  //Левая граница по оси X уровня
		Level_Borders_Array[1,2] = 15.0f;  //Правая граница по оси X уровня
		Level_Borders_Array[1,3] = -15.0f;  //Верхняя граница по оси Z уровня
		Level_Borders_Array[1,4] = 15.0f;  //Нижняя граница по оси Z уровня


		//Границы 2 уровня 
		Level_Borders_Array[2,0] = 2;  //Номер уровня
		Level_Borders_Array[2,1] = 10.0f;  //Левая граница по оси X уровня
		Level_Borders_Array[2,2] = 10.0f;  //Правая граница по оси X уровня
		Level_Borders_Array[2,3] = 10.0f;  //Верхняя граница по оси Z уровня
		Level_Borders_Array[2,4] = 10.0f;  //Нижняя граница по оси Z уровня



		Level_Number_Current = (int)  Level_Borders_Array [1, 0]  ; 
		Level_Left_Border_value = Level_Borders_Array [1, 1];
		Level_Right_Border_value = Level_Borders_Array [1, 2];
		Level_Up_Border_value = Level_Borders_Array [1, 3];
		Level_Down_Border_value = Level_Borders_Array [1, 4];
	}



	// Перед обновлением сцены	
	void FixedUpdate()
	{		

		//считываем со стрелок клавиатуры движения по горизонтали (-1  = влево и 1 = вправо) и вертикали (вверх = 1б вниз = -1)
		MoveHorizontal = Input.GetAxis("Horizontal");
		MoveVertical = Input.GetAxis("Vertical");

		//формируем вектор перемещения и отрабатываем пермещение
		//Vector3 Move = new Vector3(MoveHorizontal, 0.0f, MoveVertical);
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
		float Left_Angle = 180;
		float Right_Angle = 0;
		float Down_Angle = 90;
		float Up_Angle = 270;

		float Up_Right_Angle = 315;
		float Down_Right_Angle = 45;
		float Down_Left_Angle = 135;
		float Up_Left_Angle = 225;

		float Rotation_Angle_Steps = 0;
		float [] Angle_Steps = new float[361];
		float [] Angle_Steps_Difference_with_Angle_Left_Side = new float[361];
		float [] Angle_Steps_Difference_with_Angle_Right_Side = new float[361];
		float  Angle_Steps_Difference_with_Angle_Left_Side_MIN = 0;
		float  Angle_Steps_Difference_with_Angle_Right_Side_MIN =0;

		bool Action = false;

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








		/*
		if ((MoveVertical < 0) && (MoveHorizontal == 0) ) 
			{
			//rigidbody.rotation = Quaternion.Euler (0.0f, 90.0f, 0.0f);
			if ((current_angle < 270.0f) && (current_angle > 90.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y - Angle_Rotation_Speed;
				rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}

			if ((current_angle >= 270.0f) && (current_angle < 360.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y + Angle_Rotation_Speed;
				rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}


			if (current_angle == 360.0f)
			{
				Rotation_Variable = 0;
				rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);

			}

			if ((current_angle >= 0.0f) && (current_angle < 90.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y + Angle_Rotation_Speed;
				rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}


			}
			
			if ((MoveVertical > 0) && (MoveHorizontal == 0)) 
			{
			//	rigidbody.rotation = Quaternion.Euler (0.0f, 270.0f, 0.0f);
			if ((current_angle >= 90.0f) && (current_angle < 270.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y + Angle_Rotation_Speed;
				rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}


		
	

			if ((current_angle < 90.0f) && (current_angle > 0.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y - Angle_Rotation_Speed;
				rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}

			if (current_angle <= 00.0f)
			{
				Rotation_Variable = 360;
				rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
				
			}


			if ((current_angle <= 360.0f) && (current_angle > 270.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y - Angle_Rotation_Speed;
				rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}

		}
			
			if ((MoveVertical == 0) && (MoveHorizontal < 0)) 
			{
				//rigidbody.rotation = Quaternion.Euler (0.0f, 180.0f, 0.0f);
			
			if ((current_angle <= 360.0f) && (current_angle > 180.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y - Angle_Rotation_Speed;
				rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}


			if ((current_angle >=0.0f) && (current_angle < 180.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y + Angle_Rotation_Speed;
				rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}

			
	
			}
			
			if ((MoveVertical == 0) && (MoveHorizontal > 0)) 
			{
				//rigidbody.rotation = Quaternion.Euler (0.0f, 360.0f, 0.0f);
			if ((current_angle >= 180.0f) && (current_angle < 360.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y + Angle_Rotation_Speed;
				rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}


			if ((current_angle < 180.0f) && (current_angle > 0.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y - Angle_Rotation_Speed;
				rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}
			
			}
			
			
			if ((MoveVertical < 0) && (MoveHorizontal < 0)) 
			{
				//rigidbody.rotation = Quaternion.Euler (0.0f, 135.0f, 0.0f);

			if ((current_angle <= 315.0f) && (current_angle > 135.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y - Angle_Rotation_Speed;
				rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}

			if ((current_angle > 315.0f) && (current_angle <= 360.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y + Angle_Rotation_Speed;
				rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}

			if (current_angle >= 360.0f)
			{
				current_angle = 0.0f;

			}

			if ((current_angle >= 0.0f) && (current_angle < 135.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y + Angle_Rotation_Speed;
				rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}


			}
			
			if ((MoveVertical > 0) && (MoveHorizontal > 0)) 
			{
				//rigidbody.rotation = Quaternion.Euler (0.0f, 315.0f, 0.0f);
			if ((current_angle >= 135.0f) && (current_angle < 315.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y + Angle_Rotation_Speed;
				rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}


			if ((current_angle < 135.0f) && (current_angle > 0.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y - Angle_Rotation_Speed;
				rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}


			if (current_angle == 0.0f)
			{
				current_angle = 360.0f;

			}

			if ((current_angle <= 360.0f) && (current_angle > 315.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y - Angle_Rotation_Speed;
				rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}
			
		}
			
			if ((MoveVertical > 0) && (MoveHorizontal < 0)) 
			{
				//rigidbody.rotation = Quaternion.Euler (0.0f, 225.0f, 0.0f);
			if ((current_angle <= 360.0f) && (current_angle > 225.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y - Angle_Rotation_Speed;
				rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}

			if ((current_angle <= 45.0f) && (current_angle > 0.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y - Angle_Rotation_Speed;
				rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}
			
			if (current_angle == 0.0f)
			{

				current_angle = 360.0f;
			}

			
			if ((current_angle <  225.0f) && (current_angle > 45.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y + Angle_Rotation_Speed;
				rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}

			}
			
			if ((MoveVertical < 0) && (MoveHorizontal > 0)) 
			{
				//rigidbody.rotation = Quaternion.Euler (0.0f, 45.0f, 0.0f);
			if ((current_angle >=  225.0f) && (current_angle < 360.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y + Angle_Rotation_Speed;
				rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}


			if (current_angle ==  360.0f) 
			{
				current_angle = 0.0f;

			}

			if ((current_angle >=  0.0f) && (current_angle < 45.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y + Angle_Rotation_Speed;
				rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}

			if ((current_angle <  225.0f) && (current_angle > 45.0f))
			{
				Rotation_Variable = rigidbody.rotation.eulerAngles.y - Angle_Rotation_Speed;
				rigidbody.rotation = Quaternion.Euler (0.0f, Rotation_Variable, 0.0f);
			}
			
		}
		 */

		
			







				
						//формирвоание движения кошки
						if (Level_Borders_Check (rigidbody.position + Move / Speed) == true) {
								if (Hellcat_Mode == false) {			
										//кошка над землей
					rigidbody.position =  rigidbody.position + Move  / Speed;			
								} else {		
										//кошка под землей (быстрее двигается чем над землей)
										rigidbody.position = rigidbody.position + Move / (Speed / 2);
								}
						}
				
				}



	 


	}


