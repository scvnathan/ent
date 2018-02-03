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
		if ((this.lastVelocity - rb.velocity).magnitude > breakThreshhold) {
			Break();
		}
	}

	public void Break(Vector3 offsetPos = default(Vector3)) {
		brokenThing = Instantiate(proxy, transform.position + offsetPos, transform.rotation);
		brokenThing.SetActive(true);
		brokenThing.transform.SetPositionAndRotation(transform.position + offsetPos, transform.rotation);
		Destroy(this.gameObject, 0f);
		Destroy(brokenThing, 5f);
	}

	private void Update() {
		this.lastVelocity = rb.velocity;
	}
}