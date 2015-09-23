using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBewitch : MonoBehaviour
{
    AIEnemy pAI = null;

    // 目標.
    public GameObject ObjTarget = null;
    public GameObject ObjBullet = null;
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
        if (pAI.iHP <= 0)
        {
            Run();
            return;
        }

        // 已有抓人逃跑模式.
        if (ObjBullet != null || pAI.bHasTarget)
        {
            Take();
            return;
        }

        Chace();
    }
    // ------------------------------------------------------------------
    // 逃跑.
    void Run()
    {
        // 播放逃跑動作.
        pAI.AniPlay("Escape");

        // 調整面向.
        pAI.FaceTo(transform.position - CameraCtrl.pthis.GetMyObj().transform.position);

        ToolKit.MoveTo(gameObject, transform.position - CameraCtrl.pthis.GetMyObj().transform.position, pAI.GetSpeed() * 4);

        if (EnemyCreater.pthis.CheckPos(gameObject))
            Destroy(gameObject);
    }
    // ------------------------------------------------------------------
    // 把人帶走.
    void Take()
    {
        if (!ObjTarget)
            return;

        // 播放抓人動作.
        pAI.AniPlay("Catch");

        if (EnemyCreater.pthis.CheckPos(gameObject) && GetDistance(gameObject, ObjTarget) < 0.3f)
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
        if (!ObjTarget)
            return;

        // 檢查距離是否可抓抓.
        if (GetDistance(gameObject, ObjTarget) < 1.5f)
        {
            pAI.bHasTarget = true;

            if (ObjTarget && ObjTarget.GetComponent<AIPlayer>())
            {
                ObjBullet = UITool.pthis.CreateUIByPos(gameObject.transform.parent.gameObject, "G_Love", gameObject.transform.localPosition.x, gameObject.transform.localPosition.y);
                ObjBullet.GetComponent<AIBullet_Bewitch>().InitBullet(ObjTarget, pAI, this);
            }
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
