﻿using System.Collections;
using System.Collections.Generic;
using RootMotion;
using UnityEngine;

public class SeedpodSpawner : MonoBehaviour {
	[SerializeField] private GameObject seedPod;
	public int NumOfSeedsActive => pods.Count;
	private Coroutine spawnerRoutine;
	private List<GameObject> pods;

	private WaitForSeconds foursec;

	public GameObject floor;

	[Range(1, 4)] public int maxPods = 1;
	private bool spawning;

	private void Start() {
		foursec = new WaitForSeconds(4f);
		pods = new List<GameObject>();
		spawnerRoutine = StartCoroutine(BeginSpawning());
	}

	private void OnEnable() {
		Events.BreakEvents.OnBreak += OnBroke;
	}


	private void OnDisable() {
		Events.BreakEvents.OnBreak -= OnBroke;
	}

	private void OnBroke(GameObject obj, Events.BreakEvents.BreakableThings breakableThing) {
		if (breakableThing == Events.BreakEvents.BreakableThings.SeedPod) {
			pods.Remove(obj);
		}
	}

	public IEnumerator BeginSpawning() {
		while (true) {
			if (pods.Count < maxPods && !spawning) {
				var pod = Instantiate(seedPod,
					new Vector3(transform.position.x, transform.position.y + seedPod.GetComponent<MeshRenderer>().bounds.extents.y, transform.position.z),
					seedPod.transform.rotation);
				var scale = pod.transform.localScale;
				pod.transform.localScale = Vector3.zero;
				var seedpod = pod.GetComponent<SeedPod>();					
				seedpod.IgnorePlayers(true);
				Events.BreakEvents.InvokeSpawn(pod);
				pods.Add(pod);

				this.spawning = true;
				
				LeanTween.scale(pod, scale, 2f).setEaseOutBounce().setOnComplete(() => {
					spawning = false;
					seedpod.IgnorePlayers(false);
				});
			}

			yield return foursec;
		}
	}


	public void StopSpawning() {
		if (spawnerRoutine != null) {
			StopCoroutine(spawnerRoutine);
		}
	}
}