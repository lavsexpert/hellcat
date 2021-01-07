
using UnityEngine;
using System.Collections;

public class Object_Tree : MonoBehaviour {
	
	private int Random_Value = 0;                 	//случайное число для выбора дерева из списка
	private float Random_Value_Scale = 0.0f;      	//случайное число для создания различной величины деревьев
	private float Warrior_Height = 2.2f;   	//высота воина. высота дерева задана в пропорции от высоты воина
	private float Percents_Of_Warrior_Height = 0.2f;//процент от высоты воина для отклоения размера дерева
	
	
	 GameObject CurrentTree;

	static  string Tree_Fir_1_Path_String = "Graphics/Objects/Trees/Tree_Fir_1";
	static  string Tree_Fir_2_Path_String = "Graphics/Objects/Trees/Tree_Fir_2";
	static  string Tree_Dry_Path_String = "Graphics/Objects/Trees/Tree_Dry";
	static  string Tree_Birch_Path_String = "Graphics/Objects/Trees/Tree_Birch";


	static  string Enemy_Warrior_Path_String = "Warrior";
	static  string HellCat_Path_String = "HellCat";
	static  string Trap_Path_String = "Trap";
	static  string Treasure_Warrior_Path_String = "Treasure";
	static  string Enemy_Seeker_Path_String = "Graphics/Subjects/Seeker/seeker";
	static  string Camera_Path_String = "Graphics/Camera/Camera";



	GameObject Tree_Fir_1_GameObject;
	GameObject Tree_Fir_2_GameObject;
	GameObject Tree_Dry_GameObject;
	GameObject Tree_Birch_GameObject;

	GameObject Enemy_Seeker_GameObject;
	GameObject Enemy_Warrior_GameObject;
	GameObject HellCat_GameObject;
	GameObject Trap_GameObject;
	GameObject Treasure_GameObject;

	GameObject Camera_GameObject;

	//static public Transform Trap_Coord_Transform = new Vector3 (0.0f,0.0f, 0.0f );
	//static public Transform Treasure_Coord_Transform = new Vector3 (0.0f,0.0f, 0.0f );

	void Initialize_Tree_Models ()
	{
		Tree_Fir_1_GameObject = Resources.Load(Tree_Fir_1_Path_String,typeof(GameObject)) as GameObject;	
		Tree_Fir_2_GameObject = Resources.Load(Tree_Fir_2_Path_String,typeof(GameObject)) as GameObject;	
		Tree_Dry_GameObject = Resources.Load(Tree_Dry_Path_String,typeof(GameObject)) as GameObject;	
		Tree_Birch_GameObject = Resources.Load(Tree_Birch_Path_String,typeof(GameObject)) as GameObject;	

		Enemy_Warrior_GameObject = Resources.Load(Enemy_Warrior_Path_String,typeof(GameObject)) as GameObject;	
		Enemy_Seeker_GameObject = Resources.Load(Enemy_Seeker_Path_String,typeof(GameObject)) as GameObject;	
		HellCat_GameObject = Resources.Load(HellCat_Path_String,typeof(GameObject)) as GameObject;	
		Trap_GameObject = Resources.Load(Trap_Path_String,typeof(GameObject)) as GameObject;	
		Treasure_GameObject = Resources.Load(Treasure_Warrior_Path_String,typeof(GameObject)) as GameObject;	

		Camera_GameObject = Resources.Load(Camera_Path_String,typeof(GameObject)) as GameObject;	

	}


 


	void Set_A_Tree_Function (float TreeCoordX , float TreeCoordY ,float TreeCoordZ )
	{	
		
		

		
		
		
		//получение слуайного значения
		Random_Value = Random.Range(1,5);
		Random_Value_Scale = 2 * Warrior_Height + Random.Range(-Percents_Of_Warrior_Height * Warrior_Height,Percents_Of_Warrior_Height * Warrior_Height);


		//		var boxCollider = (BoxCollider)CurrentTree.collider;
		//		boxCollider.size = new Vector3 (0.25f, 0.25f, 0.5f);



		//первый тип дерева
		if (Random_Value == 1)
		{
			CurrentTree = Instantiate(Tree_Fir_1_GameObject, new Vector3 ( TreeCoordX , TreeCoordY, TreeCoordZ),Quaternion.AngleAxis(90,Vector3.left))as GameObject;
			CurrentTree.transform.localScale = new Vector3(Random_Value_Scale, Random_Value_Scale, Random_Value_Scale); 
			
			
		}

		//второй тип дерева
		if (Random_Value == 2)
		{
			CurrentTree = Instantiate(Tree_Fir_2_GameObject, new Vector3 ( TreeCoordX , TreeCoordY, TreeCoordZ),Quaternion.AngleAxis(90,Vector3.left))as GameObject;
			CurrentTree.transform.localScale = new Vector3(Random_Value_Scale, Random_Value_Scale, Random_Value_Scale); 
			
		}
		
		//третий тип дерева
		if (Random_Value == 3)
		{			
			CurrentTree = Instantiate(Tree_Dry_GameObject, new Vector3 ( TreeCoordX , TreeCoordY, TreeCoordZ),Quaternion.AngleAxis(90,Vector3.left))as GameObject;
			CurrentTree.transform.localScale = new Vector3(Random_Value_Scale, Random_Value_Scale, Random_Value_Scale); 
			
		}
		
		//четвертый тип дерева
		if (Random_Value == 4)
		{
			CurrentTree = Instantiate(Tree_Birch_GameObject, new Vector3 ( TreeCoordX , TreeCoordY, TreeCoordZ),Quaternion.AngleAxis(90,Vector3.left))as GameObject;
			CurrentTree.transform.localScale = new Vector3(Random_Value_Scale/3, Random_Value_Scale/3, Random_Value_Scale/3); 
			
		}


		var Tree_BoxCollider = CurrentTree.AddComponent<BoxCollider>();
		Tree_BoxCollider.size = new Vector3 (0.25f, 0.25f, 1.5f);

		//Tree_BoxCollider.transform.localScale = new Vector3 (CurrentTree.transform.localScale.x , CurrentTree.transform.localScale.y, CurrentTree.transform.localScale.z);

	}






