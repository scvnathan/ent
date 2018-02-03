using UnityEngine;

public class waterBob : MonoBehaviour {
	private Transform trans;
	private Vector3 startingPos;
	
	private void Start() {
		trans = transform;
		startingPos = trans.position;
	}

	// Update is called once per frame
	void Update () {
		var y = Mathf.Sin(Time.time) / 55;
		trans.position = new Vector3(trans.position.x, startingPos.y + y, trans.position.z); 
	}
}
