using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {
	public GameObject proxy;
	public float breakThreshhold = 4f;

	private GameObject brokenThing;
	private Vector3 lastVelocity;

	private void Start() {
		brokenThing = Instantiate(proxy, Vector3.zero, Quaternion.identity);
		brokenThing.SetActive(false);
	}

	// Update is called once per frame
	void OnCollisionEnter() {
		if ((this.lastVelocity - this.GetComponent<Rigidbody>().velocity).magnitude > breakThreshhold) {
			Break();
		}
	}

	public void Break() {
		//Debug.Log((this.lastVelocity - this.GetComponent<Rigidbody>().velocity).magnitude);
		this.gameObject.SetActive(false);
		brokenThing.SetActive(true);
		brokenThing.transform.SetPositionAndRotation(transform.position, transform.rotation);
		Destroy(this, 1f);
	}

	private void Update() {
		this.lastVelocity = this.GetComponent<Rigidbody>().velocity;
	}
}