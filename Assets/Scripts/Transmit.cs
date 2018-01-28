using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class Transmit : MonoBehaviour {

	public 

	void OnEnable() {
		PlayerEvents.OnTransmission += CallTree;
	}

	void OnDisable() {
		PlayerEvents.OnTransmission -= CallTree;
	}

	private void CallTree(CouchPlayer couchPlayer){
		
	}
}
