using UnityEngine;
using System.Collections;

public class LightFollow : MonoBehaviour 
{
    public GameObject ObjTarget = null;

    public float zPos = -0.5f;
	// Update is called once per frame
	void Update () {
        if (!ObjTarget)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 pPos = new Vector3(ObjTarget.transform.position.x, ObjTarget.transform.position.y, zPos);
        transform.position = pPos;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 15, transform.localPosition.z);
	}
}
