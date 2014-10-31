using UnityEngine;
using System.Collections;

public class Enemy_Script : MonoBehaviour {

	public Transform Player;
	public float Move_Speed;
	public float Rotation_Speed;
	public Transform Enemy;

	// Update is luanching every fps.
	void Update ()
	{
		var Look_Dir = Player.position - Enemy.position; 
		Look_Dir.y = 0;
		Enemy.rotation = Quaternion.Slerp (Enemy.rotation, Quaternion.LookRotation (Look_Dir), Rotation_Speed*Time.deltaTime);
		Enemy.position += Enemy.forward * Move_Speed * Time.deltaTime;  

	}
}
