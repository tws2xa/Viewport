using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonOnclick : MonoBehaviour {
    public Sprite[] mySprites;
    public Sprite[] myArrows;
    public int ball1, ball2, ball3, ball4;
    public Image[] images;
    public Image[] arrows;
    public int a1, a2, a3, a4;
    

    public void changeScene(int scene)
    {
        Application.LoadLevel(scene);
    }
    public void endMenu()
    {
        Application.Quit();
    }
   void Update()
    {
       // if(Input.GetAxis("Vertical_p1") > 0)
       if(Input.GetKeyUp("e"))
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
       
        if (Input.GetKeyUp("d"))
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
        
        if (Input.GetKeyUp("r"))
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
        
        if (Input.GetKeyUp("f"))
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
        
        if (Input.GetKeyUp("t"))
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
        
        if (Input.GetKeyUp("g"))
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
       
        if (Input.GetKeyUp("y"))
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
        
        if (Input.GetKeyUp("h"))
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
    void Start()
    {
        mySprites = (Sprite[])Resources.LoadAll<Sprite>("ballsprites");
        images = GameObject.Find("Balls").GetComponentsInChildren<Image>();
        myArrows = (Sprite[])Resources.LoadAll<Sprite>("arrowsprites");
        arrows = GameObject.Find("arrows").GetComponentsInChildren<Image>();
        ball1 = 0;
        ball2 = 1;
        ball3 = 2;
        ball4 = 3;
        a1 = 0;
        a2 = 0;
        a3 = 0;
        a4 = 0;
    }
}
