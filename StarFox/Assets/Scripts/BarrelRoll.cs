using UnityEngine;
using System.Collections;

public class BarrelRoll : MonoBehaviour {

	public float speed;
	public GameObject ship;

	// barrel roll
	public float barrelRollDuration = 1.0f;
	public bool inBarrelRoll = false;
	private float movementAxis;
	
	// Update is called once per frame
	void FixedUpdate () {

//		if (_inBarrelRole) {
//			_elapsedTime += Time.deltaTime * velocity;
//			float currentProgress = _elapsedTime / movementTime;
//			//transform.rotation = Quaternion.Lerp (this.transform.rotation, _endRotation, currentProgress);
//			transform.position = Vector3.Lerp (this.transform.position, _endPosition, currentProgress);
//		}

		if (Input.GetKey (KeyCode.B)) {
//			_inBarrelRole = true;
			if (ship.transform.parent == null) ship.transform.parent = this.transform;
			if (Input.GetKey (KeyCode.LeftArrow)) {
				transform.Rotate (Vector3.back * speed);
				transform.Translate (Vector3.right * speed, Space.World);
//				_endPosition = new Vector3 (this.transform.position.x - 50.0f, this.transform.position.y, this.transform.position.z);
//				_endRotation = Quaternion.Euler(new Vector3 (this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z));
			} else if (Input.GetKey (KeyCode.RightArrow)) {
				transform.Rotate (Vector3.forward * speed);
				transform.Translate (Vector3.left * speed, Space.World);
//				_endPosition = new Vector3 (this.transform.position.x + 50.0f, this.transform.position.y, this.transform.position.z);
//				_endRotation = Quaternion.Euler(new Vector3 (this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z));
			}
//			if(!inBarrelRoll)
//			{
//				if (Input.GetKeyUp(KeyCode.F ))
//				{
//					StartCoroutine("barrelRoll");
//				}
//			}
		} else {
			ship.transform.parent = null;
		}
	}

	IEnumerator barrelRoll()
	{
		inBarrelRoll = true;
		float t = 0.0f;
		Vector3 initialRotation = transform.localRotation.eulerAngles;
		Vector3 goalRotation = initialRotation;
		goalRotation.z += 180.0f;
		Vector3 currentRotation = initialRotation;

		while(t < barrelRollDuration/2.0f)
		{
			currentRotation.z = Mathf.Lerp(initialRotation.z,goalRotation.z,t/(barrelRollDuration/2.0f));
			transform.localRotation = Quaternion.Euler(currentRotation);
			t += Time.deltaTime;
			yield return null;
		}
		t = 0;

		initialRotation = transform.localRotation.eulerAngles;
		goalRotation = initialRotation;
		goalRotation.z += 180.0f ;

		while(t < barrelRollDuration/2.0f)
		{
			currentRotation.z = Mathf.Lerp(initialRotation.z,goalRotation.z,t/(barrelRollDuration/2.0f));
			transform.localRotation = Quaternion.Euler(currentRotation);
			t += Time.deltaTime;
			yield return null;
		}

		inBarrelRoll = false;
		ResetRotation();

	}

	void ResetRotation()
	{
		transform.localRotation = Quaternion.identity;
	}
}
