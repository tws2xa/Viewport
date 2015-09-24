using UnityEngine;
using System.Collections;

public class TESTPowerUp : PowerUp {

	public override void Start () {
		powerUpID = 1;
		duration = 10;
		timeLeft = 10.0f;
	}
	
	// Update is called once per frame
	public override void Update () {
		Timer ();
	}

	public override void ModifyObject() {
		Rigidbody orb = gameObject.GetComponent<Rigidbody> ();
		orb.drag = 2;
	}

	public override void Timer() {
		//Subtract duration from the timer.
		timeLeft -= 0.01f;
	}
}
