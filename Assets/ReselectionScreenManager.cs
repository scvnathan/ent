using System.Collections.Generic;
using Asyncoroutine;
using Rewired;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReselectionScreenManager : MonoBehaviour {	
	public GameObject retry;
	public GameObject main;
	public GameObject selectyDotThing;
	private RectTransform dot;

	private LinkedListNode<GameObject> currentTarget;
	private LinkedList<GameObject> targets = new LinkedList<GameObject>();
	private Player firstPlayer;


	public async void Start() {
		currentTarget = targets.AddFirst(retry);
		targets.AddAfter(currentTarget, main);

		firstPlayer = ReInput.players.GetPlayer(0);
		dot = selectyDotThing.GetComponent<RectTransform>();
	}

	public void Update() {
		if (firstPlayer.GetButtonDown("D_Up")) {
			currentTarget = currentTarget.Previous ?? targets.Last;
		}

		if (firstPlayer.GetButtonDown("D_Down")) {
			currentTarget = currentTarget.Next ?? targets.First;
		}

		UpdateSelectionDot();

		if (firstPlayer.GetButtonDown("A")) {
			Process();
		}
	}

	private void UpdateSelectionDot() {
		GameObject go = currentTarget.Value;
		var p = go.GetComponent<RectTransform>().anchoredPosition;
		
		dot.anchoredPosition = new Vector2(dot.anchoredPosition.x, p.y);
	}

	private void Process() {
		GameObject go = currentTarget.Value;
		if (go == retry) {
			SceneManager.LoadScene("Main");	
		}

		if (go == main) {
			SceneManager.LoadScene("Start");
		}
	}
}
