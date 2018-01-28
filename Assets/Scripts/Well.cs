using UnityEngine;

public class Well : MonoBehaviour {
	
	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag(Tags.PLAYER_TAG)) {
			var player = other.GetComponent<CouchPlayer>();
			Deposit(player.DetachResource());
		}
	}

	private void Deposit(ResourceData resourceDataObject) {
		Destroy(resourceDataObject.gameObject);
		//TOOD:play an animation visual?
		//trigger deposit event, with player and resource
	}
}