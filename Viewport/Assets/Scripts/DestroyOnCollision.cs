using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyOnCollision : MonoBehaviour {

	/// <summary>
	/// This list contains all destroy trigger tags.
	/// If the object collides with anything that has one
	/// of these tags, it will be destroyed. Otherwise, it
	/// will not be destroyed.
	/// </summary>
	public List<string> destroyTags = new List<string>();

	// Called when the object collides with something.
	void OnCollisionEnter(Collision collision) {
		if (destroyTags.Contains(collision.gameObject.tag)) {
			Destroy (this.gameObject);
		}
	}

}
