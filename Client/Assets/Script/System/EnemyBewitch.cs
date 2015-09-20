using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBewitch : MonoBehaviour
{
    AIEnemy pAI = null;

    // 目標.
    public GameObject ObjTarget = null;
    // 方向.
    public Vector3 vecRunDir = Vector3.zero;
    // ------------------------------------------------------------------
    void Start()
    {
        pAI = GetComponent<AIEnemy>();
    }
    // ------------------------------------------------------------------
    void Update()
    {
        if (!SysMain.pthis.bIsGaming)
            return;

        // 沒血逃跑.
        /*if (pAI.iHP <= 0)
        {
            Run();
            return;
        }*/

        // 已有抓人逃跑模式.
        if (pAI.bHasTarget)
        {
            Take();
            return;
        }

        Chace();
    }
     // ------------------------------------------------------------------
    // 把人帶走.
    void Take()
    {
        // 播放抓人動作.
        pAI.AniPlay("Catch");

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
        }
        // 沒有目標可抓就慢速追個角色.
        else
            pAI.AniPlay("Wait");
    }
    // ------------------------------------------------------------------
    // 施放魅惑抓人.
    void Catch()
    {
        // 檢查距離是否可抓抓.
        if (GetDistance(gameObject, ObjTarget) < 1.0f)
        {
            pAI.bHasTarget = true;
            if (ObjTarget && ObjTarget.GetComponent<AIPlayer>())
                ObjTarget.GetComponent<AIPlayer>().BeWitch(gameObject, 1);
        }
    }
    // ------------------------------------------------------------------
    // 取得距離.
    float GetDistance(GameObject ObjMe, GameObject ObjYou)
    {
        return Vector2.Distance(ObjMe.transform.position, ObjYou.transform.position);
    }
    // ------------------------------------------------------------------
}
