using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour {

	public GameObject destroyedPrefab;


	void OnCollisionEnter(Collision collision)
	{

		if (collision.gameObject.name.Equals ("Disk") || collision.gameObject.name.Equals ("Bullet"))
		{
			Instantiate (destroyedPrefab, gameObject.transform.position, gameObject.transform.rotation);
			Destroy(gameObject);

		}

	}

}
