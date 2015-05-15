using UnityEngine;
using System.Collections;

public class G_Crit : MonoBehaviour 
{
	// Update is called once per frame
	void Update () 
    {
        if (transform.parent.transform.localScale.x == -1)
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);  
	}
}
