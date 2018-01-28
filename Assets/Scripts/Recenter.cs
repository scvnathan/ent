using UnityEngine;
using UnityEngine.XR;

public class Recenter : MonoBehaviour {
	public SkinnedMeshRenderer EntMesh;
	public Transform hmd;

	public Transform cameraPosition;
	private Vector3 loweredCamPos;

	void Start () {
		RecenterCam();
	}

	public void RecenterCam() {
		hmd.parent.position = cameraPosition.position - hmd.position;
		InputTracking.Recenter();
		
		loweredCamPos = new Vector3(cameraPosition.position.x, 1f, cameraPosition.position.z);
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

	public void Update() {
		if ((loweredCamPos - new Vector3(hmd.position.x, 1f, hmd.position.z)).magnitude > 5f) {
			RecenterCam();
		}
	}
}
