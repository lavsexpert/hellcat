using UnityEngine;

using System.Collections;


public class MiniMap : MonoBehaviour
{
	public Texture2D Map;
	public Texture2D PlayerOnTheMap;
	public Texture2D EnemyOnTheMap;
	public Texture2D TreasureOnTheMap;

	const int MapCaptionHeight = 20;
	private int MapWidth;
	private int MapHeight;
	private Rect windowRect;

	private GameObject player;
	private GameObject treasure;
	private GameObject[] enemies;
	private GameObject[] borders;
	private double[] MapCoordinates;//LeftX, UpperZ, RightX, LowerZ

	void Start() 
	{
		player 	= GameObject.FindWithTag("Player");
		enemies = GameObject.FindGameObjectsWithTag("Enemy_Warrior");
		treasure= GameObject.FindWithTag("Treasure");
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

	void OnGUI ()
	{
		windowRect 	= GUI.Window (0, windowRect, WindowFunction, "Map");
	}

	void WindowFunction (int windowID) {

		Color c = GUI.color;
		c.a = 0.5f;
		GUI.color = c;
		GUI.DragWindow();

		int MapOffset = 0;
		//Show minimap
		GUI.Box (new Rect (MapOffset, MapCaptionHeight, MapWidth, MapHeight), Map);

		//Show player on the minimap
		double PlayerX = player.transform.position.x;
		double PlayerZ = player.transform.position.z;
		double OffsetX = (PlayerX - MapCoordinates [0]) / (MapCoordinates [2] - MapCoordinates [0]);
		double OffsetZ = (MapCoordinates[3] - PlayerZ)/(MapCoordinates[3] - MapCoordinates[1]);
		int PositionOnMapX = (int) (MapWidth * OffsetX);
		int PositionOnMapZ = (int) (MapHeight * OffsetZ);
		GUI.color = Color.green;
		GUI.DrawTexture (new Rect (MapOffset + PositionOnMapX - 8, MapCaptionHeight + PositionOnMapZ - 8,16,16), PlayerOnTheMap, ScaleMode.ScaleToFit);

		//Show enemies on the minimap
		GUI.color = Color.red;
		for (int i = 0; i < enemies.Length; i++) 
		{
			double EnemyX = enemies[i].transform.position.x;
			double EnemyZ = enemies[i].transform.position.z;
			double EnemyOffsetX = (EnemyX - MapCoordinates [0]) / (MapCoordinates [2] - MapCoordinates [0]);
			double EnemyOffsetZ = (MapCoordinates[3] - EnemyZ)/(MapCoordinates[3] - MapCoordinates[1]);
			int EnemyPositionOnMapX = (int) (MapWidth * EnemyOffsetX);
			int EnemyPositionOnMapZ = (int) (MapHeight * EnemyOffsetZ);
			GUI.DrawTexture (new Rect (MapOffset + EnemyPositionOnMapX - 8, MapCaptionHeight + EnemyPositionOnMapZ - 8,16,16), EnemyOnTheMap, ScaleMode.ScaleToFit);
		}

		//Show treasure on the minimap
		double TreasureX = treasure.transform.position.x;
		double TreasureZ = treasure.transform.position.z;
		double TreasureOffsetX = (TreasureX - MapCoordinates [0]) / (MapCoordinates [2] - MapCoordinates [0]);
		double TreasureOffsetZ = (MapCoordinates[3] - TreasureZ)/(MapCoordinates[3] - MapCoordinates[1]);
		int TreasurePositionOnMapX = (int) (MapWidth * TreasureOffsetX);
		int TreasurePositionOnMapZ = (int) (MapHeight * TreasureOffsetZ);
		GUI.color = Color.yellow;
		GUI.DrawTexture (new Rect (MapOffset + TreasurePositionOnMapX - 8, MapCaptionHeight + TreasurePositionOnMapZ - 8,16,16), TreasureOnTheMap, ScaleMode.ScaleToFit);
		//GUI.Box (new Rect (0,0,100,50), player.transform.position.x.ToString());

	}
	
}
