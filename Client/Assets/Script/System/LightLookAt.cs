using UnityEngine;
using System.Collections;

public class LightLookAt : MonoBehaviour 
{
    public GameObject pTarget = null;
	// Update is called once per frame
	void Update () 
    {
        transform.LookAt(pTarget.transform);
	}
}
