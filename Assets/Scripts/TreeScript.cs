//MD5Hash:8c7be0ee7dde704b4f28f7b71a2bf940;
using UnityEngine;
using System;


public class TreeScript : MonoBehaviour
{


	void Start()
	{
	}
	void Update()
	{
	}
	public void OnTriggerEnter(Collider collider)
	{
		collider.transform.CompareTag("Player");
		if (collider.transform.CompareTag("Player"))
		{
			Destroy(gameObject, 0f);
		}

	}
}
