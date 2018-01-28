using System.Collections;
using System.Collections.Generic;
using Roomera.Core;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;


public class CreatePlayers : MonoBehaviour {

	public Transform CouchPlayer;
	
	void Awake () {
		Transform player;
		int numberOfPlayers = GameState.Instance.numberOfPlayers;
		for (int i = 1; i < numberOfPlayers; i++) {
			player = Instantiate(CouchPlayer);
			player.SetParent (transform);	
			player.gameObject.GetComponent<ThirdPersonUserControl> ().playerNumber = i;
		}

		StartCoroutine(VRDeviceBootstrap.EnableVR());
	}
	
	

}
