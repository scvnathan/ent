using System;
using UnityEngine;

namespace Events {
	public static class BreakEvents {
		public enum BreakableThings {
			Walnut,
			SeedPod,
		}
		
		public static event Action<GameObject> OnSpawn;
		public static event Action<GameObject, BreakableThings> OnBreak;

		public static void InvokeBreak(GameObject obj, BreakableThings breakableThing) {
			Action<GameObject, BreakableThings> h = OnBreak;
			h?.Invoke(obj, breakableThing);
		}
		
		public static void InvokeSpawn(GameObject obj) {
			Action<GameObject> h = OnSpawn;
			h?.Invoke(obj);
		}
	}
}