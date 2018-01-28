using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafChain : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Rigidbody rb = gameObject.AddComponent<Rigidbody> ();
		rb.mass = 1f;
		rb.drag = 0;
		rb.useGravity = false;
		rb.isKinematic = true;

		Transform t = transform;
		while (t.childCount > 0) {
			t = t.GetChild (0);
			AddNode (t.gameObject);
		}
	}

	private void AddNode(GameObject node) {
		Rigidbody rb = node.AddComponent<Rigidbody> ();
		rb.mass = 0.005f;
		rb.drag = 0.1f;
		rb.angularDrag = 0f;
		rb.useGravity = true;
		rb.isKinematic = false;

		SpringJoint sj = node.AddComponent<SpringJoint> ();
		sj.connectedBody = node.transform.parent.GetComponent<Rigidbody> ();
		sj.spring = 10f;
		sj.damper = 10f;
	}
}
