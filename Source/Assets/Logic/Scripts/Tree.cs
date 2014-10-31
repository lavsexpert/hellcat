
using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour {

	private int Random_Value =0;
	private float Random_Value_Scale =0.0f;
	private float Warrior_Height =2.2f; //2.2f
	private float Percents_Of_Warrior_Height = 0.2f;

	public MeshFilter   Tree_Mesh_Filter;
	public Animator     Tree_Animator;
	public Transform    Tree_Transform;
	public BoxCollider  Tree_Box_Collider;

	public Mesh Tree_Model_1;
	public Mesh Tree_Model_2;
	public Mesh Tree_Model_3;
	public Mesh Tree_Model_4;

	public Avatar Tree_Animation_1;
	public Avatar Tree_Animation_2;
	public Avatar Tree_Animation_3;
	public Avatar Tree_Animation_4;

	// Use this for initialization
	void Start () {

		Tree_Mesh_Filter = GetComponent<MeshFilter>();
		Tree_Animator = GetComponent<Animator>();
		 
		Random_Value = Random.Range (1,4);
		Random_Value_Scale = 2*Warrior_Height + Random.Range (-Percents_Of_Warrior_Height*Warrior_Height,Percents_Of_Warrior_Height*Warrior_Height);

		if (Random_Value == 1)
		{



			 
			Tree_Mesh_Filter.mesh = Tree_Model_1;
			Tree_Animator.avatar = Tree_Animation_1; 
			Tree_Transform.localScale = new Vector3 (Random_Value_Scale,Random_Value_Scale,Random_Value_Scale); 
			Tree_Transform.rotation = Quaternion.AngleAxis (90, Vector3.left); 
			Tree_Box_Collider.size = new Vector3 (0.2f,0.2f,1);
			Tree_Transform.position = Tree_Transform.position - new Vector3 (0,0.5f,0);

		}

		if (Random_Value == 2)
		{

			 
			Tree_Mesh_Filter.mesh = Tree_Model_2;
			Tree_Animator.avatar = Tree_Animation_2;
			Tree_Transform.localScale = new Vector3 (Random_Value_Scale,Random_Value_Scale,Random_Value_Scale); 
			Tree_Transform.rotation = Quaternion.AngleAxis (90, Vector3.left); 
			Tree_Box_Collider.size = new Vector3 (0.2f,0.2f,1);
			Tree_Transform.position = Tree_Transform.position - new Vector3 (0,0.2f,0);

		
		}


		if (Random_Value == 3)
		{
			 
			Tree_Mesh_Filter.mesh = Tree_Model_3;
			Tree_Animator.avatar = Tree_Animation_3;
			Tree_Transform.localScale = new Vector3 (Random_Value_Scale,Random_Value_Scale,Random_Value_Scale); 
			Tree_Transform.rotation = Quaternion.AngleAxis (90, Vector3.left); 
			Tree_Box_Collider.size = new Vector3 (0.2f,0.2f,1);
			Tree_Transform.position = Tree_Transform.position - new Vector3 (0,0.5f,0);

			
		}


		if (Random_Value == 4)
		{
			 
			Tree_Mesh_Filter.mesh = Tree_Model_4;
			Tree_Animator.avatar = Tree_Animation_4;
			Tree_Transform.localScale = new Vector3 (Random_Value_Scale,Random_Value_Scale,Random_Value_Scale); 
			Tree_Transform.rotation = Quaternion.AngleAxis (90, Vector3.left); 
			Tree_Box_Collider.size = new Vector3 (0.15f,0.15f,1);
			Tree_Transform.position = Tree_Transform.position - new Vector3 (0,0.5f,0);

			
		}
	 
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}




}
