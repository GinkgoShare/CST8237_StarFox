using UnityEngine;
using System.Collections;

public class PlayerManagement : MonoBehaviour {

	public float tiltX;
	public float tiltY;
	public float velocity;

	public GameObject ring;
	public GameObject mainCamera;
	public GameObject barrelRoller;

	public float fireRate;
	public GameObject shotLeft;
	public GameObject shotRight;
	public Transform shotSpawnLeft;
	public Transform shotSpawnRight;

	private float _nextFire = 0.5f;

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
			parent = newRing;
		}
	}

	void FixedUpdate() {
		float moveVertical = Input.GetAxis ("Vertical");
		float moveHorizontal = -Input.GetAxis ("Horizontal");

		Rigidbody rigidBody = gameObject.GetComponent<Rigidbody> ();
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rigidBody.velocity = movement * velocity;
		rigidBody.rotation = Quaternion.Euler (
			rigidBody.velocity.z * -tiltY, 
			rigidBody.velocity.x * -tiltX / 2, 
			rigidBody.velocity.x * -tiltX
		);
		rigidBody.position = new Vector3 (
			rigidBody.position.x + (moveHorizontal * 5.0f), 
			rigidBody.position.y + (moveVertical * -5.0f), 
			rigidBody.position.z + -5.0f
		);

		float cameraX = rigidBody.position.x + 25.0f * moveHorizontal;
		float cameraY = rigidBody.position.y + 15 + (25 * -moveVertical);
		mainCamera.transform.position = new Vector3 (
			Mathf.Clamp (cameraX, rigidBody.position.x - 25.0f, rigidBody.position.x + 25.0f), 
			Mathf.Clamp (cameraY, rigidBody.position.y - 25.0f, rigidBody.position.y + 25.0f), 
			rigidBody.position.z + 50.0f
		);

		barrelRoller.transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
		barrelRoller.transform.rotation = this.transform.rotation;
	}

	void Update() {
		if (Input.GetKeyDown("space") && Time.time > _nextFire) {
			_nextFire = Time.time + fireRate;
			GameObject shot = Instantiate (shotLeft, shotSpawnLeft.position, shotLeft.transform.rotation) as GameObject;
			//shot.transform.parent = this.transform;
			shot = Instantiate (shotRight, shotSpawnRight.position, shotRight.transform.rotation) as GameObject;
			//shot.transform.parent = this.transform;
		}
	}
}
