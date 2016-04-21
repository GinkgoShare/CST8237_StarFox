using UnityEngine;
using System.Collections;

public class PlayerManagement : MonoBehaviour {

	public float tilt;
	public float velocity;

//	public GameObject shot;
//	public Transform shotSpawn;
//	public float fireRate = 0.5f;
//
//	float _nextFire = 0.5f;

	void FixedUpdate() {
		Rigidbody rigidBody;
		float moveVertical = Input.GetAxis ("Vertical");
		float moveHorizontal = -Input.GetAxis ("Horizontal");
		rigidBody = gameObject.GetComponent<Rigidbody> ();
		Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);

		rigidBody.velocity = movement * velocity;
		rigidBody.rotation = Quaternion.Euler (rigidBody.velocity.y * -tilt, 0.0f, rigidBody.velocity.x * -tilt);
		rigidBody.position = new Vector3 (rigidBody.position.x, rigidBody.position.y, rigidBody.position.z);
	}

//	void Update() {
//		if (Input.GetButton("Fire1") && Time.time > _nextFire) {
//			_nextFire = Time.time + fireRate;
//			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
//		}
//	}
}
