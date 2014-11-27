using UnityEngine;
using System.Collections;

public class Player_LifeBar : MonoBehaviour {

	public GUITexture LifeBar;
	public Texture[] HellCat_Lifes;
	public static int Lifes = 3;

	private float WaitTimeStarted = 0;
	public int WaitTimeKilled = 1;

	// При обновлении сцены
	void Update () 
	{  
		if (Lifes > 0)
		{
			LifeBar.texture = HellCat_Lifes[Lifes-1];
		}

		if (Lifes <= 0)
		{
			if (WaitTimeStarted == 0)
			{
				WaitTimeStarted = Time.time;
				audio.Play();
				return;
			}
			if (((Time.time - WaitTimeStarted) > WaitTimeKilled)&&(audio.isPlaying == false))
			{
				WaitTimeStarted = 0;
				Application.LoadLevel("Game_Over_Killed");
				Lifes = 3;
			}
		}
	}
}
