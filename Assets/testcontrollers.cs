using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class testcontrollers : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	private void FixedUpdate() {
		Debug.Log(CrossPlatformInputManager.GetAxis("Horizontal_P1"));
		Debug.Log(CrossPlatformInputManager.GetAxis("Horizontal_P2"));
		Debug.Log(CrossPlatformInputManager.GetAxis("Horizontal_P3"));
		Debug.Log(CrossPlatformInputManager.GetAxis("Horizontal_P4"));
		
	}
}
