using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {
	public GameObject proxy;
	public float breakThreshhold = 4f;

	private GameObject brokenThing;
	private Vector3 lastVelocity;
	private Rigidbody rb;

	private void Start() {
		this.rb = this.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void OnCollisionEnter() {
		Debug.Log((this.lastVelocity - rb.velocity).magnitude);
		if ((this.lastVelocity - rb.velocity).magnitude > breakThreshhold) {
			Break();
		}
	}

	public void Break() {
		brokenThing = Instantiate(proxy, Vector3.zero, Quaternion.identity);
		brokenThing.SetActive(true);
		brokenThing.transform.SetPositionAndRotation(transform.position, transform.rotation);
		Destroy(this.gameObject, 0f);
	}

	private void Update() {
		this.lastVelocity = rb.velocity;
	}
}