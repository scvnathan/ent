using System.Collections.Generic;
using Asyncoroutine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Creep : MonoBehaviour {
	public IntVariable inGamePlayers;
	
	[SerializeField, ReadOnly]
	private int currentResources;
	public int CurrentResources => currentResources;
	
	private int victoryCreep;

	[ReadOnly]
	public int creepStage = 0;

	[SerializeField]
	private List<float> creepStages;

	public float creepGrowthSpeed = 2f;
	private Material creepMaterial;

	private const string CUTOFF = "_Cutoff";

	private async void Awake() {
		victoryCreep = inGamePlayers.value * 50;
		
		this.creepMaterial = this.GetComponent<MeshRenderer>().sharedMaterial;
		SetInitialCreepStage();
	}

	private void SetInitialCreepStage() {
		this.creepMaterial.SetFloat(CUTOFF, creepStages[0]);
	}

	private void OnEnable() {
		Events.ResourceEvents.OnDeposit += TrackGrowth;
	}
	
	private void OnDisable() {
		Events.ResourceEvents.OnDeposit -= TrackGrowth;
	}

	private void TrackGrowth(ResourceData resourceData, object depositer) {
		currentResources += resourceData.value;
		
		if (currentResources % (victoryCreep / creepStages.Count) == 0) {
			Grow();
		}

		if (creepStage == creepStages.Count - 1) {
			Events.CreepEvents.InvokeFinalCreep();
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