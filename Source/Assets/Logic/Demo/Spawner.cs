using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour 
{
	// Этот скрипт нужно добавить к родителю точек появления неигровых персонажей
	// Замечание: enemyPrefab will have an AI script attached which will already Tag the Player object 
	// so it won't be needed here.
	
	public Transform[] spawnPoints;  	// Массив использованных точек появления персонажей
	public GameObject[] enemyPrefabs; 	// Массив видов персонажей, которые появятся
	public int amountEnemies = 20;  	// Общее количество персонажей, которые могут появиться в одной точке
	public int yieldTimeMin = 2;  		// Минимальный промежуток между случайными появлениями персонажей
	public int yieldTimeMax = 5;  		// Не превышаемый промежуток времени между появлением персонажей

	// При запуске
	void Start() 
	{
		for (int i = 0; i < amountEnemies; i++) // How many enemies to instantiate total.
		{
//			WaitForSeconds(Random.Range(yieldTimeMin, yieldTimeMax));  // How long to wait before another enemy is instantiated.
//			
//			GameObject obj = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]; // Randomize the different enemies to instantiate.
//			Transform pos = spawnPoints[Random.Range(0, spawnPoints.Length)];  // Randomize the spawnPoints to instantiate enemy at next.
//			
//			Instantiate(obj, pos.position, pos.rotation); 
		} 
	}
}
