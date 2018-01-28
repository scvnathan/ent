//MD5Hash:92f300e79504c341c574ca0b42f06041;
using UnityEngine;
using System;


public class SeedPod : MonoBehaviour
{
	public GameObject Player = null;
	

	void Update()
	{
		Input.GetKey(KeyCode.Space);
		Input.GetKey(KeyCode.Joystick1Button0);
		if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button0)))
		{
			Player.transform.SetParent(null);
			Destroy(gameObject);
			Player.SetActive(true);
		}

	}
	public void OnTriggerEnter(Collider collider)
	{
		var colliderTrans = collider.transform;
		if (colliderTrans.CompareTag(Tags.PLAYER_TAG))
		{
			colliderTrans.SetParent(transform);
			Player = colliderTrans.gameObject;
			Player.SetActive(false);
			colliderTrans.localPosition = new Vector3(0f, 0f, 0f);
		}

	}
}
