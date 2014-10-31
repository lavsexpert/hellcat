using UnityEngine;
using System.Collections;

public class Camera_Controller : MonoBehaviour
{
	
	public GameObject Player; 	// Игровой персонаж
	private Vector3 Offset;		// Вектор смещения
	
	// При запуске
	void Start() 
	{
		// Сохраняется исходная позиция камеры
		Offset = transform.position;
	}
	
	// После обновления сцены
	void LateUpdate()
	{	
		// Позиция камеры смещается на позицию персонажа (т.е. следует за персонажем) 
		transform.position = Player.transform.position + Offset;
	}
}