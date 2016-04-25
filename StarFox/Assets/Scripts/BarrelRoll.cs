using UnityEngine;
using System.Collections;

public class BarrelRoll : MonoBehaviour {

	public float speed;
	public GameObject ship;

	void FixedUpdate () {
		if (Input.GetKey (KeyCode.X) && (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow))) {
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
