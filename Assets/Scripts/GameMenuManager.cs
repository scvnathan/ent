using System;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour {
	public CanvasGroup uiGroup;
	public IntVariable inGamePlayers;
	public string scene;
	public TextMeshProUGUI playersText;
	private int maxPlayers = 4;

	private int defaultPlayers;
	private bool loading;

	private void Awake() {
		defaultPlayers = inGamePlayers.value;
		UpdatePlayersText(inGamePlayers.value);
	}

	public void NewGame() {
		loading = true;
		LeanTween.alphaCanvas(uiGroup, 0f, 3f).setOnComplete(() => {
			SceneManager.LoadScene(scene);
		});
	}

	public void Update() {
		if (loading) {
			return;
		}

		if (ReInput.players.GetPlayer(0).GetButtonDown("start")) {
			NewGame();
		}

		if (ReInput.players.GetPlayer(0).GetButtonDown("DecPlayers")) {
			DecreasePlayers();
		}

		if (ReInput.players.GetPlayer(0).GetButtonDown("IncPlayers")) {
			IncreasePlayers();
		}
	}

	public void IncreasePlayers() {
		inGamePlayers.value = Math.Min(inGamePlayers.value + 1, maxPlayers);
		UpdatePlayersText(inGamePlayers.value);
	}

	public void DecreasePlayers() {
		inGamePlayers.value = Math.Max(inGamePlayers.value - 1, 1);
		UpdatePlayersText(inGamePlayers.value);
	}

	public void UpdatePlayersText(int num) {
		playersText.text = $"<mark=#ffff00aa> {num} </mark> PLAYERS";
	}
}