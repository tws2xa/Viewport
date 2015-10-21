using UnityEngine;
using System.Collections;

public class PlayerInteraction : MonoBehaviour {

	// This Script goes on players.

	Rigidbody rb;
	PlayerControls pc;

	public float bouncyForce = 100.0f;
	Vector3 bounceVector;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
		pc = gameObject.GetComponent<PlayerControls> ();
		bounceVector = new Vector3 (0, bouncyForce, 0);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision coll) {
		switch (coll.gameObject.tag) {
		case "DestroyPlayerOnImpact":
			this.gameObject.GetComponent<PlayerPowerUpController> ().DeactivatePowerUp (null);
			DeathController.KillPlayer (this.gameObject);
			break;
		case "Bouncy":
			bouncyForce = rb.mass * 275.0F;
			bounceVector = new Vector3 (0, bouncyForce, 0);
			rb.AddForce (bounceVector);
			break;
		case "Icey":
			pc.OnIce ();
			break;
		}
	}

	void OnCollisionExit(Collision coll) {
		if (coll.gameObject.CompareTag("Icey")){
			pc.OffIce();
		}
	}
}
