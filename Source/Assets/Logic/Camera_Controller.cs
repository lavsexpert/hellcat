using UnityEngine;
using System.Collections;

public class Camera_Controller : MonoBehaviour
{
	
	public GameObject Player; 	// Игровой персонаж

	private Vector3 Offset;		// Вектор смещения
	private bool Camera_Mode = false;

	// При запуске
	void Start() 
	{
		// Сохраняется исходная позиция камеры
		Offset = transform.position;
	}

	void FixedUpdate()
	{
		if (Input.GetKeyDown (KeyCode.Tab)) {
				
			if ( Camera_Mode == false)
			{
				Camera_Mode = true;
				Offset =  new Vector3 (0.0f , 30.0f, -18.0f);
				camera.fieldOfView = 20;
				transform.rotation = Quaternion.AngleAxis(300, Vector3.left); 

			}
			else
			{
				Offset =  new Vector3 (5.0f , 40.0f, 3.5f);
				Camera_Mode = false;
				camera.fieldOfView = 35;
				transform.rotation = Quaternion.AngleAxis(270, Vector3.left); 
			}

		}








	}
	
	// После обновления сцены
	void LateUpdate()
	{	
		// Позиция камеры смещается на позицию персонажа (т.е. следует за персонажем) 
		transform.position = Player.transform.position + Offset;
	}





}