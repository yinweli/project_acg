﻿using UnityEngine;
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
    public void SetLayer(int iLayer)
    {
        UI2DSprite[] Enemy = GetComponentsInChildren<UI2DSprite>();

        // 依照角色切換layer.
        for (int i = 0; i < Enemy.Length; i++)
        {
            Enemy[i].depth = Enemy[i].depth + (iLayer * 20);

            Vector3 vecPos = Enemy[i].gameObject.transform.localPosition;
            vecPos.z = -0.00002f * (float)Enemy[i].depth;
            Enemy[i].gameObject.transform.localPosition = vecPos;
        }
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
                vecRunDir = PosStart - transform.position;                

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
				if (ObjTarget && ObjTarget.GetComponent<PlayerFollow>())
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

        // 如果可抓佇列無人就走向CamCtrl.
        if(SysMain.pthis.CatchRole.Count <= 0)
        {
            ObjTarget = CameraCtrl.pthis.gameObject;
            return;
        }

        // 沒有目標或目標已不存在可抓佇列就給新目標.
        if (!ObjTarget || !SysMain.pthis.CatchRole.ContainsKey(ObjTarget))
        {
            KeyValuePair<GameObject, int> pTemp = LibCSNStandard.Tool.RandomPick(SysMain.pthis.CatchRole);
            ObjTarget = pTemp.Key;
        }       
    }
    // ------------------------------------------------------------------
    void Chace()
    {
        // 沒有目標就放棄追蹤.
        if (!ObjTarget)
            return;

        // 檢查距離是否可抓抓.
        if (ObjTarget != CameraCtrl.pthis.gameObject && Vector2.Distance(transform.position, ObjTarget.transform.position) < 0.18f)
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
            FaceTo(-1);
        else
            FaceTo(1);
        // 把物件位置朝目標向量(玩家方向)移動.
        transform.position += vecDirection.normalized * fSpeed * Time.deltaTime;
    }
    // ------------------------------------------------------------------
    public void FaceTo(int iFace)
    {
        transform.localScale = new Vector3(iFace, transform.localScale.y, transform.localScale.z);
    }
}
