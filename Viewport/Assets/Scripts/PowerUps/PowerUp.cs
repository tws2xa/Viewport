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
		duration = 10;
		timeLeft = this.duration;
		foreach (string p in PowerUpManagementScript.getPowerUpList(1)) {
			Debug.Log(p);
			if (Type.GetType(p).Equals(this.GetType())) {
				powerUpID = index;
				break;
			}
			index++;
		}
	}
	
	// Update is called once per frame
	abstract public void FixedUpdate ();

	abstract public void ModifyObject ();

	abstract public void DemodifyObject ();

	public void Timer() {
		//Subtract duration from the timer.
		timeLeft -= Time.deltaTime;
		if (timeLeft < 0.05) {
			PlayerPowerUpController manageScript = gameObject.GetComponent<PlayerPowerUpController> ();
			this.DemodifyObject ();
			manageScript.DeactivatePowerUp (this);
			Destroy(this);
		}
	}

	public override string ToString () {
		return "ID: " + powerUpID.ToString () + " DUR: " + duration.ToString () + " " + this.GetType ().ToString ();
	}

	public int getPowerUpID() {
		return powerUpID;
	}

	public override bool Equals (object o)
	{
		if (!(o is PowerUp)) {
			return false;
		}
		return (powerUpID == ((PowerUp)o).getPowerUpID ());
	}

	public static bool operator ==(PowerUp p1, PowerUp p2)
	{
		Debug.Log ("Entered ==");
		if (!(p1 is PowerUp)) {
			return false;
		} else {
			return p1.Equals (p2);
		}
	}

	public static bool operator != (PowerUp p1, PowerUp p2)
	{
		Debug.Log ("Entered !=");
		if ((p1 is PowerUp && p2 is PowerUp)) {
			return !(p1.Equals (p2));
		} else if (!(p1 is PowerUp) && p2 is PowerUp) {
			return true;
		} else if (p1 is PowerUp && !(p2 is PowerUp)) {
			return true;
		} else {
			return false;
		}
	}
}
