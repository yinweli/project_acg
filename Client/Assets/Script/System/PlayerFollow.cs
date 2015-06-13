using UnityEngine;
using System.Collections;

public class PlayerFollow : MonoBehaviour 
{
    public GameObject ObjTarget = null;
    public Vector3 vecDir;

    public int iPos = 1;

    void Update()
    {
        if (!ObjTarget)
        {
            Destroy(this);
            return;
        }

        // 往右.
        if (vecDir.x > 0)
        {
            Vector3 pPos = new Vector3(ObjTarget.transform.position.x - 0.2f -(0.05f * iPos), ObjTarget.transform.position.y, transform.position.z);
            transform.position = pPos;
            GetComponent<AIPlayer>().FaceTo(-1, ObjTarget);
        }
        else if (vecDir.x < 0)
        {
            Vector3 pPos = new Vector3(ObjTarget.transform.position.x + 0.2f + (0.05f * iPos), ObjTarget.transform.position.y, transform.position.z);
            transform.position = pPos;
            GetComponent<AIPlayer>().FaceTo(1, ObjTarget);
        }
    }
}
