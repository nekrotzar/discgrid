using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

	public Transform target;

	void LateUpdate(){
		// Following minimap
		Vector3 position = target.position;
		position.y = transform.position.y;
		transform.position = position;

		// Rotating minimap
		transform.rotation = Quaternion.Euler(90f, target.eulerAngles.y, 0f);
	}
}
