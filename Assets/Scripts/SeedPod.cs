using UnityEngine;
using System.Collections.Generic;
using Events;
using Rewired;


public class SeedPod : MonoBehaviour {
	public Canvas hintText;
	public List<CouchPlayer> playersInside;
	private bool ignorePlayers;

	void Update() {
		if (playersInside.Count > 0) {
			var someoneJumped = false;
			for (int i = 0; i < playersInside.Count; i++) {
				if (ReInput.players.GetPlayer(playersInside[i].PlayerNum).GetButtonDown("Jump")) {
					someoneJumped = true;
					break;
				}
			}

			if (someoneJumped) {
				for (int i = 0; i < playersInside.Count; i++) {
					playersInside[i].transform.SetParent(null);
					playersInside[i].gameObject.SetActive(true);
				}

				this.GetComponent<Breakable>().Break();
				BreakEvents.InvokeBreak(this.gameObject, BreakEvents.BreakableThings.SeedPod);
				Destroy(gameObject);
			}
		}
	}

	public void OnTriggerEnter(Collider collider) {
		var colliderTrans = collider.transform;
		if (!ignorePlayers && colliderTrans.CompareTag(Tags.PLAYER_TAG)) {
			var player = colliderTrans.gameObject.GetComponent<CouchPlayer>();
			if (playersInside.Contains(player)) {
				return;
			}

			playersInside.Add(player);
			colliderTrans.SetParent(transform);
			player.gameObject.SetActive(false);
			colliderTrans.localPosition = new Vector3(0f, 0f, 0f);

			LeanTween.alphaCanvas(hintText.GetComponent<CanvasGroup>(), 1f, 1f).setDelay(2f);
		}
	}

	public void IgnorePlayers(bool doIgnore) {
		this.ignorePlayers = doIgnore;
	}
}