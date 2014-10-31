using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour 
{

	public float Speed;
	public GUIText CountText;
	private int count;
	public GUIText WinText;

	void FixedUpdate ()
	{

		float MoveHorizontal = Input.GetAxis ("Horizontal");
		float MoveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (MoveHorizontal, 0.0f , MoveVertical);

		//rigidbody.AddForce (movement * Speed * Time.deltaTime);
		rigidbody.position = rigidbody.position + movement/Speed;
	}


}
