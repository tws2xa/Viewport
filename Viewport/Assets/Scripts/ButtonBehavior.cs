using UnityEngine;
using System.Collections;

public class ButtonBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseEnter()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
    void OnMouseExit()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }
}
