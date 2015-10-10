using UnityEngine;
using System.Collections;

public class AutoSpin : MonoBehaviour {
    public float spinAmount;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, spinAmount, 0) * Time.deltaTime);
    }
}
