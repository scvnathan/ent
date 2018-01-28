using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	public float amplitude;
	public float speed;
	private float angle;
	private Vector3 startPos;
	private float amplitude2 = 0.05f;
	private float angle2;


	// Use this for initialization
	void Start () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		angle = (angle + speed * Time.deltaTime) % 360f;
		angle2 = (angle2 + Random.Range(180f, 360f) * Time.deltaTime) % 360f;
		transform.position = startPos + transform.forward * (amplitude * Mathf.Cos (Mathf.Deg2Rad * angle) + amplitude) + transform.right * amplitude2 * Mathf.Sin(Mathf.Deg2Rad * angle2);
	}
}
