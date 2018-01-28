using System.Collections;
using UnityEngine;

public class CouchPlayer : MonoBehaviour {

	private Coroutine trackingRoutine;
	private ResourceData attachedResourceData;
	public int PlayerNum { get; private set; }
	
	public void AttachResource(PickupResource pickupResource) {
		var attachedVisualResource = Instantiate(pickupResource.visualResourceToAttach, Vector3.zero, Quaternion.identity);
		attachedResourceData = attachedVisualResource.GetComponent<ResourceData>();
		
		pickupResource.RemoveFromScene();
		trackingRoutine = StartCoroutine(Track(attachedVisualResource.transform));
	}

	public void OnCollisionEnter(Collision other) {
		if (attachedResourceData == null && other.collider.CompareTag(Tags.RESOURCE_TAG)) {
			var rsrc = other.collider.GetComponent<PickupResource>();
			AttachResource(rsrc);
		}
	}
	
	private IEnumerator Track(Transform attachedResourceTransform) {
		while (true) {
			attachedResourceTransform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
			yield return null;
		}
	}

	public ResourceData DetachResource() {
		if (trackingRoutine != null) {
			StopCoroutine(trackingRoutine);
		}
		
		var removedResource = attachedResourceData;
		attachedResourceData = null;
		return removedResource;
	}

	public void Init(int player) {
		PlayerNum = player;
	}
}