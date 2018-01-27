using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Grabber : MonoBehaviour {
	[Serializable]
	public struct InputAxis {
		public string AxisName;
		public float Threshold;
	}

	private float zeroThreshold = 0.05f;
	public List<InputAxis> InputAxes;
	public Vector3 Offset = Vector3.zero;
	public float PositionalSmoothing = 0.1f;
	public float RotationalSmoothing = 0.1f;

	public int GrabbableLayer = 11;
	//public float Range = 0.25f;

	bool isGrabbing = false;
	private SphereCollider handCollider;

	Transform grabbedObject = null;
	private Rigidbody grabbedObjectsRigidbody;
	private Rigidbody handsRigidbody;
	
	private bool canGrab;
	private GameObject objectInRange;
	private Transform hand;
	
	private Vector3 oldPos;
	private Vector3 newPos;
	private Vector3 velocity;
	


	private void Awake() {
		this.handCollider = GetComponent<SphereCollider>();
		handsRigidbody = GetComponent<Rigidbody>();
		hand = transform.parent;
	}

	bool ShouldTryToGrab() {
		for (int i = 0; i < InputAxes.Count; i++) {
			InputAxis axis = InputAxes[i];
			if (Input.GetAxis(axis.AxisName) > axis.Threshold) {
				return true;
			}
		}

		return false;
	}

	bool ShouldLetGo() {
		for (int i = 0; i < InputAxes.Count; i++) {
			InputAxis axis = InputAxes[i];
			if (Input.GetAxis(axis.AxisName) < zeroThreshold) {
				return true;
			}
		}

		return false;		
	}


private bool IsGrabbableObject(GameObject obj) {
		return obj.gameObject.layer == LayerMask.NameToLayer("Grabbable");
	}

	private void OnTriggerEnter(Collider other) {
		if (IsGrabbableObject(other.gameObject)) {
			StartCoroutine(WatchForGrab());
		}		
	}

	private void StopWatchingForGrab() {
		StopCoroutine(WatchForGrab());
	}

	private IEnumerator WatchForGrab() {
		while (true) {
			Collider[] colliders = Physics.OverlapSphere(transform.position + Offset, handCollider.radius, 1 << GrabbableLayer);
			if (colliders.Length == 0) {
				StopWatchingForGrab();
				break;
			}

			if (ShouldTryToGrab()) {
				AttachObjectToHand(GetClosestItem(colliders));
			}

			yield return null;
		}
	}

	private IEnumerator WatchForLettingGo() {
		while (true) {
			CalculateVelocity();
			if (ShouldLetGo()) {
				DetachObjectFromHand();
				break;
			}

			yield return null;
		}
	}

	public GameObject GetClosestItem(Collider[] colliders) {
		GameObject closest = null;
		float minDist = Mathf.Infinity;
		for (int i = 0; i < colliders.Length; i++) {
			float dist = (colliders[i].transform.position - transform.position).magnitude;
			if (dist < minDist) {
				closest = colliders[i].gameObject;
				minDist = dist;
			}
		}

		return closest;
	}

	//TODO: Change from parenting to using joint
	private void AttachObjectToHand(GameObject item) {
		item.transform.SetParent(hand);
		grabbedObject = item.transform;
		grabbedObjectsRigidbody = item.GetComponent<Rigidbody>();
		if (grabbedObjectsRigidbody) {
			grabbedObjectsRigidbody.isKinematic = true;
		}
		
		StartCoroutine(WatchForLettingGo());
	}

	private void CalculateVelocity() {
		newPos = transform.position;
		var media =  (newPos - oldPos);
		velocity = media /Time.deltaTime;
		oldPos = newPos;
		newPos = transform.position;
		
		//TODO:Angular velocity
	}
	
	private void DetachObjectFromHand() {
		grabbedObject.parent = null;
		if (grabbedObjectsRigidbody) {
			grabbedObjectsRigidbody.isKinematic = false;
			grabbedObjectsRigidbody.velocity = velocity * 1.5f;
			//TODO:
			//grabbedObjectsRigidbody.angularVelocity = handsRigidbody.angularVelocity * 0.8f;			
		}

		grabbedObjectsRigidbody = null;
	}
}