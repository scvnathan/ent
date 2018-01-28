using UnityEngine;

public class GrabConfig : MonoBehaviour {
	public enum GrabTypes {
		Solid,
		Wobble
	}

	public GrabTypes grabType;
	public Transform wobbleAnchor;
}