	// задание начальных значнией
	void Start() 
	{

		

					Initialize_Tree_Models (); 
	


	
					var	Search = GameObject.FindGameObjectsWithTag ("Tree");
						foreach (GameObject TreeX in Search) {
						Set_A_Tree_Function (TreeX.transform.position.x, TreeX.transform.position.y, TreeX.transform.position.z);
			var Tree_BoxCollider = CurrentTree.AddComponent<BoxCollider> ();
					Tree_BoxCollider.size = new Vector3 (0.25f, 0.25f, 1.5f);	
			DestroyObject (TreeX);
		}

					Search = GameObject.FindGameObjectsWithTag ("Trap");
					foreach (GameObject TreeX in Search) {
			CurrentTree = Instantiate(Trap_GameObject, new Vector3 ( TreeX.transform.position.x, TreeX.transform.position.y-0.6f, TreeX.transform.position.z),Quaternion.AngleAxis(0,Vector3.left))as GameObject;
			var Tree_BoxCollider = CurrentTree.AddComponent<BoxCollider> ();
						Tree_BoxCollider.size = new Vector3 (0.5f, 0.5f, 0.5f);	
			Tree_BoxCollider.center = new Vector3 (0.0f, 0.2f, 0.0f);	

			DestroyObject (TreeX);



		}

			Search = GameObject.FindGameObjectsWithTag ("Treasure");
			foreach (GameObject TreeX in Search) {
						CurrentTree = Instantiate (Treasure_GameObject, new Vector3 (TreeX.transform.position.x, TreeX.transform.position.y, TreeX.transform.position.z), Quaternion.AngleAxis (0, Vector3.left))as GameObject;
			var Tree_BoxCollider = CurrentTree.AddComponent<BoxCollider> ();
				Tree_BoxCollider.size = new Vector3 (0.3f, 0.25f, 0.3f);	
			Tree_BoxCollider.center = new Vector3 (0.0f, 0.0f, 0.1f);		
						DestroyObject (TreeX);
				}

	Search = GameObject.FindGameObjectsWithTag ("Enemy_Warrior");
		foreach (GameObject TreeX in Search) {
			CurrentTree = Instantiate (Enemy_Warrior_GameObject, new Vector3 (TreeX.transform.position.x, TreeX.transform.position.y - 0.6f, TreeX.transform.position.z), Quaternion.AngleAxis (0, Vector3.left))as GameObject;
			//var Tree_BoxCollider = CurrentTree.AddComponent<BoxCollider> ();
			//Tree_BoxCollider.size = new Vector3 (0.25f, 0.25f, 1.5f);	
			DestroyObject (TreeX);
			
		}
	
		
		Search = GameObject.FindGameObjectsWithTag ("Enemy_Seeker");
		foreach (GameObject TreeX in Search) {
			CurrentTree = Instantiate (Enemy_Seeker_GameObject, new Vector3 (TreeX.transform.position.x, TreeX.transform.position.y, TreeX.transform.position.z), Quaternion.AngleAxis (0, Vector3.left))as GameObject;
			//var Tree_BoxCollider = CurrentTree.AddComponent<BoxCollider> ();
			//Tree_BoxCollider.size = new Vector3 (0.25f, 0.25f, 1.5f);	
			DestroyObject (TreeX);
			
		}

		Search = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject TreeX in Search) {

			CurrentTree = Instantiate (HellCat_GameObject, new Vector3 (TreeX.transform.position.x, TreeX.transform.position.y , TreeX.transform.position.z), Quaternion.AngleAxis (0, Vector3.left))as GameObject;
			CurrentTree = Instantiate (Camera_GameObject, new Vector3 (TreeX.transform.position.x, TreeX.transform.position.y , TreeX.transform.position.z), Quaternion.AngleAxis (0, Vector3.left))as GameObject;

			//	var Tree_BoxCollider = CurrentTree.AddComponent<BoxCollider> ();
		//	Tree_BoxCollider.size = new Vector3 (0.25f, 0.25f, 1.5f);	
			DestroyObject (TreeX);
			
		}




}
}
