﻿using UnityEngine;
using System.Collections;

public class Camera_Controller : MonoBehaviour
{	
	public GameObject HellCat; 			// Игровой персонаж
	public int Distance2D = 20; 		// Расстояние от игрового персонажа до плоской камеры (вид сверху)
	public int Distance3D = 5; 		// Расстояние от игрового персонажа до изометрической камеры (вид сзади)
	private Vector3 Offset;				// Вектор смещения
	private bool Camera_Mode = false;
	private bool FollowPlayer = true;

	// При запуске
	void Start() 
	{
		// По-умолчанию изометрический вид сзади
		SetCamera3D();
	}

	// Перед обновлением сцены
	void FixedUpdate()
	{
		if (Input.GetKeyDown (KeyCode.Tab)) {

			// Переключение режимов показа карты 2D(плоский вид сверху)/3D(изометрический вид сзади)
			if ( Camera_Mode == false)
			{
				Camera_Mode = true;
				SetCamera2D();
			}
			else
			{
				Camera_Mode = false;
				SetCamera3D();
			}
		}
	}

	// После обновления сцены
	void LateUpdate()
	{	
		// Позиция камеры смещается на позицию персонажа (т.е. следует за персонажем) 
		if (FollowPlayer == true) 
		{		
			transform.position = HellCat.transform.position + Offset;
		}
	}

	// Установка камеры для показа плоского вида сверху
	void SetCamera2D()
	{
		camera.fieldOfView = 35;
		transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f); 
		if (Distance2D == 0) Distance2D = 20;
		Offset =  new Vector3(3.0f , Distance2D, 3.0f);
	}

	// Установка камеры для показа изометрического вида сзади
	void SetCamera3D()
	{
		camera.fieldOfView = 60;
		transform.rotation = Quaternion.Euler(37.5f, 45.0f, 0.0f); 
		if (Distance3D == 0) Distance3D = 5;
		Vector3 CameraDirection = transform.rotation.eulerAngles;
		float DistanceXZ = Distance3D * Mathf.Cos(CameraDirection.x * Mathf.Deg2Rad);
		float XOffset = DistanceXZ * Mathf.Sin(CameraDirection.y * Mathf.Deg2Rad);
		float YOffset = Distance3D * Mathf.Sin(CameraDirection.x * Mathf.Deg2Rad);
		float ZOffset = DistanceXZ * Mathf.Cos(CameraDirection.y * Mathf.Deg2Rad);
		Offset = new Vector3(- XOffset, YOffset, - ZOffset); 
	}
}