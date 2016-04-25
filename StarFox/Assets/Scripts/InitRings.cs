using UnityEngine;
using System.Collections.Generic;

public class InitRings : MonoBehaviour {

	public GameObject ring;

	private List<GameObject> rings = new List<GameObject> (50);
	private int _ringCount;
	private bool _isLayoutSet = false;
	// Use this for initialization
	void Start () {
		GameObject parent = this.gameObject;
		for (_ringCount = 0; _ringCount < 15; _ringCount++) {
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
			//newRing.transform.parent = parent.transform;
			rings.Add(newRing);
			parent = newRing;
		}
		_isLayoutSet = true;
	}

	public void SetRingLayout(GameObject player) {
//		_ringCount += 10;
		_isLayoutSet = false;
		GameObject parent = player;
		for (int i = 0; i < _ringCount; i++) {
//			if (i < rings.Count) Destroy (rings [i]);
//			rings.Clear ();
			Vector3 newPosition = new Vector3 (
				                      Random.Range ((parent.transform.position.x - 200.0f), (parent.transform.position.x + 200.0f)),
				                      Random.Range ((parent.transform.position.y - 200.0f), (parent.transform.position.y + 200.0f)),
				                      parent.transform.position.z - 400.0f
			                      );
			rings [i].transform.position = newPosition;
//			GameObject newRing = Instantiate (ring, newPosition, ring.transform.rotation) as GameObject;
//			rings.Add(newRing);
			parent = rings [i];
		}
		_isLayoutSet = true;
	}

	public bool HasPlayerPassedLayout(float z) {
		return _isLayoutSet && z < rings [rings.Count - 1].transform.position.z;
	}
}
