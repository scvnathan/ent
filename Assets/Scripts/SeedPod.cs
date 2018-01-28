//MD5Hash:92f300e79504c341c574ca0b42f06041;

using UnityEngine;
using System;
using Events;
using Rewired;


public class SeedPod : MonoBehaviour {
	public CouchPlayer player;
	public GameObject floor;

	public void Start() {
		Physics.IgnoreCollision(this.GetComponent<SphereCollider>(), floor.GetComponent<MeshCollider>());
	}
	
	void Update() {
		if (player && ReInput.players.GetPlayer(player.PlayerNum).GetButtonDown("Jump")) {
			player.transform.SetParent(null);
			this.GetComponent<Breakable>().Break();
			player.gameObject.SetActive(true);
			SeedpodEvents.InvokeBreak(this.gameObject);
			Destroy(gameObject);
		}
	}

	public void OnTriggerEnter(Collider collider) {
		var colliderTrans = collider.transform;
		if (colliderTrans.CompareTag(Tags.PLAYER_TAG)) {
			colliderTrans.SetParent(transform);
			player = colliderTrans.gameObject.GetComponent<CouchPlayer>();
			player.gameObject.SetActive(false);
			colliderTrans.localPosition = new Vector3(0f, 0f, 0f);
		}
	}
}