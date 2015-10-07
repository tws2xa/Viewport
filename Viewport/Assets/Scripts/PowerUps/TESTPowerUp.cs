using UnityEngine;
using System.Collections;

public class TESTPowerUp : PowerUp {

	// This powerUp should slow the Player to 1/4 original acceleration. Max speed should still be the same.

	// Make sure, when you create a powerUp, to add it to the ENUMERATION in PowerUpManagementScript.

	Rigidbody orb;

	float origDrag;

	public TESTPowerUp (int duration) : base (duration) {
	}

	public void Start () {
		duration = 10;
		timeLeft = this.duration;
		orb = gameObject.GetComponent<Rigidbody> ();
		origDrag = orb.drag;
		ModifyObject ();
	}
	
	// Update is called once per frame
	public override void FixedUpdate () {
		Timer ();
	}

	public override void ModifyObject () {
		orb.drag = 2;
	}

	public override void DemodifyObject () {
		orb.drag = origDrag;
	}
}
