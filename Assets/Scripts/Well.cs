using System;
using Asyncoroutine;
using Events;
using UnityEngine;

public class Well : MonoBehaviour {
	public GameObject depositEffect;
	private Light light;

	private void Start() {
		light = GetComponentInChildren<Light>();
	}

	private void OnTriggerEnter(Collider other) {
		PlayerEntered(other);
	}

	private void PlayerEntered(Collider other) {
		if (other.CompareTag(Tags.PLAYER_TAG)) {
			var player = other.GetComponent<CouchPlayer>();
			var resource = player?.DetachResource();
			if (resource) {
				Deposit(resource, player);
			}
		}		
	}

	private async void Deposit(ResourceData resourceDataObject, CouchPlayer player) {
		//trigger deposit event, with player and resource
		ResourceEvents.InvokeDeposit(resourceDataObject, player);
		//depositEffect.GetComponent<ParticleSystem>().emission.enabled = true;
		Destroy(resourceDataObject.gameObject);
		
		ParticleSystem ps = depositEffect.GetComponent<ParticleSystem>();
		ps.Play(true);
		
		light.enabled = true;
		light.range = 0.5f;
		LeanTween.value(light.gameObject, (float l1, float l2) => {
			light.range = l1;
		}, 1.5f, 0f, ps.main.duration).setEaseOutCirc().setOnComplete(() => {
			light.enabled = false;
		});
		//TOOD:play an animation visual?
	}
}