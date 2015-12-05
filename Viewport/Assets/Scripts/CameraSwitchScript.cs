using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraSwitchScript : MonoBehaviour {

    public KeyCode switchKey;
    private Camera[] cams;
    int currentCam = 0;

	// Use this for initialization
	void Start () {
        cams = Camera.allCameras;
	}
	
	// Update is called once per frame
	void Update () {
	    if(switchKey > 0 && Input.GetKeyUp(switchKey))
        {
            currentCam = (currentCam + 1) % cams.Length;
            ShowCam(currentCam);
        }
	}

    private void ShowCam(int camNum)
    {
        for(int i=0; i<cams.Length; i++)
        {
            cams[i].enabled = (camNum == i);
        }
    }
}
