using UnityEngine;
using System.Collections;

public class Smaller : PowerUp {
	
	// This powerUp should shrink the Player to 0.5 units smaller.
	
	// Make sure, when you create a powerUp, to add it to the ENUMERATION in PowerUpManagementScript.
	
	Vector3 deltaScale;
	Vector3 origScale;
	float timePassed;
	float interpolant;
	float changeTime;
	
	public Smaller (int duration) : base (duration) {
	}
	
	public new void Start () {
		changeTime = 0.3f;
		powerUpID = 8;
		duration = 10;
		timeLeft = this.duration;
		deltaScale = new Vector3(0.3F,0.3F,0.3F);
		origScale = transform.localScale;
		ModifyObject ();
	}
	
	// Update is called once per frame
	public override void FixedUpdate () {
		Timer ();
		scaleTimer ();
	}
	
	public override void ModifyObject () {
	}
	
	public override void DemodifyObject () {
	}
	
	public void scaleTimer(){
		timePassed += Time.deltaTime;
		if (timePassed <= changeTime && timePassed >= 0.0F) {
			interpolant = timePassed/changeTime;
			if (interpolant >= 1.0F){
				interpolant = 1.0F;
			}
			transform.localScale = Vector3.Lerp (origScale, origScale - deltaScale, interpolant);
		} else if (timePassed >= (duration - changeTime) && timePassed <= duration) {
			interpolant = (timePassed - (duration - changeTime))/changeTime;
			if (interpolant >= 1.0F){
				interpolant = 1.0F;
			}
			transform.localScale = Vector3.Lerp(origScale - deltaScale, origScale, interpolant);
		}
	}
}
