using UnityEngine;
using System.Collections;

public class TextureChange : MonoBehaviour {

    public Texture2D texture;
    private MeshRenderer currentRenderer;
	// Use this for initialization
	void Start () {
        currentRenderer = GetComponent<MeshRenderer>();
        if (texture != null)
        {
            currentRenderer.material.SetColor("_Color", Color.white);
            currentRenderer.material.SetTexture("_MainTex", texture);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
