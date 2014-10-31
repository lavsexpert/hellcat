using UnityEngine;
using System.Collections;

public class Level_Border_Script : MonoBehaviour {

	public Transform Player_Transform;
	public Rigidbody Player_RigidBody;
	public GameObject Level_Border_Object;
	bool Player_Rigidbody_Kinematic_Mode_In_Enter_Moment;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// При столкновении
	void OnTriggerEnter (Collider Trigger)
	{

		if ((Trigger.collider.tag == "Player") &&( Level_Border_Object.tag == "Border_Z_Minus" ))
		{
			Player_Rigidbody_Kinematic_Mode_In_Enter_Moment = Player_RigidBody.isKinematic;
			Player_RigidBody.isKinematic = false;
			Player_Transform.position = Player_Transform.position - new Vector3 (0.0f, 0.0f, 0.3f);
			Player_RigidBody.isKinematic = Player_Rigidbody_Kinematic_Mode_In_Enter_Moment;
		}



		if ((Trigger.collider.tag == "Player") &&( Level_Border_Object.tag == "Border_Z_Plus" ))
		    {
			Player_Rigidbody_Kinematic_Mode_In_Enter_Moment = Player_RigidBody.isKinematic;
			Player_RigidBody.isKinematic = false;
			Player_Transform.position = Player_Transform.position + new Vector3 (0.0f, 0.0f, 0.3f);
			Player_RigidBody.isKinematic = Player_Rigidbody_Kinematic_Mode_In_Enter_Moment;
		}

		if ((Trigger.collider.tag == "Player") &&( Level_Border_Object.tag == "Border_X_Minus" ))
		    {
			Player_Rigidbody_Kinematic_Mode_In_Enter_Moment = Player_RigidBody.isKinematic;
			Player_RigidBody.isKinematic = false;
			Player_Transform.position = Player_Transform.position - new Vector3 (0.3f, 0.0f, 0.0f);
			Player_RigidBody.isKinematic = Player_Rigidbody_Kinematic_Mode_In_Enter_Moment;
		}



		if ((Trigger.collider.tag == "Player") &&( Level_Border_Object.tag == "Border_X_Plus" ))
		    {
			Player_Rigidbody_Kinematic_Mode_In_Enter_Moment = Player_RigidBody.isKinematic;
			Player_RigidBody.isKinematic = false;
			Player_Transform.position = Player_Transform.position + new Vector3 (0.3f, 0.0f, 0.0f);
			Player_RigidBody.isKinematic = Player_Rigidbody_Kinematic_Mode_In_Enter_Moment;
		}


	}



}


