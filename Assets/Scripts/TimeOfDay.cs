using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOfDay : MonoBehaviour {
	private Light sun;
	public float minutesPerDay;
	private float _time;
	private Vector3 axis;
	private bool isDay;
	public Color dayColor;
	public Color nightColor;
	private Color currentColor;
	
	[ReadOnly]
	public float currentTime;

	// Use this for initialization
	void Start () {
		sun = GetComponent<Light> ();
		axis = sun.transform.right;
		if (sun.transform.forward.y < 0)
			isDay = true;
	}
	
	// Update is called once per frame
	void Update () {
		CurrentTime = CurrentTime + speed * Time.deltaTime;
	}

	public float CurrentTime {
		get {
			return _time;
		}
		set {
			_time = value % 1f;
			SetLight (_time);
			currentTime = _time;
		}
	}

	private void SetLight(float time) {
		bool wasDay = isDay;

		sun.transform.forward = Sunrise; // reset the sun position

		float i;


		if (_time < 0.5f) {
			isDay = true;
			if (wasDay == false) { // change to day time
				sun.color = dayColor;
			}
			i = 2 * _time;
			sun.transform.Rotate (axis, _time * 360f);
		} else {
			isDay = false;
			if (wasDay == true) { // change to night time
				sun.color = nightColor;
			}
			i = (_time - 0.5f) * 2f;
			sun.transform.Rotate (axis, (_time - 0.5f) * 360f);
		}

		sun.intensity = 1f - Mathf.Abs(Mathf.Pow((i - 0.5f) * 2f, 5f));
	}

	private float speed {
		get {
			return (1f / 120f) / minutesPerDay;
		}
	}

	public Vector3 Sunrise {
		get {
			return Vector3.Cross (Vector3.up, axis);
		}
	}
}
