using UnityEngine;
using UnityEngine.SceneManagement;

public class MonitorWinLose : MonoBehaviour {
	private string winScene;
	private string loseScene;
	public TimeOfDay timeOfDay;

	// Use this for initialization
	void Start () {
		Events.CreepEvents.OnFinalCreep += OnFinalCreep;
	}

	public void Update() {
		if (timeOfDay && timeOfDay.CurrentTime == 1f) {
			SceneManager.LoadScene(loseScene);	
		}
	}

	private void OnFinalCreep() {
		SceneManager.LoadScene(winScene);
	}
}
