using UnityEngine;
using System.Collections;

public class MoveBolts : MonoBehaviour {

	public float speed;

	void Start() {
		Rigidbody rigidBody = gameObject.GetComponent<Rigidbody> ();
		rigidBody.velocity = -transform.forward * speed;
	}

}
