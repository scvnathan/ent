using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public string playerNumber;
	public float speed;
	public Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal_P" + playerNumber);
		float moveVertical = Input.GetAxis("Vertical_P" + playerNumber);
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f , moveVertical);

		rb.AddForce (movement * speed);
	}



}
