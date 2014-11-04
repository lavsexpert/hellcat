using UnityEngine;
using System.Collections;

public class NavMesh_Simple : MonoBehaviour 
{
	private NavMeshAgent Agent;	// Агент навигации по сетке

	// При запуске
	void Start() 
	{
		// Создание агента навигации по сетке
		Agent = GetComponent<NavMeshAgent>();
	}

	// При обновлении сцены
	void Update() 
	{
		RaycastHit Hit;
		// Если нажата левая кнопка мыши:
		if (Input.GetMouseButtonDown(0)) 
		{
			// Формируется луч от камеры до того места, где была нажата кнопка мыши
			Ray HitRay = Camera.main.ScreenPointToRay(Input.mousePosition);

			// И если луч попадает в какой-то объект, то игровой персонаж движется к этому объекту 
			if (Physics.Raycast(HitRay, out Hit)) 
			{
				Agent.SetDestination(Hit.point);
			}
		}
	}
}


