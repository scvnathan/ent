using Events;
using UnityEngine;

public class Creep : MonoBehaviour {

	[SerializeField, ReadOnly]
	private int currentCreep;
	public int CurrentCreep => currentCreep;
	
	private int victoryCreep;
	[Range(1,10)]
	public int numberOfCreepSpreadsUntilVictory = 3;

	private void Awake() {
		victoryCreep = GameState.Instance.numberOfPlayers * 50;
	}

	private void OnEnable() {
		ResourceEvents.OnDeposit += TrackGrowth;
	}
	
	private void OnDisable() {
		ResourceEvents.OnDeposit -= TrackGrowth;
	}

	private void TrackGrowth(ResourceData resourceData) {
		currentCreep += resourceData.value;
		if (currentCreep % (victoryCreep / numberOfCreepSpreadsUntilVictory) == 0) {
			Grow();
		}
	}

	public void Grow() {
		//PlayerEvents.InvokeJump(this);
		//PlayerEvents.OnJump += (couchPlayer) => { }
		
		//TODO: grow
	}
}
