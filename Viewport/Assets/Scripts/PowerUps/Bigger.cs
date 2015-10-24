using UnityEngine;
using System.Collections;

public class Bigger : PowerUp {
	
	// This powerUp should slow the Player to 1/4 original acceleration. Max speed should still be the same.
	
	// Make sure, when you create a powerUp, to add it to the ENUMERATION in PowerUpManagementScript.
	
	Rigidbody orb;
	bool scale;
	Vector3 deltaScale;
	
	public Bigger (int duration) : base (duration) {
	}
	
	public void Start () {
		duration = 10;
		timeLeft = this.duration;
		orb = gameObject.GetComponent<Rigidbody> ();
		deltaScale = new Vector3(1,1,1);
		ModifyObject ();
	}
	
	// Update is called once per frame
	public override void FixedUpdate () {
		Timer ();
		scaleTimer ();
	}
	
	public override void ModifyObject () {
		scale = true;
	}
	
	public override void DemodifyObject () {
		scale = false;
	}

	public void scaleTimer(){

		if (scale) {
			transform.localScale = Vector3.Lerp (transform.localScale, transform.localScale + deltaScale, 0.1F);
		} else {
			transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale - deltaScale, 0.1F);
		}
	}
}
