using UnityEngine;
using System.Collections;

public class PlayerManagement : MonoBehaviour {

	public float tiltX;
	public float tiltY;
	public float velocity;

	public GameObject mainCamera;
	public GameObject ring;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate = 0.5f;

	float _nextFire = 0.5f;

	void Start() {
		GameObject parent = this.gameObject;
		for (int i = 0; i < 50; i++) {
			GameObject newRing = (GameObject)Instantiate (
				ring, 
				new Vector3 (
					Random.Range((parent.transform.position.x - 200.0f), (parent.transform.position.x + 200.0f)),
					//Mathf.Clamp (parent.transform.position.x, (parent.transform.position.x - 200.0f), (parent.transform.position.x + 200.0f)),
					Random.Range((parent.transform.position.y - 200.0f), (parent.transform.position.y + 200.0f)),
					//Mathf.Clamp (parent.transform.position.y, (parent.transform.position.y - 200.0f), (parent.transform.position.y + 200.0f)),
					parent.transform.position.z - 400.0f
				),
				ring.transform.rotation
			);
			newRing.transform.parent = parent.transform;
			parent = newRing;
		}
	}

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
		rigidBody.rotation = Quaternion.Euler (rigidBody.velocity.z * -tiltY, rigidBody.velocity.x * -tiltX / 2, rigidBody.velocity.x * -tiltX);
		rigidBody.position = new Vector3 (
			rigidBody.position.x + (moveHorizontal * 5.0f), 
			rigidBody.position.y + (moveVertical * -5.0f), 
			rigidBody.position.z + -5.0f
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

	void Update() {
		if (Input.GetKeyDown("space") && Time.time > _nextFire) {
			_nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		}
	}
}
