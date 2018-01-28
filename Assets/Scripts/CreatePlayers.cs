using System.Collections.Generic;
using Roomera.Core;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;


public class CreatePlayers : MonoBehaviour {
	public Transform CouchPlayer;
	[SerializeField] private List<Transform> spawnPoints;

	void Awake() {
		Transform player;
		int numberOfPlayers = GameState.Instance.numberOfPlayers;
		for (int i = 1; i < numberOfPlayers; i++) {
			var spawnPoint = new Vector3(spawnPoints[i].position.x, spawnPoints[i].position.y + 1f, spawnPoints[i].position.z);
			player = Instantiate(CouchPlayer, spawnPoint, CouchPlayer.transform.rotation);
			player.SetParent(transform);
			player.gameObject.GetComponent<ThirdPersonUserControl>().playerNumber = i;
		}

		StartCoroutine(VRDeviceBootstrap.EnableVR());
	}
}