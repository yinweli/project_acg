using UnityEngine;
using System.Collections;

public class LightFollow : MonoBehaviour 
{
    public GameObject pTarget = null;
	// Update is called once per frame
	void Update () {
        if(!pTarget)
            Destroy(gameObject);

        Vector3 pPos = new Vector3(pTarget.transform.position.x, pTarget.transform.position.y, -0.2f);
        transform.position = pPos;
	}
}
