using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextureChange : MonoBehaviour {
    public List<Material> materials; 
    //public Texture2D texture;
    private MeshRenderer currentRenderer;
    int pNum;
    int tNum;
	// Use this for initialization
	void Start () {
        pNum = gameObject.GetComponent<PlayerControls>().playerNum;
        string key = "p" + pNum + "ball";

        if (!PlayerPrefs.HasKey(key))
        {
            tNum = gameObject.GetComponent<PlayerControls>().playerNum;
        }
        else
        {
            tNum = PlayerPrefs.GetInt(key);
        }

        currentRenderer = gameObject.GetComponent<MeshRenderer>();
        if (tNum != -2)
        {
            currentRenderer.material = materials[tNum];
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    
}
