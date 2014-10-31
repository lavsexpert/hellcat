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

	

		animation.CrossFade ("HellCat_Take_004_Go");
		//HellCat_Mesh_Filter.animation.CrossFade ("HellCat_Take_004_Go");
		MoveHorizontal = Input.GetAxis("Horizontal");
		MoveVertical = Input.GetAxis("Vertical");
		
		Vector3 movement = new Vector3(MoveHorizontal, 0.0f, MoveVertical);
		//rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, 0.0f);

		if (MoveVertical < 0) {
			rigidbody.rotation = Quaternion.Euler (0.0f,90.0f, 0.0f);
		}

		if (MoveVertical > 0) {
			rigidbody.rotation = Quaternion.Euler (0.0f,-90.0f, 0.0f);
		}


		if (MoveHorizontal < 0) {
			rigidbody.rotation = Quaternion.Euler (0.0f,180.0f, 0.0f);
		}
		
		if (MoveHorizontal > 0) {
			rigidbody.rotation = Quaternion.Euler (0.0f, 360.0f, 0.0f);
		}

		
		if (Hellcat_Mode == false) 
		{

			rigidbody.position = rigidbody.position + movement / Speed;

		} 
		else
		{			

			rigidbody.position = rigidbody.position + movement / (Speed / 2);
		}
		
		if (Input.GetKeyDown (KeyCode.Space)) 

		{			
			if (Hellcat_Mode == false) 
			{
				Hellcat_Mode = true;
				rigidbody.isKinematic = true;
				HellCat_Mesh_Filter.mesh = HellCat_Mesh_Mode_Two;

				//Cat_SpriteRender.sprite = HellCat_Sprite_Mode_Two;

			} 
			else 
			{
				Hellcat_Mode = false;
				rigidbody.isKinematic = false;
				HellCat_Mesh_Filter.mesh = HellCat_Mesh_Mode_One;
				//Cat_SpriteRender.sprite = HellCat_Sprite_Mode_One;				
			}
		}
		
	}





	void Update ()
	{
	 

	

		//if (Hellcat_Mode == false)
		//{

	//if (animation.isPlaying == false) {

				//if ( (MoveVertical > 0 ) && (MoveHorizontal > 0)) 
		//{
		//HellCat_Mesh_Filter.animation.CrossFade ("HellCat_Take_004_Go");
					animation.CrossFade ("HellCat_Take_004_Go");
		//HellCat_Animation.CrossFade("HellCat_Take_004_Go");
				//}
		//}
	//}


}

}