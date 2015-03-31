using UnityEngine;
using System.Collections;

public class PlayerFollow : MonoBehaviour 
{
    public GameObject ObjTarget = null;

    // Update is called once per frame
    void Update()
    {
        if (!ObjTarget)
        {
            Destroy(this);
            return;
        }

        Vector3 pPos = new Vector3(ObjTarget.transform.position.x, ObjTarget.transform.position.y, 0);
        transform.position = pPos;
    }
}
