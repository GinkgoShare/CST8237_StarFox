using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Enemy" || other.tag == "Ring" || other.tag == "Player")
		{
			return;
		}

		if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);
		}

		Destroy (other.gameObject);
		gameObject.SetActive (false);
		gameObject.GetComponent<MoveEnemy> ().SetMoveNormally (false);
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerManagement> ().PushEnemy (gameObject);
		//Destroy (gameObject);
	}
}