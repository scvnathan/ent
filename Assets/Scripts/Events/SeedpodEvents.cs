using System;
using UnityEngine;

namespace Events {
	public static class SeedpodEvents {
		public static event Action<GameObject> OnSpawn;
		public static event Action<GameObject> OnBreak;

		public static void InvokeBreak(GameObject obj) {
			Action<GameObject> h = OnBreak;
			h?.Invoke(obj);
		}
		
		public static void InvokeSpawn(GameObject obj) {
			Action<GameObject> h = OnSpawn;
			h?.Invoke(obj);
		}
	}
}