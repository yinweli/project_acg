using UnityEngine;
using System.Collections;

public class MapMove : MonoBehaviour 
{
	void Update () 
    {
	    if(SysMain.pthis.bIsGaming)
            transform.Translate(new Vector3(0, -0.08f * Time.deltaTime, 0));        
	}
}
