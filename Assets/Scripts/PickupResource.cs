using UnityEngine;

public class PickupResource : MonoBehaviour {
	public GameObject visualResourceToAttach;

	public void RemoveFromScene() {
		this.GetComponent<MeshRenderer>().enabled = false;
		Destroy(this.gameObject);
	}

}