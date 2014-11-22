using UnityEngine;
using System.Collections;

public class Game_Map : MonoBehaviour
{
	// Окно и его размеры
	private Rect WindowRect;
	const int MapCaptionHeight = 20;
	private int MapWidth;
	private int MapHeight;

	// Координаты поверхности карты
	private double LeftX;
	private double RightX;
	private double UpperZ;
	private double LowerZ;

	// Текстуры для показа персонажей и объектов
	public Texture2D Map2D;
	public Texture2D HellCat2D;
	public Texture2D Treasure2D;
	public Texture2D Trap2D;
	public Texture2D Warrior2D;
	public Texture2D Wolf2D;

	// Персонажи и объекты, показываемые на карте
	private GameObject Land;
	private GameObject[] HellCats;
	private GameObject[] Treasures;
	private GameObject[] Traps;
	private GameObject[] Warriors;
	private GameObject[] Wolfs;

	// При запуске
	void Start() 
	{
		// Поиск объектов на сцене
		HellCats	= GameObject.FindGameObjectsWithTag("Player");
		Treasures 	= GameObject.FindGameObjectsWithTag("Treasure");
		Traps 		= GameObject.FindGameObjectsWithTag("Trap");
		Warriors 	= GameObject.FindGameObjectsWithTag("Enemy_Warrior");
		Wolfs 		= GameObject.FindGameObjectsWithTag("Enemy_Wolf");

		// Подготовка карты и её краёв
		Land = GameObject.Find("Land");
		LeftX = Land.renderer.bounds.min.x;
		RightX = Land.renderer.bounds.max.x;
		UpperZ = Land.renderer.bounds.min.z;
		LowerZ = Land.renderer.bounds.max.z;

		// Вычисление логических размеров карты (чтобы не зависеть от размера экрана)
		int DesiredWidth  = (int)Screen.width  / 4;
		int DesiredHeight = (int)Screen.height / 3; 
		if (DesiredWidth > DesiredHeight) 
		{
			MapWidth = DesiredHeight;
		} 
		else 
		{
			MapWidth = DesiredWidth;
		}
		MapHeight = (int)((double)Map2D.height / (double)Map2D.width * (double)MapWidth); 
		if (MapHeight > (MapWidth - MapCaptionHeight))
		{
			MapHeight = MapWidth - MapCaptionHeight;
			MapWidth = (int)((double)Map2D.width / (double)Map2D.height * (double)MapHeight);
		}
		WindowRect = new Rect(Screen.width - MapWidth, 0, MapWidth, MapHeight + MapCaptionHeight);
	}

	// При показе интерфейса
	void OnGUI ()
	{
		WindowRect 	= GUI.Window(0, WindowRect, WindowFunction, "Карта");
	}

	// Рисование окна
	void WindowFunction(int windowID) 
	{
		// Подготовка окна
		Color WindowColor = GUI.color;
		WindowColor.a = 0.5f;
		GUI.color = WindowColor;
		GUI.DragWindow();
		int MapOffset = 0;

		// Рисование карты в окне
		GUI.Box (new Rect(MapOffset, MapCaptionHeight, MapWidth, MapHeight), Map2D);

		// Показ персонажей и объектов на карте
		ShowOnTheMap(Treasures, Treasure2D, Color.yellow, MapOffset);	// Сундуки
		ShowOnTheMap(Traps, Trap2D, Color.yellow, MapOffset);			// Ловушки
		ShowOnTheMap(Warriors, Warrior2D, Color.red, MapOffset);		// Воины
		ShowOnTheMap(Wolfs, Wolf2D, Color.red, MapOffset);				// Волки
		ShowOnTheMap(HellCats, HellCat2D, Color.black, MapOffset);		// Кошки
	}

	// Отрисовка массива персонажей или объектов на карте
	void ShowOnTheMap(GameObject[] Objects, Texture2D Object2D, Color Object2DColor, int MapOffset)
	{
		GUI.color = Object2DColor;
		for (int i = 0; i < Objects.Length; i++) 
		{
			if (Objects[i] != null)
			{
				double ObjectX = Objects[i].transform.position.x;
				double ObjectZ = Objects[i].transform.position.z;
				double ObjectOffsetX = (ObjectX - LeftX) / (RightX - LeftX);
				double ObjectOffsetZ = (LowerZ - ObjectZ) / (LowerZ - UpperZ);
				int ObjectPositionOnMapX = (int)(MapWidth * ObjectOffsetX);
				int ObjectPositionOnMapZ = (int)(MapHeight * ObjectOffsetZ);
				GUI.DrawTexture(new Rect(MapOffset + ObjectPositionOnMapX - 8, 
				                         MapCaptionHeight + ObjectPositionOnMapZ - 8,
				                         16,
				                         16), Object2D, ScaleMode.ScaleToFit);
			}
		}
	}
}
