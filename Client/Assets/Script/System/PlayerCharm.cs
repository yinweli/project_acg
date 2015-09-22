using UnityEngine;
using System.Collections;

public class PlayerCharm : MonoBehaviour 
{
    public GameObject ObjTarget = null;
    public Vector3 vecRunDir;

    public GameObject ObjSfx = null;
    // ------------------------------------------------------------------
    void Start()
    {
        ObjSfx = UITool.pthis.CreateUI(gameObject, "Prefab/Sfx/G_SfxCharm");
        ObjSfx.transform.localPosition = new Vector2(0, 90);
    }
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
    void OnDestroy()
    {
        if (ObjSfx)
            Destroy(ObjSfx);
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

        ToolKit.LocalMoveTo(gameObject, vecRunDir, GameDefine.fBaseSpeed * 1.5f);

        if (vecRunDir.x > 0)
            GetComponent<AIPlayer>().FaceTo(-1, ObjTarget);
        else if (vecRunDir.x < 0)
            GetComponent<AIPlayer>().FaceTo(1, ObjTarget);
    }
}
