using System;

namespace Events {
	public static class ResourceEvents {
		public static event Action<ResourceData> OnDeposit;

		public static void InvokeDeposit(ResourceData resourceData) {
			var h = OnDeposit;
			h?.Invoke(resourceData);
		}
	}
}