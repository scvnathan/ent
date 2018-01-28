using System;
using Events;
using UnityEngine;

public class Well : MonoBehaviour {
	
	
	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag(Tags.PLAYER_TAG)) {
			var player = other.GetComponent<CouchPlayer>();
			var resource = player.DetachResource();
			if (resource) {
				Deposit(resource);
			}
		}
	}

	private void Deposit(ResourceData resourceDataObject) {
		ResourceEvents.InvokeDeposit(resourceDataObject);
		
		Destroy(resourceDataObject.gameObject);
		//TOOD:play an animation visual?
		//trigger deposit event, with player and resource
	}
}