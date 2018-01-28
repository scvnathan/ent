using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

	public GameState gameState;
	public string scene;
	public Text PlayerBtnText;

	public void NewGame() {
		SceneManager.LoadScene (scene);
	}

	public void SetPlayers(){
		int n = gameState.numberOfPlayers;
		if (n == 5) {
			gameState.numberOfPlayers = 2;
		} else {
			gameState.numberOfPlayers++;
		}
		PlayerBtnText.text = "# of Players: " + gameState.numberOfPlayers;
	}

}
