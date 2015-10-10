using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class PowerUpManagementScript : MonoBehaviour {

	// THIS SCRIPT GOES ONTO BOTH PICKUPS AND OBJECTS THAT CAN PICK UP.

	//private SoundManagementScript soundManagerScript;

	// ENUMERATION of all the powerUps. Index starts at -1.
	public enum PowerUpList {RANDOM = -1, TESTPowerUp, SpeedUp};

	// A constant that declares the maximum amount of powerUps a Player can hold.
	public const int MAX_AMT_POW = 1;

	// A constant that declares the respawn timer IN SECONDS.
	public const float RESPAWN_TIMER = 10.0f;

	// PLAYERS have a powerUpID of -1.
	// RANDOM PICKUPS have a powerUpID of -2.
	// ALL OTHER PICKUPS have a powerUpID corresponding to their powerUp.
	// THE CORRECT powerUpID is the index of your desired powerUp in the powerUpList ENUMERATION.
	public PowerUpList powerUpID;

	// Use this for initialization
	void Start () {
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

	public void RespawnThis() {
		if (!gameObject.activeInHierarchy) {
			gameObject.SetActive(true);
		}
	}

	public static Array getPowerUpList (int choice) {
		if (choice == 1) {
			return Enum.GetNames (typeof(PowerUpList));
		} else {
			return Enum.GetValues (typeof(PowerUpList));
		}
	}
	
	public PowerUp getPowerUp (PowerUpList chosenPowerUpID) {
		PowerUp chosenPowerUp = null;
		switch (chosenPowerUpID) {
		case PowerUpList.TESTPowerUp: 
			chosenPowerUp = new TESTPowerUp(10);
			break;
        case PowerUpList.SpeedUp:
            chosenPowerUp = new SpeedUp(10);
            break;
		}
		return chosenPowerUp;
	}

    public PowerUp getThisPowerUp () {
		if (powerUpID == PowerUpList.RANDOM) {
			Array values = getPowerUpList (0);
			int randNum = UnityEngine.Random.Range (0, values.Length - 1);
			return getPowerUp ((PowerUpList) randNum);
		} else {
			return getPowerUp ((PowerUpList) powerUpID);
		}
	}

	public PowerUpList getPowerUpID () {
		return powerUpID;
	}
}
