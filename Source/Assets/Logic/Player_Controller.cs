using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour 
{
	public MeshFilter HellCat_Mesh_Filter; 
	public Mesh HellCat_Mesh_Mode_One;
	public Mesh HellCat_Mesh_Mode_Two;
 
	public float Speed;              		// Скорость игрока
	private float MoveVertical;				// Перемещение по вертикали
	private float MoveHorizontal;			// Перемещение по горизонтали

	//режим кошки, false = кошка видна целиком true = кошка ушла под землю
	public static bool Hellcat_Mode = false;

	// Перед обновлением сцены	
	void FixedUpdate()
	{		
		MoveHorizontal = Input.GetAxis("Horizontal");
		MoveVertical = Input.GetAxis("Vertical");
		
		Vector3 Move = new Vector3(MoveHorizontal, 0.0f, MoveVertical);
		Go(Move);

		if (Input.GetKeyDown (KeyCode.Space)) 
		{			
			Mode();
		}
		
	}

	// При обновлении сцены	
	void Update ()
	{
		animation.CrossFade ("HellCat_Take_004_Go");
	}

	// Изменение положения кошки
	public void Mode()
	{
		if (Hellcat_Mode == false) 
		{
			Hellcat_Mode = true;
			rigidbody.isKinematic = true;
			HellCat_Mesh_Filter.mesh = HellCat_Mesh_Mode_Two;
		} 
		else 
		{
			Hellcat_Mode = false;
			rigidbody.isKinematic = false;
			HellCat_Mesh_Filter.mesh = HellCat_Mesh_Mode_One;
		}
	}
	
	// Перемещение кошки
	public void Go(Vector3 Move)
	{
		if (Object_Level_Border.Level_Border_Touched == false) 
		{
			if ((MoveVertical < 0) && (MoveHorizontal == 0) ) 
			{
				rigidbody.rotation = Quaternion.Euler (0.0f, 90.0f, 0.0f);
			}
			
			if ((MoveVertical > 0) && (MoveHorizontal == 0)) 
			{
				rigidbody.rotation = Quaternion.Euler (0.0f, 270.0f, 0.0f);
			}
			
			if ((MoveVertical == 0) && (MoveHorizontal < 0)) 
			{
				rigidbody.rotation = Quaternion.Euler (0.0f, 180.0f, 0.0f);
			}
			
			if ((MoveVertical == 0) && (MoveHorizontal > 0)) 
			{
				rigidbody.rotation = Quaternion.Euler (0.0f, 360.0f, 0.0f);
			}


			if ((MoveVertical < 0) && (MoveHorizontal < 0)) 
			{
				rigidbody.rotation = Quaternion.Euler (0.0f, 135.0f, 0.0f);
			}
			
			if ((MoveVertical > 0) && (MoveHorizontal > 0)) 
			{
				rigidbody.rotation = Quaternion.Euler (0.0f, 315.0f, 0.0f);
			}
			
			if ((MoveVertical > 0) && (MoveHorizontal < 0)) 
			{
				rigidbody.rotation = Quaternion.Euler (0.0f, 225.0f, 0.0f);
			}
			
			if ((MoveVertical < 0) && (MoveHorizontal > 0)) 
			{
				rigidbody.rotation = Quaternion.Euler (0.0f, 45.0f, 0.0f);
			}


		} 
		else 
		{
			Object_Level_Border.Level_Border_Touched = false;
		}

		if (Hellcat_Mode == false) 
		{			
			rigidbody.position = rigidbody.position + Move / Speed;			
		} 
		else
		{						
			rigidbody.position = rigidbody.position + Move / (Speed / 2);
		}

	}


}