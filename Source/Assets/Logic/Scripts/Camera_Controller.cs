using UnityEngine;
using System.Collections;

public class Camera_Controller : MonoBehaviour
{
	
	public GameObject Player;
	private Vector3 offset;
	private 
	
	
	// Use this for initialization
	void Start () 
	{
		
		offset = transform.position;
		//transform.Rotate (-45,0,0, Space.World);
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		
		transform.position = Player.transform.position + offset;


	}
}