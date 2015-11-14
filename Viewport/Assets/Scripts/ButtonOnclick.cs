using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonOnclick : MonoBehaviour {
    public Sprite[] mySprites;
    public Sprite[] myArrows;
    public Sprite[] myLevels;
    public int ball1, ball2, ball3, ball4;
    public Image[] images;
    public Image[] arrows;
    public Image[] levels;
    public Image[] menuArrows;
    public int a1, a2, a3, a4;
    bool p1, p2, p3, p4;
    public int menu;
    Image levelImage;
    int image;



    public void changeScene()
    {
        Application.LoadLevel(image);
    }
    //sets player preferences to each player ball choice
    public void submitPlayers()
    {
        PlayerPrefs.SetInt("player1ball", ball1);
        PlayerPrefs.SetInt("player2ball", ball2);
        PlayerPrefs.SetInt("player3ball", ball3);
        PlayerPrefs.SetInt("player4ball", ball4);
        PlayerPrefs.SetInt("p1", p1 ? 1 : 0);
        PlayerPrefs.SetInt("p2", p2 ? 1 : 0);
        PlayerPrefs.SetInt("p3", p3 ? 1 : 0);
        PlayerPrefs.SetInt("p4", p4 ? 1 : 0);
    }
    public void endMenu()
    {
        Application.Quit();
    }
   void Update()
    {
        //shows which menu is enabled
        //changes on menu transition
        if (menu == 1)
       {    
            //Enables/disables player when "A" button is pressed
            if(Input.GetKeyUp("joystick 1 button 0"))
            { 
                images[0].enabled = !images[0].enabled;
                p1 = !p1;
            }
            
            if (Input.GetKeyUp("joystick 2 button 0"))
            {
                images[1].enabled = !images[1].enabled;
                p2 = !p2;
            }
            
            if (Input.GetKeyUp("joystick 3 button 0"))
            {
                images[2].enabled = !images[2].enabled;
                p3 = !p3;
            }
           
            if (Input.GetKeyUp("joystick 4 button 0"))
            {
                images[3].enabled = !images[3].enabled;
                p4 = !p4;
            }
            //Cycles through material options for each ball
            //does not allow repeats

            if (Input.GetKeyUp("e") || Input.GetKeyUp("joystick 1 button 4") && p1)
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

            if (Input.GetKeyUp("d") || Input.GetKeyUp("joystick 1 button 5") && p1)
            {
                arrows[1].sprite = myArrows[1];
                a1 = 5;
                ball1--;
                if (ball1 < 0)
                    ball1 = mySprites.Length - 1;
                while (ball1 == ball2 || ball1 == ball3 || ball1 == ball4)
                {
                    ball1--;
                    if (ball1 < 0)
                        ball1 = mySprites.Length - 1;
                }
                images[0].sprite = mySprites[ball1];
            }

            if (Input.GetKeyUp("r") || Input.GetKeyUp("joystick 2 button 4") && p2)
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

            if (Input.GetKeyUp("f") || Input.GetKeyUp("joystick 2 button 5") && p2)
            {
                arrows[3].sprite = myArrows[1];
                a2 = 5;
                ball2--;
                if (ball2 < 0)
                    ball2 = mySprites.Length - 1;
                while (ball1 == ball2 || ball2 == ball3 || ball2 == ball4)
                {
                    ball2--;
                    if (ball2 < 0)
                        ball2 = mySprites.Length - 1;
                }
                images[1].sprite = mySprites[ball2];
            }

            if (Input.GetKeyUp("t") || Input.GetKeyUp("joystick 3 button 4") && p3)
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

            if (Input.GetKeyUp("g") || Input.GetKeyUp("joystick 3 button 5") && p3)
            {
                arrows[5].sprite = myArrows[1];
                a3 = 5;
                ball3--;
                if (ball3 < 0)
                    ball3 = mySprites.Length - 1;
                while (ball1 == ball3 || ball2 == ball3 || ball3 == ball4)
                {
                    ball3--;
                    if (ball3 < 0)
                        ball3 = mySprites.Length - 1;
                }
                images[2].sprite = mySprites[ball3];
            }

            if (Input.GetKeyUp("y") || Input.GetKeyUp("joystick 4 button 4") && p4)
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

            if (Input.GetKeyUp("h") || Input.GetKeyUp("joystick 4 button 5") && p4)
            {
                arrows[7].sprite = myArrows[1];
                a4 = 5;
                ball4--;
                if (ball4 < 0)
                    ball4 = mySprites.Length - 1;
                while (ball1 == ball4 || ball4 == ball2 || ball3 == ball4)
                {
                    ball4--;
                    if (ball4 < 0)
                        ball4 = mySprites.Length - 1;
                }
                images[3].sprite = mySprites[ball4];
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
        //shows level select menu is enabled
        if(menu == 2)
        {
            //cycles through each menu option
            //arrows flash
            if(Input.GetKeyUp("left") || Input.GetKeyUp("joystick 1 button 4"))
            {
                menuArrows[0].sprite = myArrows[1];
                a1 = 5;
                if(image > 0)
                {
                    image--;
                    levelImage.sprite = myLevels[image];
                }                
                else
                {
                    image = myLevels.Length - 1;
                    levelImage.sprite = myLevels[image];

                }
            }
            if(Input.GetKeyUp("right") || Input.GetKeyUp("joystick 1 button 5"))
            {
                menuArrows[1].sprite = myArrows[1];
                a1 = 5;
                if(image < myLevels.Length - 1)
                {
                    image++;
                    levelImage.sprite = myLevels[image];
                }                
                else
                {
                    image = 0;
                    levelImage.sprite = myLevels[image];

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
       
    }
    void Start()
    {
        //finds all game objects
        mySprites = (Sprite[])Resources.LoadAll<Sprite>("ballsprites");
        images = GameObject.Find("Balls").GetComponentsInChildren<Image>();
        myArrows = (Sprite[])Resources.LoadAll<Sprite>("arrowsprites");
        arrows = GameObject.Find("arrows").GetComponentsInChildren<Image>();
        menuArrows = GameObject.Find("levelarrow").GetComponentsInChildren<Image>();
        myLevels = (Sprite[])Resources.LoadAll<Sprite>("levelsprites");
        levelImage = GameObject.Find("levelimage").GetComponent<Image>();
        image = 0;
        ball1 = 0;
        ball2 = 1;
        ball3 = 2;
        ball4 = 3;
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
    }
}
