using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class AutoTile : MonoBehaviour {

    public float globalWidthScaleValue = 1;
    public float globalHeightScaleValue = 1;

    private float prevGlobalWidthScale = 1;
    private float prevGlobalHeightScale = 1;

    private float standardWidth = 10;
    private float standardHeight = 10;
    private float standardWidthTile = 1;
    private float standardHeightTile = 1;
    
	// Use this for initialization
	void Start () {
        UpdateTile();
	}
	
    private void UpdateTile()
    {
        if (gameObject.transform != null)
        {
            float width = gameObject.transform.lossyScale.x;
            float height = gameObject.transform.lossyScale.z;

            float widthMultiplier = width / standardWidth;
            float heightMultiplier = height / standardHeight;

            float widthTile = standardWidthTile * widthMultiplier * globalWidthScaleValue;
            float heightTile = standardHeightTile * heightMultiplier * globalHeightScaleValue;

            if (gameObject.GetComponent<MeshRenderer>() != null)
            {
                MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
                renderer.sharedMaterial.mainTextureScale = new Vector2(widthTile, heightTile);
            }

            prevGlobalWidthScale = globalWidthScaleValue;
            prevGlobalHeightScale = globalHeightScaleValue;
        }
    }

	// Update is called once per frame
	void Update () {
	    if(needUpdate())
        {
            UpdateTile();
            gameObject.transform.hasChanged = false;
        }
	}

    private bool needUpdate()
    {
        return(
            transform.hasChanged ||
            prevGlobalWidthScale != globalWidthScaleValue ||
            prevGlobalHeightScale != globalHeightScaleValue
            );
    }
}
