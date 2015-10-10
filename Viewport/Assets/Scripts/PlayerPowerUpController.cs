using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerPowerUpController : MonoBehaviour {

	// An updating list of all active powerUps on the object.
	// Should only have elements if gameObject is a Player.
	List<PowerUp> activePowers;

	// The maximum amount of powerUps an object can hold.
	// For players, this int should be MAX_AMT_POW, shown below.
	// For other objects (pickups), this int should be 0.
	int maxAmtPow;
	// The current amount of powerUps an object is holding.
	// Should never be higher than maxAmtPow.
	int curAmtPow;

	bool respawningPowerUp = false;
	float timeLeft;
	List<PowerUpManagementScript> activeManageScripts;

	// Use this for initialization
	void Start () {
		maxAmtPow = PowerUpManagementScript.MAX_AMT_POW;
		activePowers = new List<PowerUp> ();
		activeManageScripts = new List<PowerUpManagementScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (respawningPowerUp) {
			RespawnTimer();
		}
	}

	void OnTriggerEnter (Collider coll) {
		if (ObjectIsPowerUp(coll.gameObject)) {
			// The other object is guarenteed to have a TransferControlScript because
			// of one of the checks in ObjectCanGrabCamera
			PowerUpManagementScript otherControlScript = coll.gameObject.GetComponent<PowerUpManagementScript>();
			PowerUp chosenPowerUp = otherControlScript.getThisPowerUp();
			ActivatePowerUp(chosenPowerUp);
			InitiateRespawnTimer(otherControlScript);
			otherControlScript.gameObject.SetActive(false);
			// Increment bkg music
			
			//			if(soundManagerScript != null) {
			//				soundManagerScript.updateBkgMusic();
			//			}
		}


	}

	public void InitiateRespawnTimer (PowerUpManagementScript script) {
		respawningPowerUp = true;
		timeLeft = PowerUpManagementScript.RESPAWN_TIMER;
		activeManageScripts.Add (script);
	}

	void RespawnTimer () {
		//Subtract duration from the timer.
		timeLeft -= Time.deltaTime;
		if (timeLeft < 0.05) {
			foreach (PowerUpManagementScript ms in activeManageScripts) {
				ms.RespawnThis();
			}
		}
	}

	/// <summary>
	/// Checks if the given object can grab a power up.
	/// </summary>
	/// <returns><c>true</c> if the powerup can be taken be taken, <c>false</c> otherwise.</returns>
	/// <param name="obj">The object with which we have collided.</param>
	bool ObjectIsPowerUp (GameObject obj) {
		// Check if the other object allows picking up of power ups.
		if (obj.GetComponent<PowerUpManagementScript> () == null) {
			return false;
		} else {
			PowerUpManagementScript manageScript = obj.GetComponent<PowerUpManagementScript> ();
			if (manageScript.powerUpID != 0) {
				return false;
			}
		}
		
		// Allow object to take powerup
		return true;
	}
	
	public void ActivatePowerUp (PowerUp power) {
		if (power == null) {
			Debug.Log ("Error: Power is Null (probably)");
			return;
		}
		if (!activePowers.Contains (power) && activePowers.Count < maxAmtPow) {
			activePowers.Add (power);
			power.Start ();
			gameObject.AddComponent (power.GetType ());
			Debug.Log("Adding: " + power.ToString ());
		} else {
			//Debug.Log("Tried to add a power that the object already has, or the object has reached it's maximum Power Limit." +
			//	"Power is shown here: " + power.ToString () + " and current/maximum Powers are listed here: MAX: " + maxAmtPow.ToString () + 
			//        "CUR: " + curAmtPow.ToString ());
		}
	}
	
	public void DeactivatePowerUp (PowerUp power) {
		if (!power) {
			foreach (PowerUp p in activePowers) {
				Destroy (gameObject.GetComponent (p.GetType ()));
			}
			activePowers.Clear ();
			Debug.Log ("Deactivated powerup is null");
		} else if (activePowers.Contains (power)) {
			Destroy (gameObject.GetComponent (power.GetType ()));
			activePowers.Remove (power);
			Debug.Log("Attempting to remove " + power.ToString ());
		} else {
			Debug.Log("Tried to remove a power that the object did not have. Power is toString'd here: " + power.ToString ());
		}
		//gameObject.GetComponent<pu.GetType()>
	}

	/// <summary>
	/// Checks whether or not a powerup is affecting the given object
	/// </summary>
	/// <returns><c>true</c> if is given object is affected by a powerup, <c>false</c> otherwise.</returns>
	/// <param name="obj">The object to check.</param>
	bool PowerupActive (GameObject obj) {
		if (activePowers.Count <= 0) {
			return false;
		}
		
		return true;
	}

	
	
	void CreatePowerupIndicator () {
		// Alert the player that they have a powerup somehow.
		// Not sure if we want to display this on UI or not.
	}
}
