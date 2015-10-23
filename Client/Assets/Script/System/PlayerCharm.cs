using UnityEngine;
using System.Collections;

public class PlayerCharm : MonoBehaviour 
{
    public GameObject ObjTarget = null;
    public GameObject ObjShield = null;
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
        if (!ObjTarget)
        {
            Destroy(this);
            return;
        }
        vecRunDir = ObjTarget.transform.position - transform.position;

        if (Vector2.Distance(transform.position, ObjTarget.transform.position) > 0.15f)
        {
            float fSpeed = GameDefine.fBaseSpeed * 1.5f;

            if (ObjShield && ObjShield.GetComponent<Freeze>())
                fSpeed = GameDefine.fBaseSpeed * 1.5f * Rule.UpgradeWeaponPistol();

            ToolKit.LocalMoveTo(gameObject, vecRunDir, fSpeed);
        }

        if (vecRunDir.x > 0)
            GetComponent<AIPlayer>().FaceTo(-1, ObjTarget);
        else if (vecRunDir.x < 0)
            GetComponent<AIPlayer>().FaceTo(1, ObjTarget);
    }
}
