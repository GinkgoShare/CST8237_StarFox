using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerManagement : MonoBehaviour {

	public float tiltX;
	public float tiltY;
	public float velocity;
	public float fireRate;
	public float enemySpawnRate;

	public Text scoreText;
	public Text levelText;

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
	private int _level = 1;
	private float _shotSpeed = 1000.0f;
	private bool _isEnemyLeft = false;
	private float _nextFire;
	private float _nextEnemySpawn;
	private bool _shouldSpawnEnemies = false;
	private GameObject _ringLayoutManager;
	private Queue<GameObject> _enemies = new Queue<GameObject>();

	void Start() {
		_ringLayoutManager = GameObject.FindGameObjectWithTag ("Layout");
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
		if (Input.GetKey(KeyCode.Space) && Time.time > _nextFire) {
			Rigidbody rigidBody = this.GetComponent<Rigidbody> ();
			_nextFire = Time.time + fireRate;
			GameObject shot = Instantiate (shotLeft, shotSpawnLeft.position, shotLeft.transform.rotation) as GameObject;
			shot.GetComponent<Rigidbody> ().velocity = -transform.forward * _shotSpeed;
			shot = Instantiate (shotRight, shotSpawnRight.position, shotRight.transform.rotation) as GameObject;
			shot.GetComponent<Rigidbody> ().velocity = -transform.forward * _shotSpeed;
		}
		if (Time.time > _nextEnemySpawn) {
			GameObject newEnemy;
			Vector3 spawnPos = (_isEnemyLeft ? enemySpawnLeft.position : enemySpawnRight.position);
			if (_enemies.Count > 0) {
				newEnemy = _enemies.Dequeue ();
				newEnemy.transform.position = spawnPos;
				//newEnemy.transform.rotation = Quaternion.identity;
				newEnemy.GetComponent<MoveEnemy>().SetEndPositionForLerp(spawnPos);
				newEnemy.SetActive (true);
			} else {
				Instantiate (enemy, spawnPos, Quaternion.identity);
			}
			_nextEnemySpawn = Time.time + enemySpawnRate;
			_isEnemyLeft = !_isEnemyLeft;
		}

		if (_ringLayoutManager.GetComponent<InitRings> ().HasPlayerPassedLayout (this.transform.position.z)) {
			//_shotSpeed += 25.0f;
			levelText.text = string.Format ("Level {0:00}", ++_level);
			_ringLayoutManager.GetComponent<InitRings> ().SetRingLayout (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Ring")) {
			_score += 25;
			//other.gameObject.SetActive (false);
			scoreText.text = string.Format ("{0:000000}", _score);
		} else if (other.gameObject.CompareTag ("BoltEnemy")) {
			this.gameObject.SetActive (false);
		}
	}

	public int GetLevel() {
		return _level;
	}

	public void PushEnemy(GameObject enemy) {
		_enemies.Enqueue (enemy);
	}
}
