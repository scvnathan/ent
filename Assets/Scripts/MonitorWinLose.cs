using UnityEngine;
using UnityEngine.SceneManagement;

public class MonitorWinLose : MonoBehaviour {
	public string winScene;
	public string loseScene;
	public TimeOfDay timeOfDay;
	private bool lost;

	// Use this for initialization
	void Start () {
		Events.CreepEvents.OnFinalCreep += OnFinalCreep;
	}

	public void Update() {
		if (!lost && timeOfDay && timeOfDay.CurrentTime >= 0.98f) {
			lost = true;
			SceneManager.LoadScene(loseScene);	
		}
	}

	private void OnFinalCreep() {
		SceneManager.LoadScene(winScene);
	}
}
