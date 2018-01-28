using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recenter : MonoBehaviour {
	public SkinnedMeshRenderer EntMesh;
	public Transform hmd;

	public Transform cameraPosition;

	// Use this for initialization
	void Start () {
		hmd.parent.position = cameraPosition.position - hmd.position;

		hmd.forward = cameraPosition.forward;
		//hmd.parent.localPosition = cameraPosition.position;


//		Vector3 v = EntMesh.bounds.center - hmd.localPosition;
//		
//		v.z -= EntMesh.bounds.size.z / 5f;
//		v.y += EntMesh.bounds.size.z / 5f;
//		transform.position = v;
//
//		//TODO: need to fix cam rot
//		//transform.rotation = Quaternion.FromToRotation(EntMesh.transform.parent.forward, EntMesh.transform.parent.right);
//		
//		EntMesh.transform.parent.rotation = Quaternion.Euler(0f, -90f, 0f);
	}
}
