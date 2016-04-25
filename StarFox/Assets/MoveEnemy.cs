using UnityEngine;
using System.Collections;

public class MoveEnemy : MonoBehaviour {

	public float speed;
	public float movementTime = 1.0f;

	private Vector3 _endPosition;
	private float _elapsedTime = 0.0f;
	private bool _moveNormally = false;
	private GameObject _playerObject;

	// Use this for initialization
	void Start () {
		_elapsedTime += Time.deltaTime * speed;
		_playerObject = GameObject.FindGameObjectWithTag ("Player");
		_endPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z - 800.0f);
		transform.Rotate (new Vector3 (this.transform.rotation.x, this.transform.rotation.y - 180.0f, this.transform.rotation.z));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!_moveNormally) {
			_elapsedTime += Time.deltaTime * speed;
			float currentProgress = _elapsedTime / movementTime;
			transform.position = Vector3.Lerp (this.transform.position, _endPosition, currentProgress);
			if (this.transform.position.z <= _endPosition.z) {
				_moveNormally = true;
				//transform.Rotate (new Vector3 (this.transform.rotation.x, this.transform.rotation.y - 180.0f, this.transform.rotation.z));
			}
		} else {
			Rigidbody rigidBody = gameObject.GetComponent<Rigidbody> ();
			rigidBody.position = new Vector3 (
				(rigidBody.position.x < _playerObject.transform.position.x - 20.0f ? 
					rigidBody.position.x + 1.0f : (
						rigidBody.position.x > _playerObject.transform.position.x + 20.0f ? rigidBody.position.x - 1.0f : rigidBody.position.x
					)
				), 
				(rigidBody.position.y < _playerObject.transform.position.y - 20.0f ? 
					rigidBody.position.y + 1.0f : (
						rigidBody.position.y > _playerObject.transform.position.y + 20.0f ? rigidBody.position.y - 1.0f : rigidBody.position.y)
				), 
				rigidBody.position.z + -5.0f
			);
//			if (transform.position.x < _playerObject.transform.position.x - 20.0f) transform.Translate (Vector3.right);
//			else if (transform.position.x > _playerObject.transform.position.x + 20.0f) transform.Translate (Vector3.left);
//			if (transform.position.y < _playerObject.transform.position.y - 20.0f) transform.Translate (Vector3.up);
//			else if (transform.position.y > _playerObject.transform.position.y + 20.0f) transform.Translate (Vector3.down);
			//transform.LookAt (_playerObject.transform);
			//transform.rotation = Quaternion.Euler (new Vector3(_playerObject.transform.rotation.x, transform.rotation.y, _playerObject.transform.rotation.z));
			transform.rotation = Quaternion.LookRotation(transform.position - _playerObject.transform.position);
		}
	}
}
