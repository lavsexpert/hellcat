
using UnityEngine;
using System.Collections;

public class Game_Control : MonoBehaviour 
{	
	private string currentLevel;	// Имя текущего уровня





	// При показе интерфейса
	void OnGUI() 
	{		
		// В меню: 
		if (Application.loadedLevelName == "Game_Menu")
		{
			// Рисуются 2 кнопки: "Загрузить уровень"(открывает сцену загрузки уровня) и "Выйти из игры"(закрывает игру)
			if (GUI.Button (new Rect (Screen.width / 2 - 80, Screen.height / 2 - 40, 160, 30), "Загрузить уровень"))
				Application.LoadLevel("Game_Load");
			if (GUI.Button (new Rect (Screen.width / 2 - 80, Screen.height / 2 + 10, 160, 30), "Выйти из игры"))
				Application.Quit();
		}
		
		// В окне загрузки уровней:
		else if (Application.loadedLevelName == "Game_Load")
		{
			// Рисуется прямоугольник меню и кнопки загрузки каждого уровня, 
			// а также в настройках сохраняется, какой уровень был загружен
			currentLevel = "";
			
			GUI.Box (new Rect (Screen.width / 2 - 300, Screen.height / 2 - 200, 600, 200), "Загрузить уровень");

			if (GUI.Button (new Rect (Screen.width / 2 - 70, Screen.height / 2 - 150, 140, 30), "Уровень 1"))
				currentLevel = "Level_01";
			if (GUI.Button (new Rect (Screen.width / 2 - 70, Screen.height / 2 - 100, 140, 30), "Демо 1 (уровень)"))
				currentLevel = "Demo_01";
			if (GUI.Button (new Rect (Screen.width / 2 - 70, Screen.height / 2 - 50, 140, 30), "Демо 2 (карта)"))
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
			if (GUI.Button (new Rect (10, Screen.height - 80, 80, 30), "Назад"))
			{
				currentLevel = PlayerPrefs.GetString("Level");
				if (currentLevel != "") 
				{
					Application.LoadLevel(currentLevel);
				}
			}

			// А также рисуется кнопка "Карта", для просмотра карты уровня
			if (GUI.Button (new Rect (10, Screen.height - 40, 80, 30), "Меню"))
				Application.LoadLevel("Game_Menu");
		}

		// В окне любого уровня:
		else
		{
			// Рисуются 2 кнопки: "Карта"(открывает карту) и "Меню"(открывает меню)
			if (GUI.Button (new Rect (10, Screen.height - 80, 80, 30), "Карта"))
				Application.LoadLevel("Game_Map");
			if (GUI.Button (new Rect (10, Screen.height - 40, 80, 30), "Меню"))
				Application.LoadLevel("Game_Menu");
		}
		
	}
	
}
