using UnityEngine;
using System.Collections;

public class ArrowFollow : MonoBehaviour {

    public GameObject target;

	void Update () {
        if (target == null) {
            Destroy(this.gameObject);
            return;
        }

        Vector3 targetPos = Camera.main.WorldToViewportPoint(target.transform.position);

        if (target.GetComponent<PlayerDeathController>().getPrevState()) {
            this.GetComponent<Renderer>().enabled = false;
            return;
        } else {
            this.GetComponent<Renderer>().enabled = true;
        }

        targetPos.x -= 0.45f;  // Translate to use center of viewport
        targetPos.y -= 0.45f;
        targetPos.z = 0;      // I think I can do this rather than do a 
                          //   a full projection onto the plane

        float fAngle = Mathf.Atan2(targetPos.x, targetPos.y);
        transform.localEulerAngles = new Vector3(0.0f, 0.0f, -fAngle * Mathf.Rad2Deg);

        targetPos.x = 0.45f * Mathf.Sin(fAngle) + 0.5f;  // Place on ellipse touching 
        targetPos.y = 0.45f * Mathf.Cos(fAngle) + 0.5f;  //   side of viewport
        targetPos.z = Camera.main.nearClipPlane + 10.5f;  // Looking from neg to pos Z;
        transform.position = Camera.main.ViewportToWorldPoint(targetPos);
        transform.LookAt(target.transform.position);
        transform.Rotate(0f, 0f, -transform.rotation.z);
    }
}
