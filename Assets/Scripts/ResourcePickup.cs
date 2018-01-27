//MD5Hash:d1fc3582b3dfd06c533ced226fa24c2a;
using UnityEngine;
using System;


public class ResourcePickup : MonoBehaviour
{
	public GameObject ResourceToAttach = null;
	public bool HasTree = false;
	public GameObject CurrentResource = null;


	void Start()
	{
	}
	void Update()
	{
	}
	public void OnTriggerEnter(Collider collider)
	{
		if (HasTree)
		{
			Debug.Log("tree");
			collider.transform.CompareTag("Well");
			if (collider.transform.CompareTag("Well"))
			{
				Debug.Log("trying to destroy");
				Destroy(CurrentResource, 0f);
				HasTree = false;
			}

		}
		else
		{
			Debug.Log("no tree");
			if (collider.transform.CompareTag("Tree"))
			{
				CurrentResource = Instantiate(ResourceToAttach, transform);
				CurrentResource.transform.localPosition = new Vector3(0f, 4f, 0f);
				HasTree = true;
				Debug.Log("i have tree");
			}

		}

	}
}
