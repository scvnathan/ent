using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public class Transmitter : MonoBehaviour {

	private Material material;
	public List<Texture> sprites;
	private float waitDelay;
	private bool perFrame;
	private WaitForSeconds secondsWaiter;

	private int count;
	private Coroutine trackingRoutine;
	private Coroutine animateAsync;

	private void Awake() {
		material = this.GetComponent<MeshRenderer>().material;
	}

	private void OnEnable() {
		PlayerEvents.OnTransmission += OnTransmission;
	}
	
	private void OnDisable() {
		PlayerEvents.OnTransmission -= OnTransmission;
	}

	private void OnTransmission(CouchPlayer obj) {
		Transmit(obj, 0.1f);
	}

	public void Transmit(CouchPlayer player, float wait = 1f, bool perFrame = false) {
		waitDelay = wait;
		this.perFrame = perFrame;
		secondsWaiter = new WaitForSeconds(wait);
		animateAsync = StartCoroutine(AnimateAsync());
		trackingRoutine = StartCoroutine(TrackPlayer(player.transform));


	}

	private IEnumerator TrackPlayer(Transform player) {
		while (true) {
			transform.position = new Vector3(player.position.x, player.position.y + 0.5f, player.position.z);
			yield return null;
		}
	}


	private IEnumerator AnimateAsync() {
		while (count < sprites.Count-1) {
			material.mainTexture = sprites[count++];
			if (perFrame) {
				yield return null;
			} else {
				yield return secondsWaiter;
			}
		}
		
		StopCoroutine(animateAsync);
		StopCoroutine(trackingRoutine);
	}
}
