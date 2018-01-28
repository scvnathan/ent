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
	private float originalGrabbedMass;

	private Transform hand;

	private Vector3 oldPos;
	private Vector3 newPos;
	private Vector3 velocity;

	public List<string> grabbableLayers;
	private SpringJoint joint;

	private Coroutine watchForGrabRoutine;
	private Coroutine watchForLetGoRoutine;
	
	private int layerMasks;

	private void Awake() {
		this.handCollider = GetComponent<SphereCollider>();
		handsRigidbody = GetComponent<Rigidbody>();
		hand = transform.parent;


		layerMasks = LayerMask.GetMask(grabbableLayers.ToArray());
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
		var l = obj.gameObject.layer;
		for (var i = 0; i < grabbableLayers.Count; i++) {
			if (l == LayerMask.NameToLayer(grabbableLayers[i])) {
				return true;
			}
		}

		return false;
	}

	private void OnTriggerEnter(Collider other) {
		if (grabbedObject == null && watchForGrabRoutine == null && IsGrabbableObject(other.gameObject)) {
			watchForGrabRoutine = StartCoroutine(WatchForGrab());
		}
	}

	private void OnTriggerExit(Collider other) {
		if (grabbedObject == null && IsGrabbableObject(other.gameObject)) {
			StopWatchingForGrab();
		}
	}

	private void StopWatchingForGrab() {
		if (watchForGrabRoutine != null) {
			StopCoroutine(watchForGrabRoutine);
			watchForGrabRoutine = null;
		}
	}

	private void StopWatchingForUnGrab() {
		if (watchForLetGoRoutine != null) {
			StopCoroutine(watchForLetGoRoutine);
			watchForLetGoRoutine = null;
		}
		
	}

	private IEnumerator WatchForGrab() {
		var watch = true;
		while (watch) {
			Collider[] colliders = Physics.OverlapSphere(transform.position + Offset, handCollider.radius, layerMasks);
			if (colliders.Length == 0) {
				StopWatchingForGrab();
				watch = false;
			}

			if (ShouldTryToGrab()) {
				StopWatchingForGrab();
				AttachObjectToHand(GetClosestItem(colliders));
				watch = false;
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
		grabbedObject = item.transform;
		this.joint = item.AddComponent<SpringJoint>();
		this.joint.spring = Mathf.Infinity;
		this.joint.minDistance = Vector3.Distance(item.transform.position, hand.position);
		this.joint.maxDistance = this.joint.minDistance;

		grabbedObjectsRigidbody = item.GetComponent<Rigidbody>();
		grabbedObjectsRigidbody.drag = 2f;
		grabbedObjectsRigidbody.angularDrag = 2f;
		grabbedObjectsRigidbody.isKinematic = false;
		//grabbedObjectsRigidbody.useGravity = false;
		this.joint.connectedBody = handsRigidbody;

		watchForLetGoRoutine = StartCoroutine(WatchForLettingGo());
	}

	private void CalculateVelocity() {
		newPos = transform.position;
		var media = (newPos - oldPos);
		velocity = media / Time.deltaTime;
		oldPos = newPos;
		newPos = transform.position;
		//TODO:Angular velocity
	}

	private void DetachObjectFromHand() {
		StopWatchingForUnGrab();
		this.joint.connectedBody = null;
		Destroy(this.joint,0f);
		
		grabbedObjectsRigidbody.useGravity = true;
		grabbedObjectsRigidbody.isKinematic = false;
		grabbedObjectsRigidbody.velocity = grabbedObjectsRigidbody.velocity + velocity * 2f;
		//TODO:
		//grabbedObjectsRigidbody.angularVelocity = handsRigidbody.angularVelocity * 0.8f;			
		grabbedObject = null;
		grabbedObjectsRigidbody = null;
	}
}