using System.Linq;
using UnityEngine.UI;

using UnityEngine;
using System;
using System.Collections.Generic;

public class endMenuScript : MonoBehaviour {
    
    // Texboxes    
	public List<Text> playerTimeText; // Time Displays
	public List<Text> playerPlaceText; // Player Displays
    public List<Material> materials; // Materials for players

    private List<bool> wasPlaying; // Index = Player #
    private List<GameObject> spheres; // Index = Place #
    private List<GameObject> podiums; // Index = Place #
    private List<KeyValuePair<int, float>> times; // Key = Player #, Value = Time (Seconds)


	void Start () {
		GameObject endMenu = GameObject.Find ("EndMenu");
		RectTransform endMenuTransform = endMenu.GetComponent<RectTransform> ();
		endMenuTransform.anchoredPosition = Vector2.zero;

        InitLists();
        SetSpheres();
		setTimes ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void toMenu() {
		Application.LoadLevel ("menu");
	}

    public void RestartLevel()
    {
        if (PlayerPrefs.HasKey("SelectedLevel"))
        {
            int levelNum = PlayerPrefs.GetInt("SelectedLevel");
            Application.LoadLevel(levelNum);
        } else
        {
            toMenu();
        }
    }
    
    public void InitLists()
    {
        // Access player preferences values set by menu
        wasPlaying = new List<bool>();
        for (int i = 1; i <= 4; i++)
        {
            wasPlaying.Add(PlayerPrefs.GetInt("p" + i + "join") == 1); // wasPlaying[0] = Player 1, wasPlaying[1] = Player 2, etc...
        }

        // Get Podiums
        podiums = new List<GameObject>();
        for (int i = 1; i <= 4; i++)
        {
            podiums.Add(GameObject.Find("Podium" + i)); // Podium 1 = First Place, 2 = Podium Place, etc...
        }

        // Get Spheres
        spheres = new List<GameObject>();
        for (int i = 1; i <= 4; i++)
        {
            spheres.Add(GameObject.Find("Sphere" + i)); // Sphere 1 = First Place, 2 = Second Place, etc...
        }

        // Get Player Time Pairs
        times = new List<KeyValuePair<int, float>>();
        for(int i=1; i<=4; i++)
        {
            if(PlayerPrefs.HasKey("p" + i + "TimeActive"))
            {
                float playerTime = PlayerPrefs.GetFloat("p" + i + "TimeActive");
                times.Add(new KeyValuePair<int, float>(i, playerTime));
            } else
            {
                times.Add(new KeyValuePair<int, float>(i, float.MaxValue - 5.0f));
            }

        }
        times.Sort((x, y) => y.Value.CompareTo(x.Value)); // Reverse sort by value (time).
    }

    public void SetSpheres()
    {
        SetSphereMaterials();
        HideUnsetSpheres();
    }

    public void HideUnsetSpheres()
    {
        // Debugging - If no players were selected, spawn all four.
        if (!wasPlaying[0] && !wasPlaying[1] && !wasPlaying[2] && !wasPlaying[3])
        {
            wasPlaying[0] = true;
            wasPlaying[1] = true;
            wasPlaying[2] = true;
            wasPlaying[3] = true;
        }

        // Make players that were not selected inactive
        for (int i = 0; i < podiums.Count; i++)
        {
            if (!wasPlaying[i])
            {
                podiums[i].SetActive(false);
            }
        }
    }

    public void SetSphereMaterials()
    {
        // Get Spheres
        for (int i = 0; i < 4; i++)
        {
            if (wasPlaying[i])
            {
                int playerNum = times[i].Key;
                int textureNum = PlayerPrefs.GetInt("p" + playerNum + "ball");
                if (textureNum != -2)
                {
                    MeshRenderer currentRenderer = spheres[i].GetComponent<MeshRenderer>();
                    currentRenderer.material = materials[textureNum];
                }
            }
        }
    }

    public void setTimes() {
		//get players
		//generate 2, 3 or 4 places
		//display lowest time first
		//buttons: replay level, quit to menu, new level
        
		//p1ball getInt 
		//textureChange.cs

		for (int i = 0; i < times.Count; i++)
        {
			if(i >= 1)
            {
				TimeSpan t = TimeSpan.FromSeconds(times[i].Value);
				this.playerTimeText[i].text = string.Format (
                    "{0}:{1:00}",
                    (int)t.TotalMinutes,
                    t.Seconds);
			}
            else
            {
				Debug.Log ("Player " + times[i].Key + ": Wins!");
			}

			int playerNumber = times[i].Key;
			string playerNumberStr = "Player " + playerNumber + ": ";
			this.playerPlaceText[i].text = playerNumberStr;
		}
	}	
}