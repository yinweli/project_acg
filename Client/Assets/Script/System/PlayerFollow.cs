using UnityEngine;
using System.Collections;

public class PlayerFollow : MonoBehaviour 
{
    public GameObject ObjTarget = null;
    public Vector3 vecDir;

    void Update()
    {
        if (!ObjTarget)
        {
            Destroy(this);
            return;
        }
        /*
        Vector3 pPos = new Vector3 (ObjTarget.transform.position.x, ObjTarget.transform.position.y, 0);
        transform.position = pPos;
         */

        // 往右.
        if (vecDir.x > 0)
        {
            Vector3 pPos = new Vector3(ObjTarget.transform.position.x - 0.2f, ObjTarget.transform.position.y, 0);
            transform.position = pPos;
            GetComponent<AIPlayer>().FaceTo(180);
        }
        else if (vecDir.x < 0)
        {
            Vector3 pPos = new Vector3(ObjTarget.transform.position.x + 0.2f, ObjTarget.transform.position.y, 0);
            transform.position = pPos;
            GetComponent<AIPlayer>().FaceTo(0);
        }
    }
}
