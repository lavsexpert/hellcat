using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player_LifeBar : MonoBehaviour {

	public Image LifeBar;
	public Texture2D[] HellCat_Lifes;
	public static int Lifes = 3;

	private float WaitTimeStarted = 0;
	public int WaitTimeKilled = 1;

	// При обновлении сцены
	void Update () 
	{  
		if (Lifes > 0)
		{
			var texture = HellCat_Lifes[Lifes-1];
			var newSprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), Vector2.one * 0.5f);
        	LifeBar.GetComponent<Image>().sprite = newSprite;
		}

		if (Lifes <= 0)
		{
			if (WaitTimeStarted == 0)
			{
				WaitTimeStarted = Time.time;
				GetComponent<AudioSource>().Play();
				return;
			}
			if (((Time.time - WaitTimeStarted) > WaitTimeKilled)&&(GetComponent<AudioSource>().isPlaying == false))
			{
				WaitTimeStarted = 0;
				Application.LoadLevel("Game_Over_Killed");
				Lifes = 3;
			}
		}
	}
}
