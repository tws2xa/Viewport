using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

	public float deathTimer;
	float origDeathTimer;

	// Use this for initialization
	void Start () {
		origDeathTimer = deathTimer;
	}
	
	// Update is called once per frame
	void Update () {
		deathTimer -= Time.deltaTime;
		if (gameObject.GetComponent<ParticleSystem> () != null) {
			if (deathTimer <= (origDeathTimer - 0.4f)) {
				gameObject.GetComponent<ParticleSystem>().enableEmission = false;
			}
			if (deathTimer <= 0.0f) {
				Destroy(this.gameObject);
			}
		}
		else if (deathTimer <= 0.0f) {
			Destroy(this.gameObject);
		}
	}
}
