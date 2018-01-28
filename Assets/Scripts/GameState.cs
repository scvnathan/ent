using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

	public int numberOfPlayers;

	public static GameState Instance;

	void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (this);
		} else if (this != Instance){
			Destroy (this.gameObject);			
		}
	}

}
