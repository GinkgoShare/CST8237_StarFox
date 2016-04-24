using UnityEngine;
using System.Collections;

public class BarrelRoll : MonoBehaviour {

	public float speed;
	public GameObject ship;
	
	// Update is called once per frame
	void FixedUpdate () {

//		if (_inBarrelRole) {
//			_elapsedTime += Time.deltaTime * velocity;
//			float currentProgress = _elapsedTime / movementTime;
//			//transform.rotation = Quaternion.Lerp (this.transform.rotation, _endRotation, currentProgress);
//			transform.position = Vector3.Lerp (this.transform.position, _endPosition, currentProgress);
//		}

		if (Input.GetKey (KeyCode.X)) {
			if (ship.transform.parent == null) ship.transform.parent = this.transform;

			float moveVertical = Input.GetAxis ("Vertical");
			float moveHorizontal = -Input.GetAxis ("Horizontal");
			transform.position = new Vector3 (
				transform.position.x + (moveHorizontal * 10.0f), 
				transform.position.y + (moveVertical * -5.0f), 
				transform.position.z + -5.0f
			);
			transform.Rotate (new Vector3(0.0f, 0.0f, -moveHorizontal) * speed);
		} else {
			ship.transform.parent = null;
		}
	}
}
