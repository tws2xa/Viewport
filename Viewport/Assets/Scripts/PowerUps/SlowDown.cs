using UnityEngine;
using System.Collections;

public class SlowDown : PowerUp {

	// This powerUp should slow the Player to 1/4 original acceleration. Max speed should still be the same.

	// Make sure, when you create a powerUp, to add it to the ENUMERATION in PowerUpManagementScript.

	Rigidbody orb;

	float dragDelta;

	public SlowDown (int duration) : base (duration) {
	}

	public void Start () {
		powerUpID = 0;
		duration = 10;
		timeLeft = this.duration;
		orb = gameObject.GetComponent<Rigidbody> ();
		dragDelta = 1;
		ModifyObject ();
	}
	
	// Update is called once per frame
	public override void FixedUpdate () {
		Timer ();
	}

	public override void ModifyObject () {
		orb.drag += dragDelta;
	}

	public override void DemodifyObject () {
		orb.drag -= dragDelta;
	}
}
