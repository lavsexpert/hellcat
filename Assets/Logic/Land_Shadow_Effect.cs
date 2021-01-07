using UnityEngine;
using System.Collections;

public class Land_Shadow_Effect : MonoBehaviour {





 
	//Карта и её координаты
	private GameObject Land;
	private float LeftX;
	private float RightX;
	private float UpperZ;
	private float LowerZ;


	private float ScaleX;
	private float ScaleY;
	private float ScaleZ;













	// Use this for initialization
	void Start () {
		// Сохранение краёв карты в переменные
		Land = GameObject.Find("Land");
		LeftX = Land.renderer.bounds.min.x;
		RightX = Land.renderer.bounds.max.x;
		UpperZ = Land.renderer.bounds.min.z;
		LowerZ = Land.renderer.bounds.max.z;

		ScaleX = Land.transform.localScale.x;
		ScaleY = Land.transform.localScale.y;
		ScaleZ = Land.transform.localScale.z;

		float ScaleSize = 2.0f;

		Material GrassMaterialXLeft= Resources.Load("Grass_Material_X_Left", typeof(Material)) as Material;
		Material GrassMaterialXRight= Resources.Load("Grass_Material_X_Right", typeof(Material)) as Material;
		Material GrassMaterialZLower= Resources.Load("Grass_Material_Z_Lower", typeof(Material)) as Material;
		Material GrassMaterialZUpper= Resources.Load("Grass_Material_Z_Upper", typeof(Material)) as Material;
		Material GrassMaterial= Resources.Load("Grass_Material", typeof(Material)) as Material;

		GameObject PlaneXRight = GameObject.CreatePrimitive(PrimitiveType.Plane);
		PlaneXRight.transform.position = new Vector3 (RightX+5.0f*ScaleSize, 0.0f,0.0f);
		PlaneXRight.transform.localScale = new Vector3 (ScaleSize, ScaleY, ScaleZ);
		PlaneXRight.renderer.material = GrassMaterialXRight;
	
		GameObject PlaneXLeft = GameObject.CreatePrimitive(PrimitiveType.Plane);
		PlaneXLeft.transform.position = new Vector3 (LeftX-5.0f*ScaleSize, 0.0f,0.0f);
		PlaneXLeft.transform.localScale = new Vector3 (ScaleSize, ScaleY, ScaleZ);
		PlaneXLeft.renderer.material = GrassMaterialXLeft;

		GameObject PlaneZUpper = GameObject.CreatePrimitive(PrimitiveType.Plane);
		PlaneZUpper.transform.position = new Vector3 (0.0f, 0.0f,UpperZ-5.0f*ScaleSize);
		PlaneZUpper.transform.localScale = new Vector3 (ScaleX, ScaleY,ScaleSize);
		PlaneZUpper.renderer.material = GrassMaterialZUpper;

		GameObject PlaneZLower = GameObject.CreatePrimitive(PrimitiveType.Plane);
		PlaneZLower.transform.position = new Vector3 (0.0f, 0.0f,LowerZ+5.0f*ScaleSize);
		PlaneZLower.transform.localScale = new Vector3 (ScaleX, ScaleY,ScaleSize);
		PlaneZLower.renderer.material = GrassMaterialZLower;



		GameObject PlaneZUpperXLeft = GameObject.CreatePrimitive(PrimitiveType.Plane);
		PlaneZUpperXLeft.transform.position = new Vector3 ( LeftX-5.0f*ScaleSize, 0.0f,UpperZ-5.0f*ScaleSize);
		PlaneZUpperXLeft.transform.localScale = new Vector3 (ScaleSize, 1.0f,ScaleSize);
		PlaneZUpperXLeft.renderer.material = GrassMaterial;



		GameObject PlaneZUpperXRight = GameObject.CreatePrimitive(PrimitiveType.Plane);
		PlaneZUpperXRight.transform.position = new Vector3 ( RightX+5.0f*ScaleSize, 0.0f,UpperZ-5.0f*ScaleSize);
		PlaneZUpperXRight.transform.localScale = new Vector3 (ScaleSize, 1.0f,ScaleSize);
		PlaneZUpperXRight.renderer.material = GrassMaterial;

		GameObject PlaneZLowerXLeft = GameObject.CreatePrimitive(PrimitiveType.Plane);
		PlaneZLowerXLeft.transform.position = new Vector3 ( LeftX-5.0f*ScaleSize, 0.0f,LowerZ+5.0f*ScaleSize);
		PlaneZLowerXLeft.transform.localScale = new Vector3 (ScaleSize, 1.0f,ScaleSize);
		PlaneZLowerXLeft.renderer.material = GrassMaterial;

		GameObject PlaneZLowerXRight = GameObject.CreatePrimitive(PrimitiveType.Plane);
		PlaneZLowerXRight.transform.position = new Vector3 ( RightX+5.0f*ScaleSize, 0.0f,LowerZ+5.0f*ScaleSize);
		PlaneZLowerXRight.transform.localScale = new Vector3 (ScaleSize, 1.0f,ScaleSize);
		PlaneZLowerXRight.renderer.material = GrassMaterial;

		GameObject go = Resources.Load("Tree_Fir_1",typeof(GameObject)) as GameObject;	  
	 



		GameObject TreeAditional;

		for (float TreeCounter = LeftX; TreeCounter <= RightX; TreeCounter++)
		{
		
		TreeAditional = Instantiate(go, new Vector3 (TreeCounter*1.0f , 0.0f, UpperZ-0.2f),Quaternion.AngleAxis(90,Vector3.left))as GameObject;
		TreeAditional.transform.localScale=  new Vector3(4.4f,4.4f, 4.4f );

			TreeAditional = Instantiate(go, new Vector3 (TreeCounter*1.0f , 0.0f, UpperZ-1.2f),Quaternion.AngleAxis(90,Vector3.left))as GameObject;
			TreeAditional.transform.localScale=  new Vector3(4.4f,4.4f, 4.4f );


			 TreeAditional = Instantiate(go, new Vector3 (TreeCounter*1.0f , 0.0f, LowerZ+0.2f),Quaternion.AngleAxis(90,Vector3.left))as GameObject;
			TreeAditional.transform.localScale=  new Vector3(4.4f,4.4f, 4.4f );

			TreeAditional = Instantiate(go, new Vector3 (TreeCounter*1.0f , 0.0f, LowerZ+1.2f),Quaternion.AngleAxis(90,Vector3.left))as GameObject;
			TreeAditional.transform.localScale=  new Vector3(4.4f,4.4f, 4.4f );
		}




		for (float TreeCounter = UpperZ; TreeCounter <= LowerZ; TreeCounter++)
		{
			
		    TreeAditional = Instantiate(go, new Vector3 (LeftX-0.2f, 0.0f, TreeCounter*1.0f),Quaternion.AngleAxis(90,Vector3.left))as GameObject;
			TreeAditional.transform.localScale =  new Vector3(4.4f,4.4f, 4.4f );

			TreeAditional = Instantiate(go, new Vector3 (LeftX-1.2f, 0.0f, TreeCounter*1.0f),Quaternion.AngleAxis(90,Vector3.left))as GameObject;
			TreeAditional.transform.localScale =  new Vector3(4.4f,4.4f, 4.4f );

			TreeAditional = Instantiate(go, new Vector3 (RightX+0.2f, 0.0f, TreeCounter*1.0f),Quaternion.AngleAxis(90,Vector3.left))as GameObject;
			TreeAditional.transform.localScale =  new Vector3(4.4f,4.4f, 4.4f );

			TreeAditional = Instantiate(go, new Vector3 (RightX+1.2f, 0.0f, TreeCounter*1.0f),Quaternion.AngleAxis(90,Vector3.left))as GameObject;
			TreeAditional.transform.localScale =  new Vector3(4.4f,4.4f, 4.4f );


		}


		 
	}
	
	// Update is called once per frame
	void Update () {
	

	 

	}






}
