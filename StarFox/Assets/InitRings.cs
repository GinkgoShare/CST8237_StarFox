using UnityEngine;
using System.Collections;

public class InitRings : MonoBehaviour {

	public GameObject ring;
	// Use this for initialization
	void Start () {
		GameObject parent = this.gameObject;
		for (int i = 0; i < 50; i++) {
			GameObject newRing = Instantiate (
				ring, 
				new Vector3 (
					Random.Range((parent.transform.position.x - 200.0f), (parent.transform.position.x + 200.0f)),
					//Mathf.Clamp (parent.transform.position.x, (parent.transform.position.x - 200.0f), (parent.transform.position.x + 200.0f)),
					Random.Range((parent.transform.position.y - 200.0f), (parent.transform.position.y + 200.0f)),
					//Mathf.Clamp (parent.transform.position.y, (parent.transform.position.y - 200.0f), (parent.transform.position.y + 200.0f)),
					parent.transform.position.z - 400.0f
				),
				ring.transform.rotation
			) as GameObject;
			newRing.transform.parent = parent.transform;
			parent = newRing;
		}
	}
}
