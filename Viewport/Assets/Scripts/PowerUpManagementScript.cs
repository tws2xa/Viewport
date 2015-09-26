using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class PowerUpManagementScript : MonoBehaviour {

	// THIS SCRIPT GOES ONTO BOTH PICKUPS AND OBJECTS THAT CAN PICK UP.

	//private SoundManagementScript soundManagerScript;

	// ENUMERATION of all the powerUps. Index starts at 0.
	enum powerUpList {TESTPowerUp};

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

	// A constant that declares the maximum amount of powerUps a Player can hold.
	public const int MAX_AMT_POW = 1;

	// PLAYERS have a powerUpID of -1.
	// RANDOM PICKUPS have a powerUpID of -2.
	// ALL OTHER PICKUPS have a powerUpID corresponding to their powerUp.
	// THE CORRECT powerUpID is the index of your desired powerUp in the powerUpList ENUMERATION.
	public int powerUpID;

	// Use this for initialization
	void Start () {
		if (gameObject.CompareTag ("Player")) {
			maxAmtPow = MAX_AMT_POW;
		} else {
			maxAmtPow = 0;
		}
		curAmtPow = 0;
		activePowers = new List<PowerUp> ();
		//loadSoundManager ();
	}

	// Update is called once per frame
	void Update () {
	}

	// Do we need this if we want to play a sound on pickup of a powerup?
	// Loads in the sound manager object.
//	public void loadSoundManager() {
//		GameObject[] soundManagerObjects = GameObject.FindGameObjectsWithTag ("SoundManager");
//		if (soundManagerObjects.Length == 1) {
//			soundManagerScript = soundManagerObjects [0].GetComponent<SoundManagementScript> ();
//		} else {
//			Debug.LogError ("Incorrect number of sound managers. Must have exactly one.");
//		}
//	}

	void CreatePowerupIndicator () {
		// Alert the player that they have a powerup somehow.
		// Not sure if we want to display this on UI or not.
	}

	// Called when the object collides with another object
	// This method should only work if this object is a pickup, and the other object is a player.
	void OnCollisionEnter (Collision collision) {
		if (ObjectCanGrabPower(collision.gameObject) && !ObjectCanGrabPower(this.gameObject)) {
			// The other object is guarenteed to have a TransferControlScript because
			// of one of the checks in ObjectCanGrabCamera
			PowerUpManagementScript otherControlScript = collision.gameObject.GetComponent<PowerUpManagementScript>();
			if (powerUpID == -2) {
				Array values = Enum.GetValues (typeof(powerUpList));
				int randNum = UnityEngine.Random.Range(0, values.Length);
				PowerUp chosenPowerUp = getPowerUp(randNum);
				otherControlScript.ActivatePowerUp(chosenPowerUp);
			} else {
				otherControlScript.ActivatePowerUp(getPowerUp (powerUpID));
			}
			gameObject.SetActive(false);
			// Increment bkg music

//			if(soundManagerScript != null) {
//				soundManagerScript.updateBkgMusic();
//			}
		}
	}

	PowerUp getPowerUp (int chosenPowerUpID) {
		PowerUp chosenPowerUp = null;
		switch (chosenPowerUpID) {
		case (int)powerUpList.TESTPowerUp: 
			chosenPowerUp = new TESTPowerUp(10);
			break;
		}
		return chosenPowerUp;
	}

	public static Array getPowerUpList () {
		return Enum.GetValues(typeof(powerUpList));
	}

	/// <summary>
	/// Checks if the given object can grab a power up.
	/// </summary>
	/// <returns><c>true</c> if the powerup can be taken be taken, <c>false</c> otherwise.</returns>
	/// <param name="obj">The object with which we have collided.</param>
	bool ObjectCanGrabPower (GameObject obj) {
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
		if (!activePowers.Contains (power) && activePowers.Count < maxAmtPow) {
			activePowers.Add (power);
			gameObject.AddComponent (power.GetType ());
		} else {
			Debug.Log("Tried to add a power that the object already has, or the object has reached it's maximum Power Limit." +
				"Power is shown here: " + power.ToString () + " and current/maximum Powers are listed here: MAX: " + maxAmtPow.ToString () + 
			          "CUR: " + curAmtPow.ToString ());
		}
	}
	
	public void DeactivatePowerUp (PowerUp power) {
		if (power == null) {
			foreach (PowerUp p in activePowers) {
				Destroy (gameObject.GetComponent (p.GetType ()));
			}
			activePowers.Clear ();
		} else if (activePowers.Contains (power)) {
			Destroy (gameObject.GetComponent (power.GetType ()));
			activePowers.Remove (power);
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

}
