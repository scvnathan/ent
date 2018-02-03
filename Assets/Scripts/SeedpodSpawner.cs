using System.Collections;
using System.Collections.Generic;
using Events;
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

	private void Start() {
		foursec = new WaitForSeconds(4f);
		pods = new List<GameObject>();
		spawnerRoutine = StartCoroutine(BeginSpawning());
	}

	private void OnEnable() {
		BreakEvents.OnBreak += OnBroke;
	}


	private void OnDisable() {
		BreakEvents.OnBreak -= OnBroke;
	}

	private void OnBroke(GameObject obj, BreakEvents.BreakableThings breakableThing) {
		if (breakableThing == BreakEvents.BreakableThings.Walnut) {
			pods.Remove(obj);
		}
	}

	public IEnumerator BeginSpawning() {
		while (pods.Count < maxPods) {
			var pod = Instantiate(seedPod,
				new Vector3(transform.position.x, transform.position.y + seedPod.GetComponent<MeshRenderer>().bounds.extents.y, transform.position.z),
				seedPod.transform.rotation);
			var scale = pod.transform.localScale;
			pod.transform.localScale = Vector3.zero;
			pod.GetComponent<SeedPod>().floor = floor;
			BreakEvents.InvokeSpawn(pod);
			pods.Add(pod);

			LeanTween.scale(pod, scale, 1f).setEaseOutBounce();
			yield return foursec;
		}
	}


	public void StopSpawning() {
		if (spawnerRoutine != null) {
			StopCoroutine(spawnerRoutine);
		}
	}
}