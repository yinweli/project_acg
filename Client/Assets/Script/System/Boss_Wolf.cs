using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss_Wolf : MonoBehaviour
{
    AIEnemy pAI = null;

    public GameObject ObjTarget = null;
    // 方向.
    public Vector3 vecRunDir = Vector3.zero;

	public float fMeetDis = 1.25f;

    public int iMaxHP = 0;
	public float fNextHP = 0;
    public float fMeetpercent = 0.28f;
    public int iMeetCount = 3;
    // ------------------------------------------------------------------
    void Start()
    {
        pAI = GetComponent<AIEnemy>();
        iMaxHP = Rule.BossHP(pAI.DBFData.HP);
		fNextHP = iMaxHP * (fMeetpercent * iMeetCount);
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

        // 檢查是否要產生肉肉.
        if (pAI.iHP < iMaxHP * (fMeetpercent * iMeetCount))
        {
			fNextHP = iMaxHP * (fMeetpercent * iMeetCount);
            EnemyCreater.pthis.CreateByStandOutRoad(0, -2, 12);
            iMeetCount--;
        }

        // 確認是否追肉肉.
        if (CheckMeet())
            HuntMeet();
        else if (pAI.bHasTarget) // 已有抓人逃跑模式.
            Take();
        else  // 如果有目標且沒抓人時，追蹤目標
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

        pAI.bHasTarget = false;
        if (vecRunDir == Vector3.zero)
            GetDir();
        // 調整面向前進.
        pAI.FaceAndMove(vecRunDir, 3.5f);

        if (EnemyCreater.pthis.CheckPos(gameObject))
            Destroy(gameObject);
    }
    // ------------------------------------------------------------------
    // 把人帶走.
    void Take()
    {
        // 播放抓人動作.
        pAI.AniPlay("Catch");

        // 調整面向前進.
        pAI.FaceAndMove(vecRunDir, 0.55f);

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
            // 調整面向前進.
            pAI.FaceAndMove(ObjTarget.transform.position - transform.position, 1);
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
            // 調整面向前進.
            if (pTempObj != null)
                pAI.FaceAndMove(pTempObj.transform.position - transform.position, 0.4f);
            return;
        }
    }
    // ------------------------------------------------------------------
    // 抓人.
    void Catch()
    {
        // 檢查距離是否可抓抓.
        if (GetDistance(gameObject, ObjTarget) > 0.175f)
            return;

        pAI.bHasTarget = true;
        if (ObjTarget && ObjTarget.GetComponent<AIPlayer>())
            ObjTarget.GetComponent<AIPlayer>().BeCaught(gameObject, 1);
        GetDir();      
    }
    // ------------------------------------------------------------------
    // 確認是否有可以追的肉肉.
    bool CheckMeet()
    {
        if (SysMain.pthis.ListMeet.Count <= 0)
            return false;

        foreach (GameObject pObj in SysMain.pthis.ListMeet)
        {
			if (GetDistance(gameObject, pObj) < fMeetDis)
                return true;
        }
        return false;
    }
    // ------------------------------------------------------------------
    // 追肉肉!!!.
    void HuntMeet()
    {
        if (SysMain.pthis.ListMeet.Count <= 0)
            return;

        // 如果有抓目標就丟下目標.
        if (pAI.bHasTarget)
        {
            pAI.bHasTarget = false;
            if (ObjTarget && ObjTarget.GetComponent<AIPlayer>())
                ObjTarget.GetComponent<AIPlayer>().BeFree();
        }

        GameObject ObjMeet = null;
        foreach (GameObject pObj in SysMain.pthis.ListMeet)
        {
            if (!ObjMeet || GetDistance(gameObject, pObj) < GetDistance(gameObject, ObjMeet))
                ObjMeet = pObj;
        }
        // 調整面向前進.
        if (ObjMeet != null)
        {
            pAI.FaceAndMove(ObjMeet.transform.position - transform.position, 1.3f);
            if (GetDistance(gameObject, ObjMeet) < 0.08f && ObjMeet.GetComponent<Btn_Meet>())
                ObjMeet.GetComponent<Btn_Meet>().DelAMeet();
        }        
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
}