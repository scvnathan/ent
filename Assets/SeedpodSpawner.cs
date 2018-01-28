using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public class SeedpodSpawner : MonoBehaviour {
	[SerializeField] private GameObject seedPod;
	private int numOfSeedsActive;
	private Coroutine spawnerRoutine;
	private List<GameObject> pods;

	[Range(1,4)]
	public int maxPods = 1;

	private void Start() {
		pods = new List<GameObject>();
		spawnerRoutine = StartCoroutine(BeginSpawning());
	}

	private void OnEnable() {
		SeedpodEvents.OnBreak += OnBroke;
	}


	private void OnDisable() {
		SeedpodEvents.OnBreak -= OnBroke;
	}
	
	private void OnBroke(GameObject obj) {
		pods.Remove(obj);
	}

	public IEnumerator BeginSpawning() {
		while (true) {
			if (numOfSeedsActive == 0) {
				var pod = Instantiate(seedPod, transform.position, seedPod.transform.rotation);
				SeedpodEvents.InvokeSpawn(pod);
				pods.Add(pod);
			}
			yield return null;
		}
	}
	
	
	
	public void StopSpawning() {
		if (spawnerRoutine != null) {
			StopCoroutine(spawnerRoutine);
		}
	}
	
}
