
using UnityEngine.UI;

using UnityEngine;
using System;
using System.Collections.Generic;

public class endMenuScript : MonoBehaviour {
	//time texts: p1t is the winner time -> p4t is the loser time

	
	public List<Text> playerTimeText = new List<Text>();

	public List<Text> playerPlaceText = new List<Text>();



	void Start () {
		GameObject endMenu = GameObject.Find ("EndMenu");
		RectTransform endMenuTransform = endMenu.GetComponent<RectTransform> ();
		endMenuTransform.anchoredPosition = Vector2.zero;


		setTimes ();

	}
	
	// Update is called once per frame
	void Update () {
		setTimes ();
	}

	public void setTimes() {
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
		List<float> times = new List<float>();
		Dictionary<float, int> dictionary =
			new Dictionary<float, int>();

		dictionary.Add (30f, 1);
		dictionary.Add (240f, 2);
		dictionary.Add (50f, 3);
		dictionary.Add (230f, 4);

		/*
		if (PlayerPrefs.HasKey ("p1TimeActive")) {
			float player1Time = PlayerPrefs.GetFloat ("p1TimeActive");


			dictionary.Add(player1Time, 1);
		}
		if (PlayerPrefs.HasKey ("p2TimeActive")) {
			float player2Time = PlayerPrefs.GetFloat ("p2TimeActive");
		dictionary.Add(player2Time, 2);
		}
		if (PlayerPrefs.HasKey ("p3TimeActive")) {
			float player3Time = PlayerPrefs.GetFloat ("p3TimeActive");
			

		dictionary.Add(player3Time, 3);

		}
		if (PlayerPrefs.HasKey ("p4TimeActive")) {
			float player4Time = PlayerPrefs.GetFloat ("p4TimeActive");
		dictionary.Add(player4Time, 4);
		}
		*/
	


	foreach (KeyValuePair<float, int> pair in dictionary)
		    {
			times.Add(pair.Key);
		}


		//p1ball getInt 
		//textureChange.cs
 
		times.Sort ();
		times.Reverse ();

		for (int i = 0; i < times.Count; i++) {
			TimeSpan t = TimeSpan.FromSeconds(times[i]);
			if(i >= 1) {
				this.playerTimeText[i].text = string.Format ("{0}:{1:00}", 
			                                             (int)t.TotalMinutes,
			                                             t.Seconds);
			}
			int playerNumber = dictionary[times[i]];
			string playerNumberStr = "Player " + playerNumber + ": ";
			this.playerPlaceText[i].text = playerNumberStr;
			
			}

			
	
	}	
}