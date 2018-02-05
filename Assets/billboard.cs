using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour {
	public Transform couchCam;

	private void Awake() {
		this.couchCam = GameObject.FindGameObjectWithTag(Tags.COUCH_CAM).transform;
	}

	// Update is called once per frame
	void Update() {
		if (this.couchCam) {
			transform.LookAt(this.couchCam);
			transform.forward = -transform.forward;
		}
	}
}