using UnityEngine;
using System.Collections;

public class PlayerManagement : MonoBehaviour {

	public float tiltX;
	public float tiltY;
	public float velocity;

	public GameObject mainCamera;

//	public GameObject shot;
//	public Transform shotSpawn;
//	public float fireRate = 0.5f;
//
//	float _nextFire = 0.5f;

	void FixedUpdate() {
		Rigidbody rigidBody;
		float moveVertical = Input.GetAxis ("Vertical");
		float moveHorizontal = -Input.GetAxis ("Horizontal");
		Debug.Log (moveHorizontal);
//		Vector3 currentRotation = transform.eulerAngles;
//		currentRotation.x = Mathf.Clamp (currentRotation.x + (moveVertical * -tilt), 315.0f, 45.0f);
//		currentRotation.z = Mathf.Clamp (currentRotation.z + (moveHorizontal * -tilt), 315.0f, 45.0f);
//		transform.eulerAngles = currentRotation;
		//transform.Rotate (new Vector3 (moveVertical * -tilt, 0.0f, moveHorizontal * -tilt));
		rigidBody = gameObject.GetComponent<Rigidbody> ();
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rigidBody.velocity = movement * velocity;
		rigidBody.rotation = Quaternion.Euler (rigidBody.velocity.z * -tiltY, 0.0f, rigidBody.velocity.x * -tiltX);
		rigidBody.position = new Vector3 (
			rigidBody.position.x + (moveHorizontal * 3), 
			rigidBody.position.y + (moveVertical * -3), 
			rigidBody.position.z + -3.0f
		);

		float cameraX = rigidBody.position.x + (25 * moveHorizontal);
//		if (moveHorizontal > 0.1f || moveHorizontal < -0.1f) {
//			cameraX += 25 * moveHorizontal;
//		}
		float cameraY = rigidBody.position.y + 15 + (25 * -moveVertical);
//		if (moveVertical > 0.1f || moveVertical < -0.1f) {
//			cameraY += 25 * -moveVertical;
//		}
		mainCamera.transform.position = new Vector3 (
			Mathf.Clamp(cameraX, rigidBody.position.x-25, rigidBody.position.x+25), 
			Mathf.Clamp(cameraY, rigidBody.position.y-25, rigidBody.position.y+25), 
			rigidBody.position.z + 50
		);

		//transform.Translate(new Vector3(moveHorizontal * 3, moveVertical * -3, -3.0f));
	}

//	void Update() {
//		if (Input.GetButton("Fire1") && Time.time > _nextFire) {
//			_nextFire = Time.time + fireRate;
//			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
//		}
//	}
}
