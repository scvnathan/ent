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

	public Transform hand;

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


		layerMasks = LayerMask.GetMask(grabbableLayers.ToArray());
	}

	bool ShouldTryToGrab() {
		for (int i = 0; i < InputAxes.Count; i++) {
			InputAxis axis = InputAxes[i];
			Debug.Log(" Trigger down: " + Input.GetAxis(axis.AxisName) + " " + (Input.GetAxis(axis.AxisName) > axis.Threshold));
			
			
			if (Input.GetAxis(axis.AxisName) > axis.Threshold) {
				return true;
			}
		}

		return false;
	}

	bool ShouldLetGo() {
		for (int i = 0; i < InputAxes.Count; i++) {
			InputAxis axis = InputAxes[i];
			Debug.Log(Input.GetAxis(InputAxes[0].AxisName));
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
			watchForGrabRoutine = StartCoroutine(WatchForGrab(other.gameObject));
		}
	}

	private void OnTriggerExit(Collider other) {
		if (grabbedObject == null && IsGrabbableObject(other.gameObject)) {
				Debug.Log("stopping 2");
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

	private IEnumerator WatchForGrab(GameObject objNear) {
		var watch = true;
		while (watch) {
			
			
			//Collider[] colliders = Physics.OverlapSphere(transform.position + handCollider.bounds.center + Offset, handCollider.radius, layerMasks);
			//Debug.Log("colliders: " + colliders.Length);
//			if (colliders.Length == 0) {
//				Debug.Log("stopping");
//				StopWatchingForGrab();
//				watch = false;
//			}

			Debug.Log("check grab");
			if (ShouldTryToGrab()) {
				StopWatchingForGrab();
				AttachObjectToHand(objNear);
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

	private void AttachObjectToHand(GameObject item) {
		if (item == null) {
			return;
		}
		GrabConfig.GrabTypes type = GrabConfig.GrabTypes.Solid;
		GrabConfig grabConfig = item.GetComponent<GrabConfig>();
		if (grabConfig) {
			type = grabConfig.grabType;
		}

		grabbedObject = item.transform;

		grabbedObjectsRigidbody = item.GetComponent<Rigidbody>();
		grabbedObjectsRigidbody.drag = 2f;
		grabbedObjectsRigidbody.angularDrag = 2f;
		grabbedObjectsRigidbody.isKinematic = false;
		grabbedObjectsRigidbody.detectCollisions = false;
		
		switch (type) {
			case GrabConfig.GrabTypes.Solid:
				item.transform.SetParent(hand);
				item.transform.localPosition = Vector3.zero;
				grabbedObjectsRigidbody.isKinematic = true;
				
				break;
			case GrabConfig.GrabTypes.Wobble:
				this.joint = item.AddComponent<SpringJoint>();
				this.joint.spring = Mathf.Infinity;
				this.joint.minDistance = Vector3.Distance(item.transform.position, hand.position);
				this.joint.maxDistance = this.joint.minDistance;
				//grabbedObjectsRigidbody.useGravity = false;
				this.joint.connectedBody = handsRigidbody;
				if (grabConfig && grabConfig.wobbleAnchor) {
					this.joint.connectedAnchor = grabConfig.wobbleAnchor.localPosition;
				}				
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}


		watchForLetGoRoutine = StartCoroutine(WatchForLettingGo());
	}

	private void CalculateVelocity() {
		newPos = hand.position;
		var media = (newPos - oldPos);
		velocity = media / Time.deltaTime;
		oldPos = newPos;
		newPos = hand.position;
		//TODO:Angular velocity
	}

	private void DetachObjectFromHand() {
		StopWatchingForUnGrab();
		GrabConfig.GrabTypes type;
		GrabConfig grabConfigSpecifier = grabbedObject.GetComponentInChildren<GrabConfig>();
		if (grabConfigSpecifier) {
			type = grabConfigSpecifier.grabType;
		} else {
			type = GrabConfig.GrabTypes.Solid;
		}

		grabbedObjectsRigidbody.detectCollisions = true;
		
		switch (type) {
			case GrabConfig.GrabTypes.Solid:
				grabbedObject.parent = null;
				break;
			case GrabConfig.GrabTypes.Wobble:
				this.joint.connectedBody = null;
				Destroy(this.joint, 0f);
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}

		grabbedObjectsRigidbody.useGravity = true;
		grabbedObjectsRigidbody.isKinematic = false;
		grabbedObjectsRigidbody.velocity = grabbedObjectsRigidbody.velocity + velocity * 1.75f;

		//TODO:
		//grabbedObjectsRigidbody.angularVelocity = handsRigidbody.angularVelocity * 0.8f;			
		grabbedObject = null;
		grabbedObjectsRigidbody = null;
	}
}