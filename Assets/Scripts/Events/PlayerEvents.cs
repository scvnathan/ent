using System;

namespace Events {
	public static class PlayerEvents {
		public static event Action<CouchPlayer> OnJump;
		
		public static void InvokeJump(CouchPlayer couchPlayer) {
			var h = OnJump;
			h?.Invoke(couchPlayer);
		}

		public static event Action<CouchPlayer> OnCall;

		public static void InvokeCall(CouchPlayer CouchPlayer){
			var h = OnCall;
			h?.Invoke(CouchPlayer);
		}
	}
}
