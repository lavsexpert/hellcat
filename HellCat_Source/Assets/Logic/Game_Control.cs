using UnityEngine;
using System.Collections;

public class Game_Control : MonoBehaviour 
{	
	private string currentLevel;	// Имя текущего уровня

	// Объекты для управления движением кошкой
	private Player_Controller HellCat;
	private Camera_Controller MainCamera;
	private short X_Direction;
	private short Y_Direction;
	private float X_Cell;
	private float Y_Cell;
	private float X_Move;
	private float Y_Move;
	private Vector3 Мove;

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
				if (GUI.Button (new Rect (6 * X_Cell, 6 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Продолжить"))
					Application.LoadLevel(currentLevel);
				if (GUI.Button (new Rect (6 * X_Cell, 7 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Загрузить"))
					Application.LoadLevel("Game_Load");
				if (GUI.Button (new Rect (6 * X_Cell, 8f * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Выйти"))
					Application.Quit();
			}
			else
			{
				// Рисуются 2 кнопки: "Играть"(открывает сцену выбора уровня) и "Выйти"(закрывает игру)
				if (GUI.Button (new Rect (6 * X_Cell, 5 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Играть"))
					Application.LoadLevel("Game_Load");
				if (GUI.Button (new Rect (6 * X_Cell, 6.5f * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Выйти"))
					Application.Quit();
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

			if (GUI.Button (new Rect (5 * X_Cell, 2.5f * Y_Cell, 5 * X_Cell, 1 * Y_Cell), "Уровень 1"))
				currentLevel = "Level_01";
			if (GUI.Button (new Rect (5 * X_Cell, 4f * Y_Cell, 5 * X_Cell, 1 * Y_Cell), "Уровень 2"))
				currentLevel = "Level_02";
			if (GUI.Button (new Rect (5 * X_Cell, 5.5f * Y_Cell, 5 * X_Cell, 1 * Y_Cell), "Демо 1 (уровень)"))
				currentLevel = "Demo_01";
			if (GUI.Button (new Rect (5 * X_Cell, 7f * Y_Cell, 5 * X_Cell, 1 * Y_Cell), "Демо 2 (карта)"))
				currentLevel = "Demo_02";

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
			GUI.Box(new Rect(1 * X_Cell, 1 * Y_Cell, 13 * X_Cell, 8 * Y_Cell), "Смерть.\r\nВоин убил кошку.");
			if (GUI.Button (new Rect (6 * X_Cell, 0 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Меню"))
				Application.LoadLevel("Game_Menu");
		}

		// В окне проигрыша пишется сообщение о проигрыше и кнопка перехода в меню 
		else if (Application.loadedLevelName == "Game_Over")
		{
			GUI.skin.box.fontSize = 36;
			GUI.skin.box.alignment = TextAnchor.MiddleCenter;
			GUI.Box(new Rect(1 * X_Cell, 1 * Y_Cell, 13 * X_Cell, 8 * Y_Cell), "Проигрыш.\r\nВоин забрал сокровище.");
			if (GUI.Button (new Rect (6 * X_Cell, 0 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Меню"))
				Application.LoadLevel("Game_Menu");
		}
		
		// В окне победы пишется сообщение о победе и кнопка перехода в меню
		else if (Application.loadedLevelName == "Game_Winner")
		{
			GUI.skin.box.fontSize = 36;
			GUI.skin.box.alignment = TextAnchor.MiddleCenter;
			GUI.Box(new Rect(1 * X_Cell, 1 * Y_Cell, 13 * X_Cell, 8 * Y_Cell), "Победа!\r\nВоин загнан в ловушку.");
			if (GUI.Button (new Rect (6 * X_Cell, 0 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Меню"))
				Application.LoadLevel("Game_Menu");
		}

		// В окне любого уровня:
		else
		{
			// Рисуется кнопка: "Пауза"(открывает меню)
			if (GUI.Button (new Rect (6 * X_Cell, 0 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Пауза"))
				Application.LoadLevel("Game_Menu");

			if ((Application.loadedLevelName != "Game_Over") && (Application.loadedLevelName != "Game_Over_Killed") && (Application.loadedLevelName != "Game_Winner"))
			{
				// Управление движением кошки
				if (GUI.Button (new Rect (1 * X_Cell, 7 * Y_Cell, 1 * X_Cell, 1 * Y_Cell), "<")) X_Direction = -1;
				if (GUI.Button (new Rect (3 * X_Cell, 7 * Y_Cell, 1 * X_Cell, 1 * Y_Cell), ">")) X_Direction = 1;
				if (GUI.Button (new Rect (2 * X_Cell, 6 * Y_Cell, 1 * X_Cell, 1 * Y_Cell), "^")) Y_Direction = -1;
				if (GUI.Button (new Rect (2 * X_Cell, 8 * Y_Cell, 1 * X_Cell, 1 * Y_Cell), "v")) Y_Direction = 1;

				if (GUI.Button (new Rect (1 * X_Cell, 6 * Y_Cell, 1 * X_Cell, 1 * Y_Cell), "")) {X_Direction = -1; Y_Direction = -1;}
				if (GUI.Button (new Rect (3 * X_Cell, 6 * Y_Cell, 1 * X_Cell, 1 * Y_Cell), "")) {X_Direction = 1; Y_Direction = -1;}
				if (GUI.Button (new Rect (1 * X_Cell, 8 * Y_Cell, 1 * X_Cell, 1 * Y_Cell), "")) {X_Direction = -1; Y_Direction = 1;}
				if (GUI.Button (new Rect (3 * X_Cell, 8 * Y_Cell, 1 * X_Cell, 1 * Y_Cell), "")) {X_Direction = 1; Y_Direction = 1;}

				HellCat = GameObject.Find("HellCat").GetComponent<Player_Controller>();
				if (GUI.Button (new Rect (11 * X_Cell, 7 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Режим")) HellCat.BroadcastMessage("Mode");
				MainCamera = GameObject.Find("Camera").GetComponent<Camera_Controller>();
				if (GUI.Button (new Rect (6 * X_Cell, 7 * Y_Cell, 3 * X_Cell, 1 * Y_Cell), "Камера")) MainCamera.BroadcastMessage("SetCamera");
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
			if (x >= 1 * X_Cell && x <= 2 * X_Cell && y >= 2 * Y_Cell && y <= 3 * Y_Cell)
			{
				X_Direction = -1;
				Y_Direction = 0;
				Moving();
			}
			// Вниз
			else if (x >= 3 * X_Cell && x <= 4 * X_Cell && y >= 2 * Y_Cell && y <= 3 * Y_Cell)
			{
				X_Direction = 1;
				Y_Direction = 0;
				Moving ();
			}
			// Вправо
			else if (x >= 2 * X_Cell && x <= 3 * X_Cell && y >= 3 * Y_Cell && y <= 4 * Y_Cell)
			{
				X_Direction = 0;
				Y_Direction = 1;
				Moving();
			}
			// Влево
			else if (x >= 2 * X_Cell && x <= 3 * X_Cell && y >= 1 * Y_Cell && y <= 2 * Y_Cell)
			{
				X_Direction = 0;
				Y_Direction = -1;
				Moving();
			}
			// Влево вверх
			else if (x >= 1 * X_Cell && x <= 2 * X_Cell && y >= 3 * Y_Cell && y <= 4 * Y_Cell)
			{
				X_Direction = -1;
				Y_Direction = 1;
				Moving();
			}
			// Вправо вверх
			else if (x >= 3 * X_Cell && x <= 4 * X_Cell && y >= 3 * Y_Cell && y <= 4 * Y_Cell)
			{
				X_Direction = 1;
				Y_Direction = 1;
				Moving ();
			}
			// Влево вниз
			else if (x >= 1 * X_Cell && x <= 2 * X_Cell && y >= 1 * Y_Cell && y <= 2 * Y_Cell)
			{
				X_Direction = -1;
				Y_Direction = -1;
				Moving();
			}
			// Вправо вниз
			else if (x >= 3 * X_Cell && x <= 4 * X_Cell && y >= 1 * Y_Cell && y <= 2 * Y_Cell)
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
		HellCat = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
		HellCat.BroadcastMessage("Go", Move);		
	}	
}


	


