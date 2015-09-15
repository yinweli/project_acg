using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyLightStop : MonoBehaviour 
{
    AIEnemy pAI = null;

    // 目標.
    public GameObject ObjTarget = null;
    // 方向.
    public Vector3 vecRunDir = Vector3.zero;

    GameObject pObjLight = null;

    bool bStop = false;
    // ------------------------------------------------------------------
    void Start()
    {
        pAI = GetComponent<AIEnemy>();
    } 
    // ------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        if (!SysMain.pthis.bIsGaming)
            return;

        // 沒血逃跑.
        if (pAI.iHP <= 0)
        {
            Run();
            return;
        }

        if (bStop)
        {
            pAI.pAni.speed = 0;
            return;
        }
        else
            pAI.pAni.speed = 1;

        // 已有抓人逃跑模式.
        if (pAI.bHasTarget)
        {
            Take();
            return;
        }

        // 如果有目標且沒抓人時，追蹤目標
        Chace(); 
    }
    // ------------------------------------------------------------------
    // 逃跑.
    void Run()
    {
        // 如果有抓目標就丟下目標.
        if (pAI.bHasTarget)
        {
            pAI.bHasTarget = false;
            if (ObjTarget && ObjTarget.GetComponent<AIPlayer>())
                ObjTarget.GetComponent<AIPlayer>().BeFree();
        }
        // 播放逃跑動作.
        pAI.AniPlay("Escape");

        if (vecRunDir == Vector3.zero)
            GetDir();
        // 調整面向.
        pAI.FaceTo(vecRunDir);

        ToolKit.MoveTo(gameObject, vecRunDir, pAI.GetSpeed() * 4);

        if (EnemyCreater.pthis.CheckPos(gameObject))
            Destroy(gameObject);
    }
    // ------------------------------------------------------------------
    // 把人帶走.
    void Take()
    {
        // 播放抓人動作.
        pAI.AniPlay("Catch");

        // 調整面向.
        pAI.FaceTo(vecRunDir);

        ToolKit.MoveTo(gameObject, vecRunDir, pAI.GetSpeed() * 0.55f);

        if (EnemyCreater.pthis.CheckPos(gameObject))
        {
            if (ObjTarget && ObjTarget.GetComponent<AIPlayer>())
                ObjTarget.GetComponent<AIPlayer>().BeKill();
            Destroy(gameObject);
        }
    }
    // ------------------------------------------------------------------
    // 尋找目標.
    GameObject FindTarget()
    {
        // 如果沒有可以抓的角色.
        if (ToolKit.CatchRole.Count <= 0)
            return null;

        // 沒有目標或目標已不存在可抓佇列就給新目標.
        if (!ObjTarget || !ToolKit.CatchRole.ContainsKey(ObjTarget))
            ObjTarget = ToolKit.GetEnemyTarget();

        return ObjTarget;
    }
    // ------------------------------------------------------------------
    // 追人.
    void Chace()
    {
        // 如果沒人或已經抓了人.
        if (SysMain.pthis.Role.Count == 0 || pAI.bHasTarget)
            return;

        // 有目標追目標.
        if (FindTarget())
        {
            Catch();
            // 調整面向.
            pAI.FaceTo(ObjTarget.transform.position - transform.position);
            // 追追追.
            ToolKit.MoveTo(gameObject, ObjTarget.transform.position - transform.position, pAI.GetSpeed());
        }
        // 沒有目標可抓就慢速追個角色.
        else if (SysMain.pthis.Role.Count > 0)
        {
            GameObject pTempObj = null;
            foreach (KeyValuePair<GameObject, int> itor in SysMain.pthis.Role)
            {
                if (!pTempObj || Vector2.Distance(transform.position, itor.Key.transform.position) < Vector2.Distance(transform.position, pTempObj.transform.position))
                    pTempObj = itor.Key;
            }
            if (pTempObj != null)
            {
                // 調整面向.
                pAI.FaceTo(pTempObj.transform.position - transform.position);
                ToolKit.MoveTo(gameObject, pTempObj.transform.position - transform.position, pAI.GetSpeed() * 0.4f);
            }
            return;
        }
    }
    // ------------------------------------------------------------------
    // 抓人.
    void Catch()
    {
        // 檢查距離是否可抓抓.
        if (GetDistance(gameObject, ObjTarget) < 0.175f)
        {
            pAI.bHasTarget = true;
            if (ObjTarget && ObjTarget.GetComponent<AIPlayer>())
                ObjTarget.GetComponent<AIPlayer>().BeCaught(gameObject, 1);
            GetDir();
        }
    }
    // ------------------------------------------------------------------
    void OnTriggerStay2D(Collider2D other)
    {
        if (pAI.iHP > 0 && other.gameObject.tag == "Look")
        {
            pObjLight = other.gameObject;
            bStop = true;
        }

        if(bStop && !pObjLight)
            bStop = false;
    }
    // ------------------------------------------------------------------
    void OnTriggerExit2D(Collider2D other)
    {
        if (pAI.iHP > 0 && other.gameObject.tag == "Look")
            bStop = false;
    }
    // ------------------------------------------------------------------
    // 取得距離.
    float GetDistance(GameObject ObjMe, GameObject ObjYou)
    {
        return Vector2.Distance(ObjMe.transform.position, ObjYou.transform.position);
    }
    // ------------------------------------------------------------------
    // 取得移動向量.
    void GetDir()
    {
        if (pAI.bHasTarget)
        {
            vecRunDir = ObjTarget.GetComponent<AIPlayer>().GetDeadPos() - transform.position;
            if (ObjTarget && ObjTarget.GetComponent<PlayerFollow>())
                ObjTarget.GetComponent<PlayerFollow>().vecDir = vecRunDir;
        }
        else
            vecRunDir = pAI.PosStart - transform.position;
    }
    // ------------------------------------------------------------------
}
