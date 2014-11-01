using UnityEngine;
using System.Collections;

public class Level_Border_Script : MonoBehaviour {

	public Transform Player_Transform;
	public Rigidbody Player_RigidBody;
	public GameObject Level_Border_Object;
	bool Player_Rigidbody_Kinematic_Mode_In_Enter_Moment;

	public static bool Level_Border_Touched = false;
	private bool Local_Level_Border_Touched = false;
	private bool Local_Level_Border_Access = true;



 


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	// При столкновении
	void OnTriggerEnter (Collider Trigger)
	{

		 Transform Player_Buffer;
	
	








		if ((Trigger.collider.tag == "Player") && (Level_Border_Object.tag == "Border_Z_Minus")) {

						Player_Rigidbody_Kinematic_Mode_In_Enter_Moment = Player_RigidBody.isKinematic;
						Player_RigidBody.isKinematic = false;
						
			if (Player_Rigidbody_Kinematic_Mode_In_Enter_Moment == true)
			{
				Player_Transform.position = Player_Transform.position - new Vector3 (0.0f, 0.0f, 0.5f);

			}
			else{

			Player_Transform.position = Player_Transform.position - new Vector3 (0.0f, 0.0f, 0.2f);
			}
				Player_Transform.rotation = Quaternion.Euler (0.0f, -90.0f, 0.0f);
						Player_RigidBody.isKinematic = Player_Rigidbody_Kinematic_Mode_In_Enter_Moment;


			Level_Border_Touched = true;
				}



		if ((Trigger.collider.tag == "Player") &&( Level_Border_Object.tag == "Border_Z_Plus" ))
		    {
			Player_Rigidbody_Kinematic_Mode_In_Enter_Moment = Player_RigidBody.isKinematic;
			Player_RigidBody.isKinematic = false;

			if (Player_Rigidbody_Kinematic_Mode_In_Enter_Moment == true)
			{
			Player_Transform.position = Player_Transform.position + new Vector3 (0.0f, 0.0f, 0.5f);
			}
			else
			{
				Player_Transform.position = Player_Transform.position + new Vector3 (0.0f, 0.0f, 0.2f);

			}
				Player_Transform.rotation = Quaternion.Euler (0.0f, 90.0f, 0.0f);

			Player_RigidBody.isKinematic = Player_Rigidbody_Kinematic_Mode_In_Enter_Moment;
			Level_Border_Touched = true;
		}

		if ((Trigger.collider.tag == "Player") &&( Level_Border_Object.tag == "Border_X_Minus" ))
		    {
			Player_Rigidbody_Kinematic_Mode_In_Enter_Moment = Player_RigidBody.isKinematic;
			Player_RigidBody.isKinematic = false;
			if (Player_Rigidbody_Kinematic_Mode_In_Enter_Moment == true)
			{
			Player_Transform.position = Player_Transform.position - new Vector3 (0.5f, 0.0f, 0.0f);
			}
			else
			{
				Player_Transform.position = Player_Transform.position - new Vector3 (0.2f, 0.0f, 0.0f);

			}
				Player_Transform.rotation = Quaternion.Euler (0.0f, 360.0f, 0.0f);
			Player_RigidBody.isKinematic = Player_Rigidbody_Kinematic_Mode_In_Enter_Moment;
			Level_Border_Touched = true;
		}



		if ((Trigger.collider.tag == "Player") &&( Level_Border_Object.tag == "Border_X_Plus" ))
		    {
			Player_Rigidbody_Kinematic_Mode_In_Enter_Moment = Player_RigidBody.isKinematic;
			Player_RigidBody.isKinematic = false;
			if (Player_Rigidbody_Kinematic_Mode_In_Enter_Moment == true)
			{
			Player_Transform.position = Player_Transform.position + new Vector3 (0.5f, 0.0f, 0.0f);
			}
			else
			{
				Player_Transform.position = Player_Transform.position + new Vector3 (0.2f, 0.0f, 0.0f);

			}
				Player_Transform.rotation = Quaternion.Euler (0.0f, 180.0f, 0.0f);
			Player_RigidBody.isKinematic = Player_Rigidbody_Kinematic_Mode_In_Enter_Moment;
			Level_Border_Touched = true;
		}


	}


	/*void OnTriggerExit (Collider Trigger)
	{
		if (Level_Border_Object.transform.position.z > Player_Transform.position.z) {
						Level_Border_Touched = false;
				} else {
				
		}
		}
*/






}


