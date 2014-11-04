using UnityEngine;
using System.Collections;

public class Camera_Controller : MonoBehaviour
{
	
	public GameObject Player; 	// Игровой персонаж
	public int distance; //Расстояние от игрового персонажа

	private Vector3 Offset;		// Вектор смещения
	private bool Camera_Mode = false;
	private bool FollowPlayer = true;

	// При запуске
	void Start() 
	{
		// Установим исходную позицию камеры относительно игрового персонажа
		if (distance == 0) 
		{
			distance = 10;
		}
		Vector3 CameraDirection = transform.rotation.eulerAngles;
		float distanceXZ = distance * Mathf.Cos(CameraDirection.x * Mathf.Deg2Rad);
		float XOffset = distanceXZ * Mathf.Sin(CameraDirection.y * Mathf.Deg2Rad);
		float YOffset = distance * Mathf.Sin(CameraDirection.x * Mathf.Deg2Rad);
		float ZOffset = distanceXZ * Mathf.Cos(CameraDirection.y * Mathf.Deg2Rad);
		Offset = new Vector3(- XOffset, YOffset, - ZOffset); 
		//Offset = transform.position;
	}

	void FixedUpdate()
	{
		if (Input.GetKeyDown (KeyCode.Tab)) {

							
			/*if ( Camera_Mode == false)
			{
				Camera_Mode = true;
				//Offset =  new Vector3 (0.0f , 30.0f, -18.0f);
				Offset =  new Vector3 (0.0f , 20.0f, -18.0f);
				camera.fieldOfView = 20;
				transform.rotation = Quaternion.AngleAxis(300, Vector3.left); 

			}
			else
			{
				Offset =  new Vector3 (5.0f , 40.0f, 3.5f);
				Camera_Mode = false;
				camera.fieldOfView = 35;
				transform.rotation = Quaternion.AngleAxis(270, Vector3.left); 
			}*/

		}








	}

	void SetFollowPlayer(bool folplayer)
	{
		FollowPlayer = folplayer;
	}
	// После обновления сцены
	void LateUpdate()
	{	
		// Позиция камеры смещается на позицию персонажа (т.е. следует за персонажем) 
		if (FollowPlayer == true) 
		{		
			transform.position = Player.transform.position + Offset;
		}
	}





}