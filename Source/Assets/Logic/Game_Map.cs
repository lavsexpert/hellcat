using UnityEngine;
using System.Collections;

public class Game_Map : MonoBehaviour
{
	public Texture2D Map;
	public Texture2D PlayerOnTheMap;
	public Texture2D TreasureOnTheMap;
	public Texture2D TrapOnTheMap;
	public Texture2D WarriorOnTheMap;
	public Texture2D WolfOnTheMap;

	const int MapCaptionHeight = 20;
	private int MapWidth;
	private int MapHeight;
	private Rect windowRect;

	private GameObject player;
	private GameObject treasure;
	private GameObject[] warriors;
	private GameObject[] wolfs;
	private GameObject[] traps;
	private GameObject[] borders;
	private double[] MapCoordinates;//LeftX, UpperZ, RightX, LowerZ

	// При запуске
	void Start() 
	{
		// Поиск объектов на сцене
		player 	= GameObject.FindWithTag("Player");
		treasure = GameObject.FindWithTag("Treasure");
		traps = GameObject.FindGameObjectsWithTag("Trap");
		warriors = GameObject.FindGameObjectsWithTag("Enemy_Warrior");
		wolfs = GameObject.FindGameObjectsWithTag("Enemy_Wolf");

		// Подготовка карты и её краёв
		borders = new GameObject[4];
		MapCoordinates = new double[4];
		borders[0] = GameObject.FindWithTag("Border_Z_Minus");
		borders[1] = GameObject.FindWithTag("Border_Z_Plus");
		borders[2] = GameObject.FindWithTag("Border_X_Minus");
		borders[3]= GameObject.FindWithTag("Border_X_Plus");
		for (int i = 0; i < 4; i++) 
		{
			if (i == 0)
			{
				MapCoordinates[0] = borders[i].transform.position.x;
				MapCoordinates[1] = borders[i].transform.position.z;
				MapCoordinates[2] = borders[i].transform.position.x;
				MapCoordinates[3] = borders[i].transform.position.z;
			}
			else
			{
				if (MapCoordinates[0] > borders[i].transform.position.x)
				{
					MapCoordinates[0] = borders[i].transform.position.x;
				}
				if (MapCoordinates[1] > borders[i].transform.position.z)
				{
					MapCoordinates[1] = borders[i].transform.position.z;
				}
				if (MapCoordinates[2] < borders[i].transform.position.x)
				{
					MapCoordinates[2] = borders[i].transform.position.x;
				}
				if (MapCoordinates[3] < borders[i].transform.position.z)
				{
					MapCoordinates[3] = borders[i].transform.position.z;
				}
			}


		}

		// Вычисление логических размеров карты (чтобы не зависеть от размера экрана)
		int DesiredWidth 	= (int) Screen.width / 4;
		int DesiredHeight = (int)Screen.height / 3; 
		if (DesiredWidth > DesiredHeight) 
		{
			MapWidth = DesiredHeight;
		} 
		else 
		{
			MapWidth = DesiredWidth;
		}
		MapHeight = (int) ((double)Map.height / (double)Map.width * (double)MapWidth); 

		if (MapHeight > (MapWidth - MapCaptionHeight))
		{
			MapHeight = MapWidth - MapCaptionHeight;
			MapWidth = (int) ((double)Map.width / (double)Map.height * (double)MapHeight);
		}
		windowRect = new Rect (Screen.width - MapWidth, 0, MapWidth, MapHeight + MapCaptionHeight);

	}

	// При показе интерфейса
	void OnGUI ()
	{
		windowRect 	= GUI.Window (0, windowRect, WindowFunction, "Map");
	}

