using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class ButtonOnclick : MonoBehaviour {
    public Sprite[] mySprites;
    private Sprite[] myArrows;
    public Sprite[] levelSprites;
    private int ball1, ball2, ball3, ball4;
    private Image[] images;
    public string[] levelNames;
    private Image[] arrows;
    private Image[] menuArrows;
    private int a1, a2, a3, a4;
    private bool p1, p2, p3, p4;
    private int menu;
    private Image levelImage;
    private int levelSelected;

	private int menuSelector = 0;
	private float menuSelectorTimer = 1.5f;
	private bool menuSelectorTimerActive = false;

	public GameObject[] arrowSelectors;

    GameObject mainMenu;
    GameObject selectPlayersMenu;
    GameObject selectLevelMenu;
    
    public void changeScene()
    {
        String levelString = levelNames[levelSelected];
        PlayerPrefs.SetString("SelectedLevel", levelString);
        Application.LoadLevel(levelString);
    }
    public void showMenu(int menuNum)
    {
        if(menu < 0 || menu > 3)
        {
            Debug.LogError("Error - Setting Menu to Invalid Number: " + menuNum);
            return;
        }
        mainMenu.SetActive(menuNum == 0);
        selectPlayersMenu.SetActive(menuNum == 1);
        selectLevelMenu.SetActive(menuNum == 2);

        this.menu = menuNum;
    }

    //sets player preferences to each player ball choice
    public void submitPlayers()
    {
        int join1 = p1 ? 1 : 0;
        int join2 = p2 ? 1 : 0;
        int join3 = p3 ? 1 : 0;
        int join4 = p4 ? 1 : 0;
        if (join1 + join2 + join3 + join4 >= 2)
        {
            PlayerPrefs.SetInt("p1ball", ball1);
            PlayerPrefs.SetInt("p2ball", ball2);
            PlayerPrefs.SetInt("p3ball", ball3);
            PlayerPrefs.SetInt("p4ball", ball4);
            PlayerPrefs.SetInt("p1join", join1);
            PlayerPrefs.SetInt("p2join", join2);
            PlayerPrefs.SetInt("p3join", join3);
            PlayerPrefs.SetInt("p4join", join4);
            showMenu(2);
        }
    }

    public void endMenu()
    {
        Application.Quit();
    }

   void Update()
    {
        // Shows which menu is enabled
        // Changes on menu transition
//		if (menu == 0) {
//
//		}
//        else if (menu == 1) // Select Player
//        {
//            HandleSelectPlayerUpdate();
//        }
//        else if(menu == 2) // Select Level
//        {
//            HandleSelectLevelUpdate();
//        }
		switch (menu) {
		case 0:
			HandleSelectMenuUpdate();
			break;
		case 1:
			HandleSelectPlayerUpdate();
			break;
		case 2:
			HandleSelectLevelUpdate();
			break;
		}
       
    }

    public int GetFirstUnusedImageInt()
    {
        return GetNextUnusedImageInt(0);
    }

    /// <summary>
    /// Find the next unused ball image starting from start (inclusive).
    /// </summary>
    /// <param name="start">Where to begin checking for unused images (inclusive)</param>
    public int GetNextUnusedImageInt(int start)
    {
        return FindUnusedImage(start, 1);
    }

    /// <summary>
    /// Find the previous unused ball image starting from start (inclusive).
    /// </summary>
    /// <param name="start">Where to begin checking for unused images (inclusive)</param>
    public int GetPreviousUnusedImageInt(int start)
    {
        return FindUnusedImage(start, -1);
    }

    /// <summary>
    /// Finds the next unused images
    /// </summary>
    /// <param name="start">Where to start (inclusive)</param>
    /// <param name="delta">How much to change each iteration</param>
    public int FindUnusedImage(int start, int delta)
    {
        int i = start;
        while (ball1 == i || ball2 == i || ball3 == i || ball4 == i)
        {
            i = (i + delta) % mySprites.Length;
            if (i == start)
            {
                // Not enough sprite images for players
                Debug.LogError("More Players Than Sprites!");
                return -1;
            }
        }
        return i;
    }

	public void HandleSelectMenuUpdate()
	{
		if (menuSelectorTimerActive) {
			menuSelectorTimer += Time.deltaTime;
			if (Mathf.Abs(Input.GetAxis ("Vertical_p1")) < 0.05) {
				menuSelectorTimer = 1.5f;
			}
		}
		if (menuSelectorTimer > 1.0f) {
			menuSelectorTimerActive = false;
			if (Input.GetAxis ("Vertical_p1") > 0.5f || Input.GetKeyDown (KeyCode.UpArrow)) {
				menuSelector = Mathf.Max (0, menuSelector - 1);
				menuSelectorTimer = 0.0f;
				menuSelectorTimerActive = true;
			}
			else if (Input.GetAxis ("Vertical_p1") < -0.5f || Input.GetKeyDown (KeyCode.DownArrow)) {
				menuSelector = Mathf.Min (2, menuSelector + 1);
				menuSelectorTimer = 0.0f;
				menuSelectorTimerActive = true;
			}
			for (int i = 0; i < arrowSelectors.Length; i++) {
				if (i == menuSelector && !arrowSelectors[i].activeInHierarchy) {
					arrowSelectors[i].SetActive(true);
				} else if (i != menuSelector && arrowSelectors[i].activeInHierarchy) {
					arrowSelectors[i].SetActive(false);
					Debug.Log ("Setting False");
				}
			}
		}
		if (Input.GetKeyUp ("joystick 1 button 0") || Input.GetKeyDown (KeyCode.A)) {
			switch (menuSelector) {
			case 0:
				showMenu(1);
				break;
			case 1:
				endMenu();
				break;
			case 2:
				endMenu();
				break;
			}
		}
	}

    public void HandleSelectPlayerUpdate()
    {
		if (Input.GetKeyUp ("joystick 1 button 0") || Input.GetKeyUp (KeyCode.A)) {
			submitPlayers();
		}
        //Enables/disables player when "A" button is pressed
        if (Input.GetKeyUp("joystick 1 button 7") || Input.GetKeyUp("c"))
        {
            p1 = !p1;
            if(p1)
            {
                ball1 = GetFirstUnusedImageInt();
                images[0].sprite = mySprites[ball1];
            } else
            {
                ball1 = -2;
            }
            images[0].enabled = !images[0].enabled;
        }

        if (Input.GetKeyUp("joystick 2 button 7") || Input.GetKeyUp("v"))
        {
            p2 = !p2;
            if (p2)
            {
                ball2 = GetFirstUnusedImageInt();
                images[1].sprite = mySprites[ball2];
            }
            else
            {
                ball2 = -2;
            }
            images[1].enabled = !images[1].enabled;
        }

        if (Input.GetKeyUp("joystick 3 button 7") || Input.GetKeyUp("b"))
        {
            p3 = !p3;
            if (p3)
            {
                ball3 = GetFirstUnusedImageInt();
                images[2].sprite = mySprites[ball3];
            }
            else
            {
                ball3 = -2;
            }
            images[2].enabled = !images[2].enabled;
        }

        if (Input.GetKeyUp("joystick 4 button 7") || Input.GetKeyUp("n"))
        {
            p4 = !p4;
            if (p4)
            {
                ball4 = GetFirstUnusedImageInt();
                images[3].sprite = mySprites[ball4];
            }
            else
            {
                ball4 = -2;
            }
            images[3].enabled = !images[3].enabled;
        }

        //Cycles through material options for each ball
        //does not allow repeats
        if ((Input.GetKeyUp("e") || Input.GetKeyUp("joystick 1 button 4")) && p1)
        {
            arrows[0].sprite = myArrows[1];
            a1 = 5;
            ball1++;
            if (ball1 > mySprites.Length - 1)
                ball1 = 0;
            while (ball1 == ball2 || ball1 == ball3 || ball1 == ball4)
            {
                ball1++;
                if (ball1 > mySprites.Length - 1)
                    ball1 = 0;
            }
            images[0].sprite = mySprites[ball1];
        }

        if ((Input.GetKeyUp("d") || Input.GetKeyUp("joystick 1 button 5")) && p1)
        {
            arrows[1].sprite = myArrows[1];
            a1 = 5;
            ball1--;
            if (ball1 < 0 && ball1 != -2)
                ball1 = mySprites.Length - 1;
            while (ball1 == ball2 || ball1 == ball3 || ball1 == ball4)
            {
                ball1--;
                if (ball1 < 0 && ball1 != -2)
                    ball1 = mySprites.Length - 1;
            }
            images[0].sprite = mySprites[ball1];
        }

        if ((Input.GetKeyUp("r") || Input.GetKeyUp("joystick 2 button 4")) && p2)
        {
            arrows[2].sprite = myArrows[1];
            a2 = 5;
            ball2++;
            if (ball2 > mySprites.Length - 1)
                ball2 = 0;
            while (ball2 == ball1 || ball2 == ball3 || ball2 == ball4)
            {
                ball2++;
                if (ball2 > mySprites.Length - 1)
                    ball2 = 0;
            }
            images[1].sprite = mySprites[ball2];
        }

        if ((Input.GetKeyUp("f") || Input.GetKeyUp("joystick 2 button 5")) && p2)
        {
            arrows[3].sprite = myArrows[1];
            a2 = 5;
            ball2--;
            if (ball2 < 0 && ball2 != -2)
                ball2 = mySprites.Length - 1;
            while (ball1 == ball2 || ball2 == ball3 || ball2 == ball4)
            {
                ball2--;
                if (ball2 < 0 && ball2 != -2)
                    ball2 = mySprites.Length - 1;
            }
            images[1].sprite = mySprites[ball2];
        }

        if ((Input.GetKeyUp("t") || Input.GetKeyUp("joystick 3 button 4")) && p3)
        {
            arrows[4].sprite = myArrows[1];
            a3 = 5;
            ball3++;
            if (ball3 > mySprites.Length - 1)
                ball3 = 0;
            while (ball1 == ball3 || ball2 == ball3 || ball3 == ball4)
            {
                ball3++;
                if (ball3 > mySprites.Length - 1)
                    ball3 = 0;
            }
            images[2].sprite = mySprites[ball3];
        }

        if ((Input.GetKeyUp("g") || Input.GetKeyUp("joystick 3 button 5")) && p3)
        {
            arrows[5].sprite = myArrows[1];
            a3 = 5;
            ball3--;
            if (ball3 < 0 && ball3 != -2)
                ball3 = mySprites.Length - 1;
            while (ball1 == ball3 || ball2 == ball3 || ball3 == ball4)
            {
                ball3--;
                if (ball3 < 0 && ball3 != -2)
                    ball3 = mySprites.Length - 1;
            }
            images[2].sprite = mySprites[ball3];
        }

        if ((Input.GetKeyUp("y") || Input.GetKeyUp("joystick 4 button 4")) && p4)
        {
            arrows[6].sprite = myArrows[1];
            a4 = 5;
            ball4++;
            if (ball4 > mySprites.Length - 1)
                ball4 = 0;
            while (ball4 == ball1 || ball2 == ball4 || ball3 == ball4)
            {
                ball4++;
                if (ball4 > mySprites.Length - 1)
                    ball4 = 0;
            }
            images[3].sprite = mySprites[ball4];
        }

        if ((Input.GetKeyUp("h") || Input.GetKeyUp("joystick 4 button 5")) && p4)
        {
            arrows[7].sprite = myArrows[1];
            a4 = 5;
            ball4--;
            if (ball4 < 0 && ball4 != -2)
                ball4 = mySprites.Length - 1;
            while (ball1 == ball4 || ball4 == ball2 || ball3 == ball4)
            {
                ball4--;
                if (ball4 < 0 && ball4 != -2)
                    ball4 = mySprites.Length - 1;
            }
            images[3].sprite = mySprites[ball4];
        }
        
        //Cycles through colors to a new one that isn't selected
        if (p1 && ball1 == -2)
        {
            for (int i = 0; i <= mySprites.Length - 1; i++)
            {
                if (i != ball2 && i != ball3 && i != ball4)
                {
                    ball1 = i;
                    break;
                }
            }
        }
        if (p2 && ball1 == -2)
        {
            for (int i = 0; i <= mySprites.Length - 1; i++)
            {
                if (i != ball1 && i != ball3 && i != ball4)
                {
                    ball2 = i;
                    break;
                }
            }
        }
        if (p3 && ball1 == -2)
        {
            for (int i = 0; i <= mySprites.Length - 1; i++)
            {
                if (i != ball2 && i != ball1 && i != ball4)
                {
                    ball3 = i;
                    break;
                }
            }
        }
        if (p4 && ball1 == -2)
        {
            for (int i = 0; i <= mySprites.Length - 1; i++)
            {
                if (i != ball2 && i != ball3 && i != ball1)
                {
                    ball4 = i;
                    break;
                }
            }
        }

        //causes left and right arrows to flash on key press
        if (a1 > 0)
            a1--;
        else
        {
            arrows[1].sprite = myArrows[0];
            arrows[0].sprite = myArrows[0];
        }
        if (a2 > 0)
            a2--;
        else
        {
            arrows[3].sprite = myArrows[0];
            arrows[2].sprite = myArrows[0];
        }
        if (a3 > 0)
            a3--;
        else
        {
            arrows[5].sprite = myArrows[0];
            arrows[4].sprite = myArrows[0];
        }
        if (a4 > 0)
            a4--;
        else
        {
            arrows[7].sprite = myArrows[0];
            arrows[6].sprite = myArrows[0];
        }

    }

    public void HandleSelectLevelUpdate()
    {
		if (Input.GetKeyUp ("joystick 1 button 0") || Input.GetKeyUp (KeyCode.A)) {
			changeScene();
		}
        //cycles through each menu option
        //arrows flash
        if (Input.GetKeyUp("left") || Input.GetKeyUp("joystick 1 button 4"))
        {
            menuArrows[0].sprite = myArrows[1];
            a1 = 5;
            if (levelSelected > 0)
            {
                levelSelected--;
                levelImage.sprite = levelSprites[levelSelected];
            }
            else
            {
                levelSelected = levelSprites.Length - 1;
                levelImage.sprite = levelSprites[levelSelected];

            }
        }
        if (Input.GetKeyUp("right") || Input.GetKeyUp("joystick 1 button 5"))
        {
            menuArrows[1].sprite = myArrows[1];
            a1 = 5;
            if (levelSelected < levelSprites.Length - 1)
            {
                levelSelected++;
                levelImage.sprite = levelSprites[levelSelected];
            }
            else
            {
                levelSelected = 0;
                levelImage.sprite = levelSprites[levelSelected];

            }
        }
        if (a1 > 0)
            a1--;
        else
        {
            menuArrows[1].sprite = myArrows[0];
            menuArrows[0].sprite = myArrows[0];
        }
    }

    private void ClearPrefs()
    {
        PlayerPrefs.DeleteKey("p1ball");
        PlayerPrefs.DeleteKey("p2ball");
        PlayerPrefs.DeleteKey("p3ball");
        PlayerPrefs.DeleteKey("p4ball");
        PlayerPrefs.DeleteKey("p1join");
        PlayerPrefs.DeleteKey("p2join");
        PlayerPrefs.DeleteKey("p3join");
        PlayerPrefs.DeleteKey("p4join");
        PlayerPrefs.DeleteKey("SelectedLevel");
    }

    public void Start()
    {
        ClearPrefs();

        //finds all game objects
        // mySprites = (Sprite[])Resources.LoadAll<Sprite>("ballsprites");
        images = GameObject.Find("Balls").GetComponentsInChildren<Image>();
        myArrows = (Sprite[])Resources.LoadAll<Sprite>("arrowsprites");
        arrows = GameObject.Find("arrows").GetComponentsInChildren<Image>();
        menuArrows = GameObject.Find("levelarrow").GetComponentsInChildren<Image>();
        //levelSprites = (Sprite[])Resources.LoadAll<Sprite>("levelsprites");
        levelImage = GameObject.Find("levelimage").GetComponent<Image>();
        levelSelected = 0;
        ball1 = -2;
        ball2 = -2;
        ball3 = -2;
        ball4 = -2;
        menu = 1;
        a1 = 0;
        a2 = 0;
        a3 = 0;
        a4 = 0;
        p1 = false;
        p2 = false;
        p3 = false;
        p4 = false;
        foreach(Image i in images)
        {
            i.enabled = false;
        }

        // Gather the menu objects
        mainMenu = GameObject.Find("Main Menu");
        selectPlayersMenu = GameObject.Find("Select Players");
        selectLevelMenu = GameObject.Find("Select Level");

        // Set Menu Positions
        RectTransform mainMenuTransform = mainMenu.GetComponent<RectTransform>();
        RectTransform selectPlayersMenuTransform = selectPlayersMenu.GetComponent<RectTransform>();
        RectTransform selectLevelMenuTransform = selectLevelMenu.GetComponent<RectTransform>();

        mainMenuTransform.anchoredPosition = Vector2.zero;
        selectPlayersMenuTransform.anchoredPosition = Vector2.zero;
        selectLevelMenuTransform.anchoredPosition = Vector2.zero;

        // Show the Main Menu
        showMenu(0);
    }
}
