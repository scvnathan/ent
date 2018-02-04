using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

	public IntVariable inGamePlayers;
	public string scene;
	public Text PlayerBtnText;

	private int defaultPlayers;


	private void Awake() {
		defaultPlayers = inGamePlayers.value;
		UpdatePlayersText(inGamePlayers.value);
	}

	public void NewGame() {
		SceneManager.LoadScene (scene);
	}

	public void SetPlayers(){
		int n = inGamePlayers.value;
		if (n == 5) {
			inGamePlayers.value = defaultPlayers;
		} else {
			inGamePlayers.value++;
		}
		UpdatePlayersText(inGamePlayers.value);
	}

	public void UpdatePlayersText(int num) {
		PlayerBtnText.text = "Couch Players: " + num;
	}

}
