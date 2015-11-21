using UnityEngine;
using System.Collections;

public class PlayerInteraction : MonoBehaviour {

	// This Script goes on players.

	Rigidbody rb;
	PlayerControls pc;

	float bouncyForce;
	Vector3 bounceVector;
	Material currBouncePadMat;
	bool bouncePadMatActive = false;

	float timePassed;
	float interpolant;

	float durationOfBP = 0.5f;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
		pc = gameObject.GetComponent<PlayerControls> ();
		bounceVector = new Vector3 (0, bouncyForce, 0);
	}
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;
		interpolant = timePassed/durationOfBP;
		if (interpolant >= 1.0f) {
			interpolant = 1.0f;
			bouncePadMatActive = false;
		}
		if (bouncePadMatActive) {
			currBouncePadMat.SetColor("_EmissionColor", Vector4.Lerp(new Color(0.4f, 0.4f, 0.4f, 1.0f), Color.black, interpolant));
		}
	}

	void OnCollisionEnter(Collision coll) {
		switch (coll.gameObject.tag) {
		case "DestroyPlayerOnImpact":
			this.gameObject.GetComponent<PlayerPowerUpController> ().DeactivatePowerUp (null);
			DeathController.KillPlayer (this.gameObject, DeathController.DeathCause.Lava);
			break;
		case "Bouncy":
			bouncyForce = rb.mass * 275.0F;
			bounceVector = new Vector3 (0, bouncyForce, 0);
			rb.AddForce (bounceVector);
			currBouncePadMat = coll.gameObject.GetComponent<MeshRenderer>().material;
			currBouncePadMat.EnableKeyword("_EMISSION");
			timePassed = 0.0f;
			bouncePadMatActive = true;
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
