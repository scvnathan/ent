using Asyncoroutine;
using Events;
using UnityEngine;

public class Walnut : MonoBehaviour {
	public int resourcesInside = 4;
	[SerializeField] private GameObject whatsInside;

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag(Tags.PLAYER_HAND)) {
			this.GetComponent<Breakable>().Break(new Vector3(0f, .25f, 0f));
			BreakEvents.InvokeBreak(this.gameObject, BreakEvents.BreakableThings.Walnut);
			Destroy(gameObject);

			ExplodeTheResources();		
		}
	}

	private void ExplodeTheResources() {
		Instantiate(whatsInside, transform.position, whatsInside.transform.rotation);
		Instantiate(whatsInside, transform.position, whatsInside.transform.rotation);
	}
}