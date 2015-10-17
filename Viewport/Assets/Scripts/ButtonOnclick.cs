using UnityEngine;
using System.Collections;

public class ButtonOnclick : MonoBehaviour {

	public void changeScene(int scene)
    {
        Application.LoadLevel(scene);
    }
    public void endMenu()
    {
        Application.Quit();
    }
}
