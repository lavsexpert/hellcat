using UnityEngine;
using System.Collections;

public class Game_Control : MonoBehaviour 
{	
	private string currentLevel;	// Имя текущего уровня

	// Объекты для управления движением кошкой
	private Player_Controller Player;
	private short X_Direction;
	private short Y_Direction;
	private float X_Cell;
	private float Y_Cell;
	private float X_Move;
	private float Y_Move;
	private Vector3 Мove;
	public static bool Hellcat_Mode = false;

	// При показе интерфейса
	void OnGUI() 
	{		
		X_Cell = Screen.width / 15;
		Y_Cell = Screen.height / 10;

		// В меню: 
		if (Application.loadedLevelName == "Game_Menu")
		{
			// Рисуются 2 кнопки: "Играть"(открывает сцену выбора уровня) и "Выйти"(закрывает игру)
			if (GUI.Button (new Rect (6 * X_Cell, 5 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Играть"))
				Application.LoadLevel("Game_Load");
			if (GUI.Button (new Rect (6 * X_Cell, 6.5f * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Выйти"))
				Application.Quit();
		}
		
		// В окне загрузки уровней:
		else if (Application.loadedLevelName == "Game_Load")
		{
			// Рисуется прямоугольник меню и кнопки загрузки каждого уровня, 
			// а также в настройках сохраняется, какой уровень был загружен
			currentLevel = "";
			
			GUI.Box (new Rect (4 * X_Cell, 1 * Y_Cell, 7 * X_Cell, 8 * Y_Cell), "Загрузить уровень");

			if (GUI.Button (new Rect (5 * X_Cell, 3 * Y_Cell, 5 * X_Cell, 1 * Y_Cell), "Уровень 1"))
				currentLevel = "Level_01";
			if (GUI.Button (new Rect (5 * X_Cell, 5 * Y_Cell, 5 * X_Cell, 1 * Y_Cell), "Демо 1 (уровень)"))
				currentLevel = "Demo_01";
			if (GUI.Button (new Rect (5 * X_Cell, 7 * Y_Cell, 5 * X_Cell, 1 * Y_Cell), "Демо 2 (карта)"))
				currentLevel = "Demo_02";

			if (currentLevel != "") 
			{
				Application.LoadLevel(currentLevel);
				PlayerPrefs.SetString("Level", currentLevel);
			}
		}

		// В окне карты:
		else if (Application.loadedLevelName == "Game_Map")
		{
			// Рисуется кнопка "Назад", которая возвращает к ранее сохранённому в настройках уровню
			if (GUI.Button (new Rect (0 * Y_Cell, 0 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Назад"))
			{
				currentLevel = PlayerPrefs.GetString("Level");
				if (currentLevel != "") 
				{
					Application.LoadLevel(currentLevel);
				}
			}

			// А также рисуются кнопки "Загрузить", "Перезапустить", "Выйти"
			if (GUI.Button (new Rect (4 * X_Cell, 0 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Загрузить"))
				Application.LoadLevel("Game_Load");
			if (GUI.Button (new Rect (8 * X_Cell, 0 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Заново"))
			{
				currentLevel = PlayerPrefs.GetString("Level");
				if (currentLevel != "") 
				{
					Application.LoadLevel(currentLevel);
				}
			}
			if (GUI.Button (new Rect (12 * X_Cell, 0 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Выйти"))
				Application.Quit();
		}

		// В окне любого уровня:
		else
		{
			// Рисуются 4 кнопки: "Карта"(открывает карту), "Загрузить"(вызывает окно загрузки уровней),
			// "Заново"(перезапускает уровень), "Выйти"(выход из игры)
			if (GUI.Button (new Rect (0 * X_Cell, 0 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Карта"))
				Application.LoadLevel("Game_Map");
			if (GUI.Button (new Rect (4 * X_Cell, 0 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Загрузить"))
				Application.LoadLevel("Game_Load");
			if (GUI.Button (new Rect (8 * X_Cell, 0 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Заново"))
			{
				currentLevel = PlayerPrefs.GetString("Level");
				if (currentLevel != "") 
				{
					Application.LoadLevel(currentLevel);
				}
			}
			if (GUI.Button (new Rect (12 * X_Cell, 0 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Выйти"))
				Application.Quit();

			if ((Application.loadedLevelName != "Game_Over") && (Application.loadedLevelName != "Game_Winner"))
			{
				// Управление движением кошки
				Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
				if (GUI.Button (new Rect (1 * X_Cell, 8 * Y_Cell, 1 * X_Cell, 1 * Y_Cell), "<")) X_Direction = -1;
				if (GUI.Button (new Rect (3 * X_Cell, 8 * Y_Cell, 1 * X_Cell, 1 * Y_Cell), ">")) X_Direction = 1;
				if (GUI.Button (new Rect (2 * X_Cell, 7 * Y_Cell, 1 * X_Cell, 1 * Y_Cell), "^")) Y_Direction = -1;
				if (GUI.Button (new Rect (2 * X_Cell, 9 * Y_Cell, 1 * X_Cell, 1 * Y_Cell), "v")) Y_Direction = 1;

				if (GUI.Button (new Rect (1 * X_Cell, 7 * Y_Cell, 1 * X_Cell, 1 * Y_Cell), "")) {X_Direction = -1; Y_Direction = -1;}
				if (GUI.Button (new Rect (3 * X_Cell, 7 * Y_Cell, 1 * X_Cell, 1 * Y_Cell), "")) {X_Direction = 1; Y_Direction = -1;}
				if (GUI.Button (new Rect (1 * X_Cell, 9 * Y_Cell, 1 * X_Cell, 1 * Y_Cell), "")) {X_Direction = -1; Y_Direction = 1;}
				if (GUI.Button (new Rect (3 * X_Cell, 9 * Y_Cell, 1 * X_Cell, 1 * Y_Cell), "")) {X_Direction = 1; Y_Direction = 1;}

				if (GUI.Button (new Rect (11 * X_Cell, 8 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Режим")) Player.BroadcastMessage("Mode");
			}

		}
		
	}

	// При подготовке к обновлению 
	void FixedUpdate ()
	{
		// Отслеживание, какая кнопка интерфейса нажата - от этого зависит направление движения
		if (Input.GetMouseButton(0)) 
		{
			float x = Input.mousePosition.x;
			float y = Input.mousePosition.y;
			// Вверх
			if (x >= 1 * X_Cell && x <= 2 * X_Cell && y >= 1 * Y_Cell && y <= 2 * Y_Cell)
			{
				X_Direction = -1;
				Y_Direction = 0;
				Moving();
			}
			// Вниз
			else if (x >= 3 * X_Cell && x <= 4 * X_Cell && y >= 1 * Y_Cell && y <= 2 * Y_Cell)
			{
				X_Direction = 1;
				Y_Direction = 0;
				Moving ();
			}
			// Вправо
			else if (x >= 2 * X_Cell && x <= 3 * X_Cell && y >= 2 * Y_Cell && y <= 3 * Y_Cell)
			{
				X_Direction = 0;
				Y_Direction = 1;
				Moving();
			}
			// Влево
			else if (x >= 2 * X_Cell && x <= 3 * X_Cell && y >= 0 * Y_Cell && y <= 1 * Y_Cell)
			{
				X_Direction = 0;
				Y_Direction = -1;
				Moving();
			}
			// Влево вверх
			else if (x >= 1 * X_Cell && x <= 2 * X_Cell && y >= 2 * Y_Cell && y <= 3 * Y_Cell)
			{
				X_Direction = -1;
				Y_Direction = 1;
				Moving();
			}
			// Вправо вверх
			else if (x >= 3 * X_Cell && x <= 4 * X_Cell && y >= 2 * Y_Cell && y <= 3 * Y_Cell)
			{
				X_Direction = 1;
				Y_Direction = 1;
				Moving ();
			}
			// Влево вниз
			else if (x >= 1 * X_Cell && x <= 2 * X_Cell && y >= 0 * Y_Cell && y <= 1 * Y_Cell)
			{
				X_Direction = -1;
				Y_Direction = -1;
				Moving();
			}
			// Вправо вниз
			else if (x >= 3 * X_Cell && x <= 4 * X_Cell && y >= 0 * Y_Cell && y <= 1 * Y_Cell)
			{
				X_Direction = 1;
				Y_Direction = -1;
				Moving();
			}
			else
			{
				X_Direction = 0;
				Y_Direction = 0;
				X_Move = 0;
				Y_Move = 0;
			}
		} 
		else 
		{
			X_Move = 0;
			Y_Move = 0;
		}
	}

	// Перемещение кошки
	void Moving()
	{	
		// Вычисление смещания кошки по горизонтали
		X_Move = X_Move + X_Direction * Time.deltaTime;
		if (X_Move > 1.0f) X_Move = 1.0f;
		if (X_Move < -1.0f) X_Move = -1.0f;

		// Вычисление смещания кошки по вертикали
		Y_Move = Y_Move + Y_Direction * Time.deltaTime;
		if (Y_Move > 1.0f) Y_Move = 1.0f;
		if (Y_Move < -1.0f) Y_Move = -1.0f;

		Vector3 Move = new Vector3(X_Move, 0.0f, Y_Move);
		
		// Передача информации шару о том, куда ему нужно двигаться
		Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
		Player.BroadcastMessage("Go", Move);		
	}
	
}


	


