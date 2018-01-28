using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;


public class CreatePlayers : MonoBehaviour {

	public Transform CouchPlayer;
	public Transform CouchPlayers;
	// Use this for initialization
	void Awake () {
		Transform player;
		int numberOfPlayers = GameState.Instance.numberOfPlayers;
		for (int i = 2; i < numberOfPlayers; i++) {
			player = Instantiate(CouchPlayer);
			player.SetParent (CouchPlayers);
			player.gameObject.GetComponent<ThirdPersonUserControl> ().playerNumber = i;
		}
	}

}
