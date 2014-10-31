using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour {

	private int Random_Value =0;
	public SpriteRenderer Tree_Sprite ;
	public Sprite sprite1; 
	public Sprite sprite2;

	// Use this for initialization
	void Start () {

		Tree_Sprite = GetComponent<SpriteRenderer>(); 
		Random_Value = Random.Range (1,3);

		if (Random_Value == 1)
		{
			Tree_Sprite.sprite = sprite1;
		}

		if (Random_Value == 2)
		{
			Tree_Sprite.sprite = sprite2;
		}

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	/*void OnTriggerEnter (Collider tr)
	{
		if (tr.collider.tag == "Player") 
		{

		}
	}*/
}
