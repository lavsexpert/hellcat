using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	// При показе интерфейса
	void OnGUI () 
	{

		// Рисуется прямоугольник меню и кнопки меню, 
		// при нажатии на которые загружается соответствюущий уровень
		GUI.Box (new Rect (Screen.width / 2 - 100, 10, 200, 120), "Меню");
		if (GUI.Button (new Rect (Screen.width / 2 - 90, 40, 80, 30), "1 уровень"))
			Application.LoadLevel ("Level01");
		if (GUI.Button (new Rect (Screen.width / 2 - 90, 80, 80, 30), "2 уровень"))
			Application.LoadLevel ("Level02");
		if (GUI.Button (new Rect (Screen.width / 2 + 10, 40, 80, 30), "3 уровень"))
			Application.LoadLevel ("Level03");
		if (GUI.Button (new Rect (Screen.width / 2 + 10, 80, 80, 30), "4 уровень"))
			Application.LoadLevel ("Level04");

	}

}
