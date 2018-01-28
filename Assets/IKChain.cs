using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class IKChain : MonoBehaviour {
	public Transform target;
	private string chainName;
	private FABRIK ik;


	// Use this for initialization
	void Start () {
		chainName = name.Split ('-')[0];

		ik = gameObject.AddComponent<FABRIK> ();

		List<IKSolver.Bone> bones = new List<IKSolver.Bone> ();

		Transform t = transform;
		int count = int.Parse(t.name.Split ('-')[1]);

		do {
			bones.Add(new IKSolver.Bone(t));
			RotationLimitAngle rl = t.gameObject.AddComponent<RotationLimitAngle>();
			rl.axis = t.forward;
			rl.limit = 60f;
			rl.twistLimit = 90f;
			string n = string.Format("{0}-{1:000}", chainName, ++count);
			t = t.Find(n);
		} while (t != null);

		ik.solver.bones = bones.ToArray ();
		ik.solver.target = target;
		Destroy (this);
	}

	/*
	void Start () {
		chainName = name.Split ('-')[0];

		ik = gameObject.AddComponent<FABRIK> ();

		List<Transform> bones = new List<Transform> ();

		Transform t = transform;
		int count = int.Parse(t.name.Split ('-')[1]);

		do {
			bones.Add(t);
			RotationLimitAngle rl = t.gameObject.AddComponent<RotationLimitAngle>();
			rl.axis = t.forward;
			rl.limit = 60f;
			rl.twistLimit = 90f;
			string n = string.Format("{0}-{1:000}", chainName, ++count);
			t = t.Find(n);
		} while (t != null);

		ik.solver.bones = bones.ToArray ();
		ik.solver.target = target;
		Destroy (this);
	}*/
}
