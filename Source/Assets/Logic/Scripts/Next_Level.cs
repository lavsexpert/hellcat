using UnityEngine;
using System.Collections;

public class Next_Level : MonoBehaviour 
{
	void OnTriggerEnter (Collider tr)
	{
		if (tr.collider.tag == "Player") 
		{
			Application.LoadLevel("Demo_02");
		}
	}
}
