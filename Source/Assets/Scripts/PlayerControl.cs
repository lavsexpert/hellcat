using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float maxSpeed = 3f;

	// При подготовке к обновлению
	void FixedUpdate () {
		// Нажатие клавиш управлени курсором на клавиатуре приводит к движению персонажа
		float moveX = Input.GetAxis ("Horizontal");
		float moveY = Input.GetAxis ("Vertical");
		rigidbody2D.velocity = new Vector2 (moveX * maxSpeed, moveY * maxSpeed);
	}
}
