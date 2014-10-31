using UnityEngine;
using System.Collections;

public class Treasure : MonoBehaviour 
{
	void OnTriggerEnter (Collider tr)
	{
		if (tr.collider.tag == "Enemy_Warrior") 
		{
			Application.LoadLevel("Game_Over");
		}
	}
}
