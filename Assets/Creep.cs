using Events;
using UnityEngine;

public class Creep : MonoBehaviour {
	public int CurrentCreepValue { get; private set; }
	public int VictoryCreep = 0;
	
	private void OnEnable() {
		ResourceEvents.OnDeposit += TrackGrowth;
	}
	
	private void OnDisable() {
		ResourceEvents.OnDeposit -= TrackGrowth;
	}

	private void TrackGrowth(ResourceData resourceData) {
		CurrentCreepValue += resourceData.value;
	}

	public void Grow() {
		
	}
}
