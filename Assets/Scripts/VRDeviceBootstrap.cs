using System.Collections;

namespace Roomera.Core {
	public static class VRDeviceBootstrap {
		public static IEnumerator EnableVR() {
			foreach (var device in UnityEngine.XR.XRSettings.supportedDevices) {
				if (!device.Equals("None")) {
					UnityEngine.XR.XRSettings.LoadDeviceByName(device);
					yield return null;
					
					if (UnityEngine.XR.XRSettings.loadedDeviceName == device) {
						break;
					}
				}
			}
			UnityEngine.XR.XRSettings.enabled = true;
		}
	}

}