	// Рисование окна
	void WindowFunction (int windowID) 
	{
		// Подготовка окна
		Color c = GUI.color;
		c.a = 0.5f;
		GUI.color = c;
		GUI.DragWindow();
		int MapOffset = 0;

		// Рисование карты в окне
		GUI.Box (new Rect (MapOffset, MapCaptionHeight, MapWidth, MapHeight), Map);

		// Показ игрока на карте
		double PlayerX = player.transform.position.x;
		double PlayerZ = player.transform.position.z;
		double OffsetX = (PlayerX - MapCoordinates [0]) / (MapCoordinates [2] - MapCoordinates [0]);
		double OffsetZ = (MapCoordinates[3] - PlayerZ)/(MapCoordinates[3] - MapCoordinates[1]);
		int PositionOnMapX = (int) (MapWidth * OffsetX);
		int PositionOnMapZ = (int) (MapHeight * OffsetZ);
		GUI.color = Color.green;
		GUI.DrawTexture (new Rect (MapOffset + PositionOnMapX - 8, MapCaptionHeight + PositionOnMapZ - 8,16,16), PlayerOnTheMap, ScaleMode.ScaleToFit);
		
		// Показ сундука на карте
		double TreasureX = treasure.transform.position.x;
		double TreasureZ = treasure.transform.position.z;
		double TreasureOffsetX = (TreasureX - MapCoordinates [0]) / (MapCoordinates [2] - MapCoordinates [0]);
		double TreasureOffsetZ = (MapCoordinates[3] - TreasureZ)/(MapCoordinates[3] - MapCoordinates[1]);
		int TreasurePositionOnMapX = (int) (MapWidth * TreasureOffsetX);
		int TreasurePositionOnMapZ = (int) (MapHeight * TreasureOffsetZ);
		GUI.color = Color.yellow;
		GUI.DrawTexture (new Rect (MapOffset + TreasurePositionOnMapX - 8, MapCaptionHeight + TreasurePositionOnMapZ - 8,16,16), TreasureOnTheMap, ScaleMode.ScaleToFit);
		//GUI.Box (new Rect (0,0,100,50), player.transform.position.x.ToString());

		// Показ ловушек на карте
		GUI.color = Color.red;
		for (int i = 0; i < traps.Length; i++) 
		{
			double TrapX = traps[i].transform.position.x;
			double TrapZ = traps[i].transform.position.z;
			double TrapOffsetX = (TrapX - MapCoordinates [0]) / (MapCoordinates [2] - MapCoordinates [0]);
			double TrapOffsetZ = (MapCoordinates[3] - TrapZ)/(MapCoordinates[3] - MapCoordinates[1]);
			int TrapPositionOnMapX = (int) (MapWidth * TrapOffsetX);
			int TrapPositionOnMapZ = (int) (MapHeight * TrapOffsetZ);
			GUI.DrawTexture (new Rect (MapOffset + TrapPositionOnMapX - 8, MapCaptionHeight + TrapPositionOnMapZ - 8,16,16), TrapOnTheMap, ScaleMode.ScaleToFit);
		}

		// Показ воинов на карте
		GUI.color = Color.red;
		for (int i = 0; i < warriors.Length; i++) 
		{
			double WarriorX = warriors[i].transform.position.x;
			double WarriorZ = warriors[i].transform.position.z;
			double WarriorOffsetX = (WarriorX - MapCoordinates [0]) / (MapCoordinates [2] - MapCoordinates [0]);
			double WarriorOffsetZ = (MapCoordinates[3] - WarriorZ)/(MapCoordinates[3] - MapCoordinates[1]);
			int WarriorPositionOnMapX = (int) (MapWidth * WarriorOffsetX);
			int WarriorPositionOnMapZ = (int) (MapHeight * WarriorOffsetZ);
			GUI.DrawTexture (new Rect (MapOffset + WarriorPositionOnMapX - 8, MapCaptionHeight + WarriorPositionOnMapZ - 8,16,16), WarriorOnTheMap, ScaleMode.ScaleToFit);
		}
		
		// Показ волков на карте
		GUI.color = Color.red;
		for (int i = 0; i < wolfs.Length; i++) 
		{
			double WolfX = wolfs[i].transform.position.x;
			double WolfZ = wolfs[i].transform.position.z;
			double WolfOffsetX = (WolfX - MapCoordinates [0]) / (MapCoordinates [2] - MapCoordinates [0]);
			double WolfOffsetZ = (MapCoordinates[3] - WolfZ)/(MapCoordinates[3] - MapCoordinates[1]);
			int WolfPositionOnMapX = (int) (MapWidth * WolfOffsetX);
			int WolfPositionOnMapZ = (int) (MapHeight * WolfOffsetZ);
			GUI.DrawTexture (new Rect (MapOffset + WolfPositionOnMapX - 8, MapCaptionHeight + WolfPositionOnMapZ - 8,16,16), WolfOnTheMap, ScaleMode.ScaleToFit);
		}

	}
	
}
