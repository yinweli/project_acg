using UnityEngine;
using System.Collections;

public class LightFollow : MonoBehaviour 
{
    public GameObject pTarget = null;

    public float zPos = -0.5f;
	// Update is called once per frame
	void Update () {
        if(!pTarget)
            Destroy(gameObject);

        Vector3 pPos = new Vector3(pTarget.transform.position.x, pTarget.transform.position.y, zPos);
        transform.position = pPos;
	}
}
