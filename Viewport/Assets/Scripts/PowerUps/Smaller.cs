using UnityEngine;
using System.Collections;

public class Smaller : PowerUp {
	
	// This powerUp should slow the Player to 1/4 original acceleration. Max speed should still be the same.
	
	// Make sure, when you create a powerUp, to add it to the ENUMERATION in PowerUpManagementScript.
	
	Rigidbody orb;
	Vector3 deltaScale;
	Vector3 origScale;
	float timePassed;
	
	public Smaller (int duration) : base (duration) {
	}
	
	public void Start () {
		duration = 10;
		timeLeft = this.duration;
		orb = gameObject.GetComponent<Rigidbody> ();
		deltaScale = new Vector3(1,1,1);
		origScale = transform.localScale;
		ModifyObject ();
	}
	
	// Update is called once per frame
	public override void FixedUpdate () {
		Timer ();
		scaleTimer ();
	}
	
	public override void ModifyObject () {
		print ("PENIS!");
	}
	
	public override void DemodifyObject () {
		transform.localScale = new Vector3 (1, 1, 1);
		print ("SHRINKING BACK!");
	}
	
	public void scaleTimer(){
		timePassed += Time.deltaTime;
		print (timePassed);
		print (timeLeft);
		if (timePassed <= 5.0F && timePassed >= 0.0F) {
			transform.localScale = Vector3.Lerp (transform.localScale, origScale + deltaScale, 0.1F);
		} else if (timePassed >= 5.0F && timePassed <= 10.0F) {
			transform.localScale = Vector3.Lerp(transform.localScale, origScale, 0.1F);
		}
	}
}
