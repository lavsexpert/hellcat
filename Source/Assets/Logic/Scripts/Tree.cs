
using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour {

	private int Random_Value =0;

	
	public MeshFilter   Tree_Mesh_Filter;
	public Animator     Tree_Animator;
	public Transform    Tree_Scale;

	public Mesh Tree_Model_1;
	public Mesh Tree_Model_2;

	public Avatar Tree_Animation_1;
	public Avatar Tree_Animation_2;

	// Use this for initialization
	void Start () {

		Tree_Mesh_Filter = GetComponent<MeshFilter>();
		Tree_Animator = GetComponent<Animator>();
		 
		Random_Value = Random.Range (1,3);

	
		if (Random_Value == 1)
		{
			//Tree_Sprite.sprite = sprite1;
			Tree_Mesh_Filter.mesh = Tree_Model_1;
			Tree_Animator.avatar = Tree_Animation_1;
			Tree_Scale.localScale = new Vector3 (3,3,3);  




		}

		if (Random_Value == 2)
		{
			//Tree_Sprite.sprite = sprite2;
			Tree_Mesh_Filter.mesh = Tree_Model_2;
			Tree_Animator.avatar = Tree_Animation_2;
			Tree_Scale.localScale = new Vector3 (3,3,3);  
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
