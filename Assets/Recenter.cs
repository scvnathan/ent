using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recenter : MonoBehaviour {
	public SkinnedMeshRenderer EntMesh;
	public Transform hmd;

	// Use this for initialization
	void Start () {
		transform.forward = EntMesh.transform.forward;

		Vector3 v = EntMesh.bounds.center - hmd.localPosition;
		
		
		v.z -= EntMesh.bounds.size.z / 5f;
		v.y += EntMesh.bounds.size.z / 5f;
		transform.position = v;

		//TODO: need to fix cam rot
		transform.rotation = Quaternion.RotateTowards(EntMesh.transform.parent.rotation, hmd.transform.rotation, EntMesh.transform.parent.rotation.eulerAngles.magnitude);
	}
}
