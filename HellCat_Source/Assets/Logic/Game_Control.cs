using UnityEngine;
using System.Collections;

public class Game_Control : MonoBehaviour 
{	
	private string currentLevel;	// Имя текущего уровня
	private int Start_Number = 0;

	// Объекты для управления движением кошкой
	private Player_Controller HellCat_Controller;
	private GameObject HellCat_Object;
	private Camera_Controller MainCamera;

	// Логические размеры элементов
	private float X_Cell;
	private float Y_Cell;

	// Перемещение главного персоража
	private float X_Direction;
	private float Y_Direction;
	private float X_Move;
	private float Y_Move;
	private Vector3 Мove;

	// Джойстик
	public Texture GamePad_Texture;
	private Rect GamePad_Rect;
	public Texture GamePad_Point_Texture;
	private Rect GamePad_Point_Rect;

	void Start()
	{
		PlayerPrefs.SetString ("Level", "");
		HellCat_Object = GameObject.Find ("HellCat(Clone)");
	}

	// При показе интерфейса
	void OnGUI() 
	{		
		X_Cell = Screen.width / 15;
		Y_Cell = Screen.height / 10;

		// В меню: 
		if (Application.loadedLevelName == "Game_Menu")
		{
			currentLevel = PlayerPrefs.GetString("Level");
			if (currentLevel != "") 
			{
				if (GUI.Button (new Rect (6 * X_Cell, 5 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Заново"))
					Application.LoadLevel(currentLevel);
				if (GUI.Button (new Rect (6 * X_Cell, 6 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Загрузить"))
					Application.LoadLevel("Game_Load");
				if (GUI.Button (new Rect (6 * X_Cell, 7 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Выйти"))
				{
					PlayerPrefs.SetString("Level", "");
					Application.Quit();
				}
				if (GUI.Button (new Rect (6 * X_Cell, 8 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), Start_Number.ToString()))
					Application.LoadLevel(currentLevel);
			}
			else
			{
				// Рисуются 2 кнопки: "Играть"(открывает сцену выбора уровня) и "Выйти"(закрывает игру)
				if (GUI.Button (new Rect (6 * X_Cell, 5 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Играть"))
					Application.LoadLevel("Game_Load");
				if (GUI.Button (new Rect (6 * X_Cell, 6.5f * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Выйти"))
				{
					PlayerPrefs.SetString("Level", "");
					Application.Quit();
				}
			}
		}
		
		// В окне загрузки уровней:
		else if (Application.loadedLevelName == "Game_Load")
		{
			// Рисуется прямоугольник меню и кнопки загрузки каждого уровня, 
			// а также в настройках сохраняется, какой уровень был загружен
			currentLevel = "";
			
			GUI.skin.box.fontSize = 20;
			GUI.skin.box.alignment = TextAnchor.UpperCenter;
			GUI.Box (new Rect (4 * X_Cell, 1 * Y_Cell, 7 * X_Cell, 8 * Y_Cell), "Загрузить уровень");

			if (GUI.Button (new Rect (5 * X_Cell, 4f * Y_Cell, 5 * X_Cell, 1 * Y_Cell), "Уровень 1"))
				currentLevel = "Level_01";
			if (GUI.Button (new Rect (5 * X_Cell, 6f * Y_Cell, 5 * X_Cell, 1 * Y_Cell), "Уровень 2"))
				currentLevel = "Level_02";

			if (currentLevel != "") 
			{
				Application.LoadLevel(currentLevel);
				PlayerPrefs.SetString("Level", currentLevel);
			}
		}
		
		// В окне смерти от воина пишется сообщение о смерти и кнопка перехода в меню 
		else if (Application.loadedLevelName == "Game_Over_Killed")
		{
			GUI.skin.box.fontSize = 36;
			GUI.skin.box.alignment = TextAnchor.MiddleCenter;
			GUI.Box(new Rect(1 * X_Cell, 1 * Y_Cell, 13 * X_Cell, 8 * Y_Cell), 
			    "Смерть.\r\nВоин убил кошку.");
			if (GUI.Button (new Rect (6 * X_Cell, 0 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Меню"))
				Application.LoadLevel("Game_Menu");
		}

		// В окне проигрыша пишется сообщение о проигрыше и кнопка перехода в меню 
		else if (Application.loadedLevelName == "Game_Over")
		{
			GUI.skin.box.fontSize = 36;
			GUI.skin.box.alignment = TextAnchor.MiddleCenter;
			GUI.Box(new Rect(1 * X_Cell, 1 * Y_Cell, 13 * X_Cell, 8 * Y_Cell), 
			    "Проигрыш.\r\nВоин забрал сокровище.");
			if (GUI.Button (new Rect (6 * X_Cell, 0 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Меню"))
				Application.LoadLevel("Game_Menu");
		}
		
		// В окне победы пишется сообщение о победе и кнопка перехода в меню
		else if (Application.loadedLevelName == "Game_Winner")
		{
			GUI.skin.box.fontSize = 36;
			GUI.skin.box.alignment = TextAnchor.MiddleCenter;
			GUI.Box(new Rect(1 * X_Cell, 1 * Y_Cell, 13 * X_Cell, 8 * Y_Cell), 
			    "Победа!\r\nВоин загнан в ловушку.");
			if (GUI.Button (new Rect (6 * X_Cell, 0 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Меню"))
				Application.LoadLevel("Game_Menu");
		}

		// В окне любого уровня:
		else
		{
			// Рисуется кнопка: "Пауза"(открывает меню)
			if (GUI.Button (new Rect (5 * X_Cell, 0 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Пауза"))
				Application.LoadLevel("Game_Menu");

			// Кнопка "Камера"
			MainCamera = GameObject.Find("Camera(Clone)").GetComponent<Camera_Controller>(); 
			//MainCamera = GameObject.Find("Camera").GetComponent<Camera_Controller>();
			if (GUI.Button (new Rect (8 * X_Cell, 0 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Камера")) 
				MainCamera.BroadcastMessage("SetCamera");

			if ((Application.loadedLevelName != "Game_Over") 
			 && (Application.loadedLevelName != "Game_Over_Killed") 
			 && (Application.loadedLevelName != "Game_Winner"))
			{
				// Рисование джойстика с наложением на него текстуры
				GamePad_Rect = new Rect (1 * Y_Cell, 6 * Y_Cell, 3 * Y_Cell, 3 * Y_Cell);
				// GamePad_Style.border.Remove(GamePad_Rect);
				GUI.backgroundColor = new Color();
				GUI.Box(GamePad_Rect, GamePad_Texture); //, GamePad_Style);

				 //Кнопка "Режим"

				HellCat_Controller = GameObject.Find("HellCat(Clone)").GetComponent<Player_Controller>();
				//HellCat_Controller = GameObject.Find("Player").GetComponent<Player_Controller>();
				HellCat_Controller = HellCat_Object.GetComponent<Player_Controller>();
				if (GUI.Button (new Rect (11 * X_Cell, 7 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Режим")) 
					HellCat_Controller.BroadcastMessage("Mode");

				// Отслеживание, какая кнопка интерфейса нажата - от этого зависит направление движения
				if (Input.GetMouseButton(0)) 
				{
					float x = Input.mousePosition.x;
					float y = Input.mousePosition.y;


					if (x >= 1 * Y_Cell && x <= 4 * Y_Cell && y >= 1 * Y_Cell && y <= 4 * Y_Cell)
					{
						// Рисование джойстика с наложением на него текстуры
						GamePad_Point_Rect = new Rect (x - 0.5f * Y_Cell, -y + 9f * Y_Cell, 1 * Y_Cell, 1 * Y_Cell);
						GUI.backgroundColor = new Color();
						GUI.Box(GamePad_Point_Rect, GamePad_Point_Texture); //, GamePad_Style);

						X_Direction = x - 2.5f * Y_Cell;
						Y_Direction = y - 2.5f * Y_Cell;
						Moving();
					}
					else 
					{
						// Рисование джойстика с наложением на него текстуры
						GamePad_Point_Rect = new Rect (2 * Y_Cell, 7f * Y_Cell, 1 * Y_Cell, 1 * Y_Cell);
						GUI.backgroundColor = new Color();
						GUI.Box(GamePad_Point_Rect, GamePad_Point_Texture); //, GamePad_Style);

						X_Move = 0;
						Y_Move = 0;
					}
				} 
				else 
				{
					X_Move = 0;
					Y_Move = 0;
					// Рисование джойстика с наложением на него текстуры
					GamePad_Point_Rect = new Rect (2 * Y_Cell, 7f * Y_Cell, 1 * Y_Cell, 1 * Y_Cell);
					GUI.backgroundColor = new Color();
					GUI.Box(GamePad_Point_Rect, GamePad_Point_Texture); //, GamePad_Style);
				}
			}
		}
	}

	// При подготовке к обновлению 
	void FixedUpdate ()
	{
	}

	// Перемещение кошки
	void Moving()
	{	
		// Вычисление смещения кошки по горизонтали
		X_Move = X_Move + X_Direction * Time.deltaTime;
		if (X_Move > 1.0f) X_Move = 1.0f;
		if (X_Move < -1.0f) X_Move = -1.0f;

		// Вычисление смещения кошки по вертикали
		Y_Move = Y_Move + Y_Direction * Time.deltaTime;
		if (Y_Move > 1.0f) Y_Move = 1.0f;
		if (Y_Move < -1.0f) Y_Move = -1.0f;

		Vector3 Move = new Vector3(X_Move, 0.0f, Y_Move);
		
		// Передача информации кошке о том, куда она должна двигаться
		HellCat_Object = GameObject.Find ("HellCat(Clone)");
	
		//HellCat_Object = GameObject.FindGameObjectWithTag("Player");
		HellCat_Controller = HellCat_Object.GetComponent<Player_Controller>();
		HellCat_Controller.HellCat_Object = HellCat_Object;
		HellCat_Controller.BroadcastMessage("Go", Move);	
	
	}	
}


	


