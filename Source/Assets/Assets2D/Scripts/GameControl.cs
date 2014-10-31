using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {
	
	public string currentLevel;
	public Font GUIFont;

	// При показе интерфейса
	void OnGUI () 
	{
//		GUIFont = new Font("arial.ttf");
//		GUI.skin.font = GUIFont;
		// Окно меню
		if (Application.loadedLevelName == "GameMenu")
		{
			if (GUI.Button (new Rect (Screen.width / 2 - 80, Screen.height / 2 - 40, 160, 30), "Загрузить уровень"))
				Application.LoadLevel ("LoadLevel");
			if (GUI.Button (new Rect (Screen.width / 2 - 80, Screen.height / 2 + 10, 160, 30), "Выйти из игры"))
				Application.Quit();
		}
		
		// Окно загрузки уровней
		else if (Application.loadedLevelName == "LoadLevel")
		{
			// Рисуется прямоугольник меню и кнопки меню, 
			// при нажатии на которые загружается соответствюущий уровень
			currentLevel = "";
			
			GUI.Box (new Rect (Screen.width / 2 - 300, Screen.width / 2 - 200, 600, 200), "Загрузить уровень");
			
			//			if (GUI.Button (new Rect (Screen.width / 2 - 230, Screen.width / 2 - 160, 100, 30), "1 уровень"))
			//				currentLevel = "Level_01";
			//			if (GUI.Button (new Rect (Screen.width / 2 - 230, Screen.width / 2 - 120, 100, 30), "2 уровень"))
			//				currentLevel = "Level_02";
			//			if (GUI.Button (new Rect (Screen.width / 2 - 230, Screen.width / 2 - 80, 100, 30), "3 уровень"))
			//				currentLevel = "Level_03";
			//			if (GUI.Button (new Rect (Screen.width / 2 - 230, Screen.width / 2 - 40, 100, 30), "4 уровень"))
			//				currentLevel = "Level_04";
			
			if (GUI.Button (new Rect (Screen.width / 2 - 110, Screen.width / 2 - 160, 100, 30), "3D - 1 версия"))
				currentLevel = "DemO_Scene_01";
			if (GUI.Button (new Rect (Screen.width / 2 - 110, Screen.width / 2 - 120, 100, 30), "3D - 2 версия"))
				currentLevel = "Demo_Scene_02";
			if (GUI.Button (new Rect (Screen.width / 2 - 110, Screen.width / 2 - 80, 100, 30), "2D - 4 карта"))
				currentLevel = "Level_16";
			//			if (GUI.Button (new Rect (Screen.width / 2 - 110, Screen.width / 2 - 40, 100, 30), "8 уровень"))
			//				currentLevel = "Level_08";
			
			if (GUI.Button (new Rect (Screen.width / 2 + 10, Screen.width / 2 - 160, 100, 30), "2D - 1 карта"))
				currentLevel = "Level_13";	
			if (GUI.Button (new Rect (Screen.width / 2 + 10, Screen.width / 2 - 120, 100, 30), "2D - 2 карта"))
				currentLevel = "Level_14";
			if (GUI.Button (new Rect (Screen.width / 2 + 10, Screen.width / 2 - 80, 100, 30), "2D - 3 карта"))
				currentLevel = "Level_15";
			//			if (GUI.Button (new Rect (Screen.width / 2 + 10, Screen.width / 2 - 40, 100, 30), "Прототип2D 4"))
			//				currentLevel = "Level_16";
			
			//			if (GUI.Button (new Rect (Screen.width / 2 + 130, Screen.width / 2 - 160, 100, 30), "Прототип2D 1"))
			//				currentLevel = "Level_13";
			//			if (GUI.Button (new Rect (Screen.width / 2 + 130, Screen.width / 2 - 120, 100, 30), "Прототип2D 2"))
			//				currentLevel = "Level_14";
			//			if (GUI.Button (new Rect (Screen.width / 2 + 130, Screen.width / 2 - 80, 100, 30), "Прототип2D 3"))
			//				currentLevel = "Level_15";
			//			if (GUI.Button (new Rect (Screen.width / 2 + 130, Screen.width / 2 - 40, 100, 30), "Прототип2D 4"))
			//				currentLevel = "Level_16";
			
			if (currentLevel != "") 
			{
				Application.LoadLevel(currentLevel);
				PlayerPrefs.SetString("Level", currentLevel);
			}
		}
		
		// Окно игры (любого уровня) и окно карты
		else
		{
			// В окне карты есть кнопка "Назад", которая возвращает к уровню
			if (Application.loadedLevelName == "Map")
			{
				if (GUI.Button (new Rect (10, Screen.height - 80, 80, 30), "Назад"))
				{
					currentLevel = PlayerPrefs.GetString("Level");
					if (currentLevel != "") 
					{
						Application.LoadLevel(currentLevel);
					}
				}
			}
			// На уровне есть кнопка "Карта"
			else
			{
				if (GUI.Button (new Rect (10, Screen.height - 80, 80, 30), "Карта"))
					Application.LoadLevel("Map");
				
			}
			// На любом уровне и карте есть кнопка "Меню"
			if (GUI.Button (new Rect (10, Screen.height - 40, 80, 30), "Меню"))
				Application.LoadLevel("GameMenu");
		}
		
	}
	
}
