using UnityEngine;
using System.Collections;

public class AutoSpin : MonoBehaviour {
    public float spinAmount;
    public bool aboutX = false;
    public bool aboutY = true;
    public bool aboutZ = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float x = (aboutX ? spinAmount : 0);
        float y = (aboutY ? spinAmount : 0);
        float z = (aboutZ ? spinAmount : 0);
        transform.Rotate(new Vector3(x, y, z) * Time.deltaTime);
    }
}
