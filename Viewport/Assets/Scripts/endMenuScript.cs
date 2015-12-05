using System.Linq;
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
	}

	public void toMenu() {
		Application.LoadLevel ("menu");
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
		List<KeyValuePair<int, float>> times = new List<KeyValuePair<int,float>>();
		Dictionary<int, float> dictionary =
			new Dictionary<int, float>();


		//p1ball getInt 
		//textureChange.cs


		if (PlayerPrefs.HasKey ("p1TimeActive")) {
			float player1Time = PlayerPrefs.GetFloat ("p1TimeActive");

			print ("p1" + player1Time);
			dictionary.Add (1, player1Time);
		} else {
			dictionary.Add (1, float.MaxValue - 5.0f);
		}
		if (PlayerPrefs.HasKey ("p2TimeActive")) {
			float player2Time = PlayerPrefs.GetFloat ("p2TimeActive");
			dictionary.Add(2, player2Time);
			print ("p2" + player2Time);
		}else {
			dictionary.Add (2, float.MaxValue - 5.0f);
		}
		if (PlayerPrefs.HasKey ("p3TimeActive")) {
			float player3Time = PlayerPrefs.GetFloat ("p3TimeActive");
			print ("p3" + player3Time);

			dictionary.Add(3, player3Time);

		}else {
			dictionary.Add (3, float.MaxValue - 5.0f);
		}
		if (PlayerPrefs.HasKey ("p4TimeActive")) {
			float player4Time = PlayerPrefs.GetFloat ("p4TimeActive");
			dictionary.Add(4, player4Time);
			print ("p4" + player4Time);
		}else {
			dictionary.Add (4, float.MaxValue - 5.0f);
		}

	


	

		times = dictionary.ToList ();
		times.Sort ((x, y) => y.Value.CompareTo (x.Value));

 
	
	

		for (int i = 0; i < times.Count; i++) {
			if(i >= 1) {
				TimeSpan t = TimeSpan.FromSeconds(times[i].Value);
				Debug.Log ("Player " + times[i].Key + ": " + string.Format ("{0}:{1:00}", 
				                                                            (int)t.TotalMinutes,
				                                                            t.Seconds));
				this.playerTimeText[i].text = string.Format ("{0}:{1:00}", 
			                                             (int)t.TotalMinutes,
			                                             t.Seconds);
			} else {
					Debug.Log ("Player " + times[i].Key + ": Wins!");
				}
			int playerNumber = times[i].Key;
			string playerNumberStr = "Player " + playerNumber + ": ";
			this.playerPlaceText[i].text = playerNumberStr;
			}

			
	
	}	
}