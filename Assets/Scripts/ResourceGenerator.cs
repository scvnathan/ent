using System.Collections.Generic;
using UnityEngine;


//TODO: Change to random spawn on navmesh
public class ResourceGenerator : MonoBehaviour {
	public List<GameObject> resources;
	private float radius;

	[SerializeField, Range(1, 25)] private int numberToSpawnPerResource = 5;
	[SerializeField, Tooltip("If this is toggled the `numberToSpawnPerResource` acts as a maximum to spawn")] private bool randomizeNumbertoSpawn;
	[SerializeField, Range(1, 25)] private int minimumPerResource = 1;


	private void Awake() {
		var scollider = GetComponent<SphereCollider>();
		radius = scollider.radius;
		DestroyImmediate(scollider);
		GenerateResources();
	}

	public void GenerateResources() {
		
		for (var i = 0; i < resources.Count; i++) {
			var numToSpawn = numberToSpawnPerResource;
			if (randomizeNumbertoSpawn) {
				numToSpawn = Random.Range(minimumPerResource, numberToSpawnPerResource);
			}

			for (int j = 0; j < numToSpawn; j++) {
				Spawn(resources[i]);
			}
		}
	}

	private void Spawn(GameObject obj) {
		Vector2 location = Random.insideUnitCircle * radius;
		var spawnedResource = Instantiate(obj, transform.position + new Vector3(location.x, 1f, location.y), obj.transform.rotation);
		spawnedResource.transform.localPosition = spawnedResource.transform.position;
	}
}