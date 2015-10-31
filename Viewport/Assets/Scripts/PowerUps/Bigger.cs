﻿using UnityEngine;
using System.Collections;

public class Bigger : PowerUp {
	
	// This powerUp should slow the Player to 1/4 original acceleration. Max speed should still be the same.
	
	// Make sure, when you create a powerUp, to add it to the ENUMERATION in PowerUpManagementScript.
	
	Vector3 deltaScale;
	Vector3 origScale;
	float timePassed;
	float interpolant;
	
	public Bigger (int duration) : base (duration) {
	}
	
	public override void Start () {
		duration = 10;
		timeLeft = this.duration;
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
	}
	
	public override void DemodifyObject () {
		transform.localScale = new Vector3 (1, 1, 1);
	}
	
	public void scaleTimer(){
		timePassed += Time.deltaTime;
		if (timePassed <= duration/2 && timePassed >= 0.0F) {
			interpolant = timePassed/duration/2;
			if (interpolant >= 1.0F){
				interpolant = 1.0F;
			}
			transform.localScale = Vector3.Lerp (transform.localScale, origScale + deltaScale, interpolant);
		} else if (timePassed >= duration/2 && timePassed <= duration) {
			transform.localScale = Vector3.Lerp(transform.localScale, origScale, 0.1F);
			interpolant = timePassed/duration;
			if (interpolant >= 1.0F){
				interpolant = 1.0F;
			}
		}
	}
}