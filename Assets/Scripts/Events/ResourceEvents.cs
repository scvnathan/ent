using System;

namespace Events {
	public static class ResourceEvents {
		public static event Action<ResourceData, object> OnDeposit;

		public static void InvokeDeposit(ResourceData resourceData, object depositer) {
			var h = OnDeposit;
			h?.Invoke(resourceData, depositer);
		}
	}
}