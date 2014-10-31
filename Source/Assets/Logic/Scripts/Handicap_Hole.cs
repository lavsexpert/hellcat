using UnityEngine;
using System.Collections;

public class Handicap_Hole : MonoBehaviour 
{
	void OnTriggerEnter (Collider tr)
	{
		if (tr.collider.tag == "Enemy_Warrior") 
		{
			Application.LoadLevel("Game_Winner");
		}
	}
}
