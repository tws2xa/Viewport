using UnityEngine;
using System.Collections.Generic;

public class ApplyMenuAttributes : MonoBehaviour {

	// set number of players and player skins as determined by menu input
	void Start () {
        // access player preferences values set by menu
        int toPlay1 = PlayerPrefs.GetInt("player1ball");
        int toPlay2 = PlayerPrefs.GetInt("player2ball");
        int toPlay3 = PlayerPrefs.GetInt("player3ball");
        int toPlay4 = PlayerPrefs.GetInt("player4ball");

        // get players 
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		int length = players.Length;
	/*	if (length < 2) {
						return;
		}*/
        // make players that were not selected inactive
        for(int i = 0; i < players.Length; i++)
        {
            PlayerControls controls = players[i].GetComponent<PlayerControls>();
            int playerNum = controls.playerNum;

            if (playerNum == 1 && toPlay1 == 0)
                players[i].SetActive(false);
            if (playerNum == 2 && toPlay2 == 0)
                players[i].SetActive(false);
            if (playerNum == 3 && toPlay3 == 0)
                players[i].SetActive(false);
            if (playerNum == 4 && toPlay4 == 0)
                players[i].SetActive(false);
        }
           
        // set player skins
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
