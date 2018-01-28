using System.Collections.Generic;
using Events;
using UnityEngine;

public class Creep : MonoBehaviour {

	[SerializeField, ReadOnly]
	private int currentResources;
	public int CurrentResources => currentResources;
	
	private int victoryCreep;
	[Range(1,10)]
	public int numberOfCreepSpreadsUntilVictory = 3;

	[ReadOnly]
	public int creepStage;
	private List<int> creepStages = new List<int>();

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
		currentResources += resourceData.value;
		if (currentResources % (victoryCreep / numberOfCreepSpreadsUntilVictory) == 0) {
			Grow();
		}
	}

	public void Grow() {
		//.6
		//5
		//10
		//PlayerEvents.InvokeJump(this);
		//PlayerEvents.OnJump += (couchPlayer) => { }
		
		//LeanTween.
		//TODO: grow
	}
}
