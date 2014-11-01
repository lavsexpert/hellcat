using UnityEngine;
using System.Collections;

public class HellCat_LifeBar_Script : MonoBehaviour {

	public GUITexture HellCat_LifeBar_GUITexture;
	public Texture HellCat_3_Lifes_texture ;
	public Texture HellCat_2_Lifes_texture ;
	public Texture HellCat_1_Lifes_texture ;
	public static int HellCat_LifeBar_Value = 3;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	  
		if (HellCat_LifeBar_Value == 3)
		{
			HellCat_LifeBar_GUITexture.texture = HellCat_3_Lifes_texture;
		}


		if (HellCat_LifeBar_Value == 2)
		{
			HellCat_LifeBar_GUITexture.texture = HellCat_2_Lifes_texture;
		}


		if (HellCat_LifeBar_Value == 1)
		{
			HellCat_LifeBar_GUITexture.texture = HellCat_1_Lifes_texture;
		}

		if (HellCat_LifeBar_Value == 0)
		{
			Application.LoadLevel("Game_Over_Killed");
			HellCat_LifeBar_Value =3;
		}

	}
}
