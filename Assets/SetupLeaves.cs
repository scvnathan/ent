using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupLeaves : MonoBehaviour {
	private string[] leafNodes = {
		"Arm_L-002-Leaf01",
		"Arm_L-003-Leaf02",
		"Arm_R-002-Leaf03",
		"Arm_R-002-Leaf05.001",
		"Arm_R-004-Leaf04",
		"Head_02-Leaf07",
		"Head_02-Leaf08.001",
		"Head_04-Leaf06"
	};

	// Use this for initialization
	void Start () {
		foreach (string node in leafNodes) {
			GameObject go = GameObject.Find (node);
			if (go != null) {
				go.AddComponent<LeafChain> ();
			}
		}

		Destroy (this);
	}
}
