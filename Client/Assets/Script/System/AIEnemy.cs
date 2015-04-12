using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIEnemy : MonoBehaviour 
{
	// 怪物編號
	public int iMonster = 1;
	// 怪物模式
	public ENUM_ModeMonster emMode = ENUM_ModeMonster.Null;
	// HP
	public int iHP = 0;
	// 移動速度
	public float fMoveSpeed = 0.0f;
	// 威脅
	public int iThreat = 0;
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
		DBFMonster DBFData = GameDBF.This.GetMonster(iMonster) as DBFMonster;

		if(DBFData == null)
		{
			Debug.Log("DBFMonster(" + iMonster + ") null");
			return;
		}//if

		emMode = (ENUM_ModeMonster)DBFData.Mode;
        iHP = DBFData.HP;
		fMoveSpeed = DBFData.MoveSpeed;
		iThreat = DBFData.Threat;

        PosStart = transform.position;
	}
    // ------------------------------------------------------------------
    void OnDestroy()
    {
        if (SysMain.pthis.Enemy.ContainsKey(gameObject))
            SysMain.pthis.Enemy.Remove(gameObject);
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
                iThreat = 0;
                bHasTarget = false;
                if (ObjTarget.GetComponent<AIPlayer>())
                    ObjTarget.GetComponent<AIPlayer>().BeFree();                
            }
            // 播放逃跑動作.
            if (pAni)
                pAni.Play("Escape");
            // 取向量.
            if (vecRunDir == Vector3.zero)
            {
                vecRunDir = PosStart - transform.position;
                GameData.pthis.iKill++;
            }

            MoveTo(vecRunDir, fMoveSpeed * 4);
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
                vecRunDir = PosStart - transform.position;
                if (ObjTarget.GetComponent<PlayerFollow>())
                    ObjTarget.GetComponent<PlayerFollow>().vecDir = vecRunDir;
            }

            MoveTo(vecRunDir, fMoveSpeed * 0.5f);
            
            if (EnemyCreater.pthis.CheckPos(gameObject))
            {
                Destroy(ObjTarget);
                if (ObjTarget)
                {
                    GameData.pthis.iDead++;
                    Destroy(ObjTarget);
                }  
                Destroy(gameObject);
            }
            return;
        }

        // 確認目標.
        FindTarget();        

        // 如果有目標且沒抓人時，追蹤目標
        //if (!ObjTarget && !bHasTarget)
        Chace();
    }
    // ------------------------------------------------------------------
    void OnTriggerStay2D(Collider2D other)
    {
        if (iHP > 0 && other.gameObject.tag == "Light")
        {
            if (!SysMain.pthis.Enemy.ContainsKey(gameObject))
                SysMain.pthis.Enemy.Add(gameObject, iThreat);
        }
    }
    // ------------------------------------------------------------------
    void OnTriggerExit2D(Collider2D other)
    {
        if (iHP > 0 && other.gameObject.tag == "Light")
        {
            if (SysMain.pthis.Enemy.ContainsKey(gameObject))
                SysMain.pthis.Enemy.Remove(gameObject);
        }
    }
    // ------------------------------------------------------------------
    public void AddHP(int iValue)
    {
        iHP += iValue;

        // 沒血逃跑.
        if (iHP <= 0)
        {
            NGUITools.PlaySound(ClipDead, 0.8f);
            // 從可攻擊陣列移除.
            if (SysMain.pthis.Enemy.ContainsKey(gameObject))
                SysMain.pthis.Enemy.Remove(gameObject);       
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

        // 如果已經有目標要檢查目標是否被抓了.
        if (ObjTarget && ObjTarget.GetComponent<AIPlayer>() && !ObjTarget.GetComponent<AIPlayer>().bBeCaught)
            return;

        //測試先指定目標.
        KeyValuePair<GameObject, int> pTemp = LibCSNStandard.Tool.RandomPick(SysMain.pthis.Role);
        ObjTarget = pTemp.Key;

        // 清空目標.
        //ObjTarget = null;        
    }
    // ------------------------------------------------------------------
    void Chace()
    {
        // 沒有目標就放棄追蹤.
        if (!ObjTarget)
            return;

        // 檢查距離是否可抓抓.
        if (Vector2.Distance(transform.position, ObjTarget.transform.position) < 0.055f)
        {
            bHasTarget = true;
            iThreat += 5;
            if (ObjTarget.GetComponent<AIPlayer>())
                ObjTarget.GetComponent<AIPlayer>().BeCaught(gameObject);
        }

        // 取向量.
        MoveTo(ObjTarget.transform.position - transform.position, fMoveSpeed);
    }
    // ------------------------------------------------------------------
    void MoveTo(Vector3 vecDirection, float fSpeed)
    {        
        // 把z歸零, 因為沒有要動z值.
        vecDirection.z = 0;

        if (vecDirection.x < 0)
            transform.rotation = new Quaternion(0, 180, 0, 0);
        else
            transform.rotation = new Quaternion(0, 0, 0, 0);
        // 把物件位置朝目標向量(玩家方向)移動.
        transform.position += vecDirection.normalized * fSpeed * Time.deltaTime;
    }
}
