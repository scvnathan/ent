using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class GameState : MonoBehaviour {

	public int numberOfPlayers;
	public static GameState Instance;

	private int count = 0;

	void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (this);
		} else if (this != Instance){
			Destroy (this.gameObject);			
		}
	}
}
