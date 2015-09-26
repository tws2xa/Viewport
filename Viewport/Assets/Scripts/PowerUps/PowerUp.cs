using UnityEngine;
using System;
using System.Collections;

public abstract class PowerUp : MonoBehaviour {

	// ALL POWERUPS INHERIT FROM THIS CLASS

	// powerUpID is found by looking at the powerUpList enum in PowerUpManagementScript.
	protected int powerUpID;
	public int duration; // In seconds
	protected float timeLeft;

	protected PowerUp (int duration) {
		this.duration = duration;
		timeLeft = this.duration;
	}

	public void Start () {
		int index = 0;
		foreach (PowerUp p in PowerUpManagementScript.getPowerUpList()) {
			if (p.GetType().Equals(this.GetType())) {
				powerUpID = index;
				break;
			}
			index++;
		}
	}
	
	// Update is called once per frame
	abstract public void Update ();

	abstract public void ModifyObject ();

	abstract public void DemodifyObject ();

	public void Timer() {
		//Subtract duration from the timer.
		timeLeft -= 0.01f;
		if (timeLeft < 0.05) {
			PowerUpManagementScript manageScript = gameObject.GetComponent<PowerUpManagementScript> ();
			this.DemodifyObject ();
			manageScript.DeactivatePowerUp (this);
		}
	}

	public override string ToString () {
		return "ID: " + powerUpID.ToString () + "DUR: " + duration.ToString () + this.GetType ().ToString ();
	}
}
