using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManagement : MonoBehaviour {

	public float tiltX;
	public float tiltY;
	public float velocity;
	public float fireRate;
	public float enemySpawnRate;

	public Text scoreText;

	public GameObject mainCamera;
	public GameObject barrelRoller;

	public GameObject enemy;
	public GameObject shotLeft;
	public GameObject shotRight;
	public Transform shotSpawnLeft;
	public Transform shotSpawnRight;
	public Transform enemySpawnLeft;
	public Transform enemySpawnRight;

	private int _score = 0;
	private bool _isEnemyLeft = false;
	private float _nextFire;
	private float _nextEnemySpawn;
	private bool _shouldSpawnEnemies = false;

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
		if (Input.GetKey(KeyCode.Space) && Time.time > _nextFire) {
			Rigidbody rigidBody = this.GetComponent<Rigidbody> ();
			Vector3 vecVel = new Vector3 (rigidBody.velocity.x, rigidBody.velocity.y * 600, rigidBody.velocity.z);
			_nextFire = Time.time + fireRate;
			GameObject shot = Instantiate (shotLeft, shotSpawnLeft.position, shotLeft.transform.rotation) as GameObject;
			shot.GetComponent<Rigidbody> ().velocity = -transform.forward * 600.0f;
			//shot.transform.parent = this.transform;
			shot = Instantiate (shotRight, shotSpawnRight.position, shotRight.transform.rotation) as GameObject;
			shot.GetComponent<Rigidbody> ().velocity = -transform.forward * 600.0f;
			//shot.transform.parent = this.transform;
		}
		if (Time.time > _nextEnemySpawn) {
			GameObject shot = Instantiate (
				enemy, 
				(_isEnemyLeft ? enemySpawnLeft.position : enemySpawnRight.position), 
				Quaternion.identity
			) as GameObject;
			_nextEnemySpawn = Time.time + enemySpawnRate;
			_isEnemyLeft = !_isEnemyLeft;
		}
	}

//	void OnCollisionEnter(Collision other) {
//		Debug.Log ("In collision");
//		if (other.gameObject.CompareTag ("Powerup1")) {
//			Debug.Log ("In collision");
//			_score += 25;
//			scoreText.text = string.Format ("{0:000000}", _score);
//		}
//	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Powerup1")) {
			_score += 25;
			//other.gameObject.SetActive (false);
			scoreText.text = string.Format ("{0:000000}", _score);
		} else if (other.gameObject.CompareTag ("BoltEnemy")) {
			this.gameObject.SetActive (false);
		}
	}
}
