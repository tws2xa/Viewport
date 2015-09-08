using UnityEngine;
using System.Collections;

public class DestroyOnPlayerCollision : MonoBehaviour {

	// Called when the object collides with something.
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Player") {
			Destroy (this.gameObject);
		}
	}

}
