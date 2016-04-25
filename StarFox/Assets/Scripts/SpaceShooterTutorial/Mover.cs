using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
	public float speed;

	void Start () 
	{
		transform.rotation = Quaternion.Euler (new Vector3(this.transform.rotation.x + 90.0f, this.transform.rotation.y, this.transform.rotation.z));
		GetComponent<Rigidbody> ().velocity = transform.up * speed;
	}
}
