using UnityEngine;
using System.Collections.Generic;

public class endMenuScript : MonoBehaviour {

	
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

	public static void setMenu() {
		//get players
		//generate 2, 3 or 4 places
		//display lowest time first
		//buttons: replay level, quit to menu, new level

		int toPlay1 = PlayerPrefs.GetInt ("player1ball");
		int toPlay2 = PlayerPrefs.GetInt ("player2ball");
		int toPlay3 = PlayerPrefs.GetInt ("player3ball");
		int toPlay4 = PlayerPrefs.GetInt ("player4ball");

		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		GameObject[] sortPlayers;

			if (PlayerPrefs.HasKey ("p1TimeActive")) {
				float player1Time = PlayerPrefs.GetFloat ("p1TimeActive");
			}
			if (PlayerPrefs.HasKey ("p2TimeActive")) {
				float player2Time = PlayerPrefs.GetFloat ("p2TimeActive");
			}
			if (PlayerPrefs.HasKey ("p3TimeActive")) {
				float player3Time = PlayerPrefs.GetFloat ("p3TimeActive");
			}
			if (PlayerPrefs.HasKey ("p4TimeActive")) {
				float player4Time = PlayerPrefs.GetFloat ("p4TimeActive");
			}

		//sort by times
		if (players.Length==4) {
			if (PlayerPrefs.HasKey ("p1TimeActive")) {
				int tmp;
				if (player1Time > player2Time) { tmp = player1Time; player1Time = player2Time; player2Time = tmp; }
				if (player3Time > player4Time) { tmp = player3Time; player3Time = player4Time; player4Time = tmp; }
				if (player1Time > player3Time) { tmp = player1Time; player1Time = player3Time; player3Time = tmp; }
				if (player2Time > player4Time) { tmp = player2Time; player2Time = player4Time; player4Time = tmp; }
				if (player2Time > player3Time) { tmp = player2Time; player2Time = player3Time; player3Time = tmp; }

			}

		if(players.Length == 3) {
			}
		if(players.Length == 2){
				if(player1Time > player2Time) {
						maxTime = player1Time;
					}
					else {
						maxTime = player2Time;
					}

				}
		Text player1T = GetComponent<p1time>();
		Text player2T = GetComponent<p2time>();
		Text player3T = GetComponent<p3time>();
		Text player4T = GetComponent<p4time>();
			player1T.text = player1Time;

	
		}