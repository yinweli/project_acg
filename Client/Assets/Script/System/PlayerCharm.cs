using UnityEngine;
using System.Collections;

public class PlayerCharm : MonoBehaviour 
{
    public GameObject ObjTarget = null;
    public Vector3 vecRunDir;
    // ------------------------------------------------------------------
    void Update()
    {
        if (!SysMain.pthis.bIsGaming)
            return;

        if (Vector2.Distance(transform.position, ObjTarget.transform.position) < 0.15f)
            return;

        WalktoTarget();
    }
    // ------------------------------------------------------------------
    void WalktoTarget()
    {
        vecRunDir = ObjTarget.transform.position - transform.position;

        if (!ObjTarget)
        {
            Destroy(this);
            return;
        }

        ToolKit.LocalMoveTo(gameObject, vecRunDir, GameDefine.fBaseSpeed);

        if (vecRunDir.x > 0)
            GetComponent<AIPlayer>().FaceTo(-1, ObjTarget);
        else if (vecRunDir.x < 0)
            GetComponent<AIPlayer>().FaceTo(1, ObjTarget);
    }
}
