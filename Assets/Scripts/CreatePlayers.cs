using System.Collections.Generic;
using Cinemachine;
using Rewired;
using Roomera.Core;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;


public class CreatePlayers : MonoBehaviour {
	public Transform CouchPlayer;
	public Camera couchCam;
	[SerializeField] private List<Transform> spawnPoints;
	public CinemachineTargetGroup targetGroup;

	void Awake() {
		Transform player;		
		int numberOfPlayers = GameState.Instance.numberOfPlayers;
		
		for (int i = 0; i < numberOfPlayers; i++) {
			var spawnPoint = new Vector3(spawnPoints[i].position.x, 1f, spawnPoints[i].position.z);
			player = Instantiate(CouchPlayer, spawnPoint, CouchPlayer.transform.rotation);
			player.GetComponent<CouchPlayer>().Init(i);
			player.SetParent(transform);
			var tpuc = player.gameObject.GetComponent<ThirdPersonUserControl>();
			tpuc.playerNumber = i;
			tpuc.couchPlayerCamera = couchCam;
			
			targetGroup.m_Targets[i] = new CinemachineTargetGroup.Target {
				radius = 0,
				weight = 1,
				target = player.transform
			};
		}

		StartCoroutine(VRDeviceBootstrap.EnableVR());
	}
}