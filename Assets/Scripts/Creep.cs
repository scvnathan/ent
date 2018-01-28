using System.Collections.Generic;
using Asyncoroutine;
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
	public int creepStage = 0;

	[SerializeField]
	private List<float> creepStages;

	public float creepGrowthSpeed = 2f;
	private Material creepMaterial;

	private const string CUTOFF = "_Cutoff";

	private async void Awake() {
		//victoryCreep = GameState.Instance.numberOfPlayers * 50;
		victoryCreep = 2* 50;
		
		this.creepMaterial = this.GetComponent<MeshRenderer>().sharedMaterial;
		SetInitialCreepStage();
		
		await new WaitForSeconds(5f);
		Grow();

		await new WaitForSeconds(5f);

		Grow();
		
		await new WaitForSeconds(5f);

		Grow();
		
		await new WaitForSeconds(5f);

		Grow();


	}

	private void SetInitialCreepStage() {
		this.creepMaterial.SetFloat(CUTOFF, creepStages[0]);
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
		creepStage = creepStage + 1;
		if (creepStage <= creepStages.Count-1) {
			AnimateGrow(creepStages[creepStage]);
		}
	}

	private void AnimateGrow(float nextStageValue) {
		var currentCreepValue = this.creepMaterial.GetFloat(CUTOFF);

		LeanTween.value(this.gameObject, (float f1, float f2) => {
			creepMaterial.SetFloat(CUTOFF, f1);
		}, currentCreepValue, nextStageValue, creepGrowthSpeed).setEaseOutCubic();
	}
}