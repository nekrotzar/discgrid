using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	public float maxHeight = 5.0f;
	public float minHeight = -1.0f;
	public float speed = 5.0f;

	private int direction = 1;

	void UpdateDirection () {

		if (transform.position.y >= maxHeight) 
		{
			direction = -1;
		} 

		else if (transform.position.y <= minHeight) 
		{
			direction = 1;
		}

	}
	
	// Update is called once per frame
	void Update () {
		UpdateDirection ();
		transform.Translate (Vector3.up * speed * direction * Time.deltaTime);
	}
}
