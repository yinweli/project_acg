using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIEnemy : MonoBehaviour 
{
	// 怪物編號
	public int iMonster = 1;
	// HP
	public int iHP = 0;
    // 怪物資料.
    public DBFMonster DBFData;

    // 目標
    public GameObject ObjTarget = null;
    // 是否已抓了人.
    public bool bHasTarget = false;

    public Animator pAni = null;

    public AudioClip ClipHurt;
    public AudioClip ClipDead;

    // 記住起點.
    Vector3 PosStart = new Vector3();

    Vector3 vecRunDir = Vector3.zero;
    
	// Use this for initialization
	void Start () 
    {
        SysMain.pthis.Enemy.Add(gameObject);

		DBFMonster DBFData = GameDBF.This.GetMonster(iMonster) as DBFMonster;

		if(DBFData == null)
		{
			Debug.Log("DBFMonster(" + iMonster + ") null");
			return;
		}//if

        iHP = DBFData.HP;
        PosStart = transform.position;
	}
    // ------------------------------------------------------------------
    public void SetLayer(int iLayer)
    {
        UI2DSprite[] Enemy = GetComponentsInChildren<UI2DSprite>();

        // 依照角色切換layer.
        for (int i = 0; i < Enemy.Length; i++)
        {
            Enemy[i].depth = Enemy[i].depth + (iLayer * 30);

            Vector3 vecPos = Enemy[i].gameObject.transform.localPosition;
            vecPos.z = -0.00002f * (float)Enemy[i].depth;
            Enemy[i].gameObject.transform.localPosition = vecPos;
        }
    }
    // ------------------------------------------------------------------
    void OnDestroy()
    {
        if (SysMain.pthis.AtkEnemy.ContainsKey(gameObject))
            SysMain.pthis.AtkEnemy.Remove(gameObject);
    }
    // ------------------------------------------------------------------
    void Update()
    {
        if (!SysMain.pthis.bIsGaming)
            return;

        // 沒血逃跑.
        if (iHP <= 0)
        {
            // 如果有抓目標就丟下目標.
            if (bHasTarget)
            {
                bHasTarget = false;
                if (ObjTarget.GetComponent<AIPlayer>())
                    ObjTarget.GetComponent<AIPlayer>().BeFree();                
            }
            // 播放逃跑動作.
            if (pAni)
                pAni.Play("Escape");
            // 取向量.
            if (vecRunDir == Vector3.zero)
                vecRunDir = PosStart - transform.position;

            MoveTo(vecRunDir, DBFData.MoveSpeed * 4);
            if (EnemyCreater.pthis.CheckPos(gameObject))
                Destroy(gameObject);
            return;
        }

        // 已有抓人逃跑模式.
        if (bHasTarget)
        {
            // 播放逃跑動作.
            if (pAni)
                pAni.Play("Catch");
            // 取向量.
            if (vecRunDir == Vector3.zero)
            {
                vecRunDir = ObjTarget.GetComponent<AIPlayer>().GetDeadPos() - transform.position;
				if (ObjTarget && ObjTarget.GetComponent<PlayerFollow>())
                    ObjTarget.GetComponent<PlayerFollow>().vecDir = vecRunDir;
            }

            MoveTo(vecRunDir, DBFData.MoveSpeed * 0.5f);
            
            if (EnemyCreater.pthis.CheckPos(gameObject))
            {
                if (ObjTarget && ObjTarget.GetComponent<AIPlayer>())
                    ObjTarget.GetComponent<AIPlayer>().BeKill();
                Destroy(gameObject);
            }
            return;
        }

        // 確認目標.
        FindTarget();        

        // 如果有目標且沒抓人時，追蹤目標
        Chace();
    }
    // ------------------------------------------------------------------
    void OnTriggerStay2D(Collider2D other)
    {
        if (iHP > 0 && other.gameObject.tag == "Light")
        {
            if (!SysMain.pthis.AtkEnemy.ContainsKey(gameObject))
                SysMain.pthis.AtkEnemy.Add(gameObject, GetTheat());
        }
    }
    // ------------------------------------------------------------------
    void OnTriggerExit2D(Collider2D other)
    {
        if (iHP > 0 && other.gameObject.tag == "Light")
        {
            if (SysMain.pthis.AtkEnemy.ContainsKey(gameObject))
                SysMain.pthis.AtkEnemy.Remove(gameObject);
        }
    }
    // ------------------------------------------------------------------
    void OnClick()
    {
        if (iHP > 0)
            AddHP(-GameDefine.iDamageClick);
    }
    // ------------------------------------------------------------------
    public void AddHP(int iValue)
    {
        if (iHP <= 0 && iValue < 0)
            return;

        iHP += iValue;

		// 播放受擊特效.
		if(iValue < 0)
			UITool.pthis.CreateUI(gameObject,"Prefab/S_Hit");

        // 沒血逃跑.
        if (iHP <= 0)
        {
            NGUITools.PlaySound(ClipDead, 0.8f);
            GameData.pthis.iKill++;
            // 從可攻擊陣列移除.
            if (SysMain.pthis.AtkEnemy.ContainsKey(gameObject))
                SysMain.pthis.AtkEnemy.Remove(gameObject);       
        }
        else
            NGUITools.PlaySound(ClipHurt, 0.8f);
    }
    // ------------------------------------------------------------------
    void FindTarget()
    {
        // 如果已經抓了人就不用再找目標了.
        if (bHasTarget)
            return;

        // 如果可抓追著離自己最近的角色跑.
        if(ToolKit.CatchRole.Count <= 0)
        {
            ObjTarget = null;
            return;
        }

        // 沒有目標或目標已不存在可抓佇列就給新目標.
        if (!ObjTarget || !ToolKit.CatchRole.ContainsKey(ObjTarget))
            ObjTarget = ToolKit.GetEnemyTarget();
    }
    // ------------------------------------------------------------------
    void Chace()
    {
        if(SysMain.pthis.Role.Count == 0)
            return;

        // 沒有目標就給他慢速追個角色.
        if (!ObjTarget)
        {
            GameObject pTempObj = null;
            foreach (KeyValuePair<GameObject, int> itor in SysMain.pthis.Role)
            {
                if(!pTempObj || Vector2.Distance(transform.position, itor.Key.transform.position) < Vector2.Distance(transform.position, pTempObj.transform.position))
                    pTempObj = itor.Key;
            }
            if (pTempObj != null)
                MoveTo(pTempObj.transform.position - transform.position, DBFData.MoveSpeed * 0.4f);
            return;
        }

        // 檢查距離是否可抓抓.
        if (Vector2.Distance(transform.position, ObjTarget.transform.position) < 0.175f)
        {
            bHasTarget = true;
            if (ObjTarget.GetComponent<AIPlayer>())
                ObjTarget.GetComponent<AIPlayer>().BeCaught(gameObject);

            return;
        }

        // 取向量.
        MoveTo(ObjTarget.transform.position - transform.position, DBFData.MoveSpeed);
    }
    // ------------------------------------------------------------------
    public int GetTheat()
    {
        if (bHasTarget)
            return DBFData.Threat + 5;
        else
            return DBFData.Threat;
    }
    // ------------------------------------------------------------------
    void MoveTo(Vector3 vecDirection, float fSpeed)
    {        
        if (vecDirection.x < 0)
            FaceTo(-1);
        else
            FaceTo(1);
        ToolKit.MoveTo(gameObject, vecDirection, fSpeed);
    }
    // ------------------------------------------------------------------
    public void FaceTo(int iFace)
    {
        transform.localScale = new Vector3(iFace, transform.localScale.y, transform.localScale.z);
    }
}
