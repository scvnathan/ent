//MD5Hash:d1fc3582b3dfd06c533ced226fa24c2a;

using System.Collections;
using UnityEngine;

public class ResourcePickup : MonoBehaviour {
	public GameObject resourceToAttach;
	private GameObject currentResource;
	
	private bool hasResource;

	public void OnTriggerEnter(Collider collider) {
		var rsrc = collider.GetComponent<PickupResource>();
		if (rsrc) {
			//currentResource = Instantiate(rsrc.ResourceToAttachPrefab, Vector3.zero, Quaternion.identity);
			
		}
		
		
//		if (hasResource) {
//			if (collider.transform.CompareTag("Well")) {
//				Destroy(currentResource, 0f);
//				hasResource = false;
//			}
//		} else if (collider.transform.CompareTag("Pickup")) {
//			currentResource = Instantiate(resourceToAttach, transform);
//			currentResource.AddComponent<BoxCollider>().isTrigger = true;
//			currentResource.transform.localPosition = new Vector3(0f, 2f, 0f);
//			
//			AttachToPlayer();
//			hasResource = true;
//		}
	}

	private void AttachToPlayer() {
		//StartCoroutine(Track());
	}

//	private IEnumerator Track() {
//		if (  ) {
//			
//		}
//	}

	private void Detatch() {
		
	}
	
}